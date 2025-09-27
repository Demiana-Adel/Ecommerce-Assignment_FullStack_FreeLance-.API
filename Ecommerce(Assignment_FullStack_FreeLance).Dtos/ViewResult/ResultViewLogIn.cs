using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Dtos.ViewResult
{
    public class ResultViewLogIn<TEntity>
    { 
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public TEntity Entity { get; set; }
    
     }
}
