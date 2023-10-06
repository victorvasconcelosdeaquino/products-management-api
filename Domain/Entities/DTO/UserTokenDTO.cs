using System;

namespace Domain.Entities.DTO
{
    public  class UserTokenDTO
    {
        public bool IsAuthenticated { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
