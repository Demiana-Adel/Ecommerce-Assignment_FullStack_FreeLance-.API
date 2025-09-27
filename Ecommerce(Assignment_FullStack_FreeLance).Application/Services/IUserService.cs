using Ecommerce_Assignment_FullStack_FreeLance_.Dtos.UserDtos;
using Ecommerce_Assignment_FullStack_FreeLance_.Dtos.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Application.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(Guid ID);
        Task<ResultDataList<GetAllUserDto>> GetAllPaginationUser(int items, int pagenumber);
        Task<ResultView<UserDto>> DeleteUser(Guid UserId);
        Task<ResultView<RegisterDto>> RegisterUser(RegisterDto model);
        Task<ResultViewLogIn<LoginDto>> LoginUser(LoginDto model);
        Task<bool> LogoutUser();
    }
}
