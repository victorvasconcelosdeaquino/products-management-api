using System;

namespace Api.ViewModels
{
    public class UserTokenViewModel
    {
        public bool IsAuthenticated { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
