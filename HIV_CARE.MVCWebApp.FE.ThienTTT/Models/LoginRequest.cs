using Microsoft.AspNetCore.Antiforgery;

namespace HIV_CARE.MVCWebApp.FE.ThienTTT.Models
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
