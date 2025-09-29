using AutoMapper;
using Ecommerce_Assignment_FullStack_FreeLance_.Application.Contract;
using Ecommerce_Assignment_FullStack_FreeLance_.Dtos.UserDtos;
using Ecommerce_Assignment_FullStack_FreeLance_.Dtos.ViewResult;
using Ecommerce_Assignment_FullStack_FreeLance_.Models;
using BCrypt.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Ecommerce_Assignment_FullStack_FreeLance_.Dtos.ProductDtos;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Application.Services
{
    public class userService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public userService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ResultView<UserDto>> DeleteUser(Guid UserId)
        {
            var user = await _userRepository.GetByIdAsync(UserId);
            if (user == null)
            {
                return new ResultView<UserDto>();
            }
            user.IsDeleted = true;
            await _userRepository.SaveChangesAsync();
            return new ResultView<UserDto>
            {
                Entity = _mapper.Map<UserDto>(user),
                IsSuccess = true,
                Message = "User Deleted Successfully"
            };
        }

        public async Task<ResultDataList<GetAllUserDto>> GetAllPaginationUser(int items, int pagenumber)
        {
            var Alldata = (await _userRepository.GetAllAsync());
            var users = Alldata.Where(u => u.IsDeleted == false).Skip(items * (pagenumber - 1)).Take(items)
                                              .Select(u=> new GetAllUserDto
                                              {
                                                  Id = u.Id,
                                                  UserName = u.UserName,
                                                  Email = u.Email,
                                                  LastLoginTime = u.LastLoginTime,
                                              }).ToList();
            ResultDataList<GetAllUserDto> resultDataList = new ResultDataList<GetAllUserDto>();
            resultDataList.Entities = users;
            resultDataList.Count = users.Count();
            return resultDataList;
        }

        public async Task<UserDto> GetUserById(Guid ID)
        {
            var user = await _userRepository.GetByIdAsync(ID);
             return _mapper.Map<UserDto>(user);
        }
        public async Task<ResultView<RegisterDto>> RegisterUser(RegisterDto model)
        {
            var oldUsers = await _userRepository.GetAllAsync();
            if (oldUsers.Any(u => u.Email == model.Email || u.UserName == model.UserName))
                return new ResultView<RegisterDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Email or User Name already exists"
                };
            var user = _mapper.Map<User>(model);
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            user.LastLoginTime = DateTime.Now; 
            var newUser = await _userRepository.CreateAsync(user);
            await _userRepository.SaveChangesAsync();
            return new ResultView<RegisterDto>
            {
                Entity = _mapper.Map<RegisterDto>(newUser),
                IsSuccess = true,
                Message = "Register Successfully"
            };


        }
        public async Task<ResultViewLogIn<LoginDto>> LoginUser(LoginDto model)
        {
            // Get the actual user entity, not a boolean
            var user = await _userRepository.GetByUserNameAsync(model.UserName);

            // Check if user exists and verify password
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                return new ResultViewLogIn<LoginDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Invalid username or password"
                };
            }

            // Update the existing tracked entity
            user.LastLoginTime = DateTime.UtcNow;

            // Update the existing user instead of creating a new one
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            var token = GenerateJwtToken(user);

            return new ResultViewLogIn<LoginDto>
            {
                Entity = null,
                IsSuccess = true,
                Message = "Login successfully",
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(GetTokenExpirationHours()),
            };
        }

        public Task<bool> LogoutUser()
        {
           return Task.FromResult(true);
        }
        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("userId", user.Id.ToString()),
            new Claim("userName", user.UserName)
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(GetTokenExpirationHours()),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private int GetTokenExpirationHours()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            return int.Parse(jwtSettings["ExpirationHours"] ?? "24");
        }
    }
}
