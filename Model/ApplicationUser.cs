using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PayRollManagement.Model
{
    public class ApplicationUser:IdentityUser
    {
        public string RefreshToken { get; set; }
        public System.DateTime? RefreshTokenExpiryTime { get; set; }
       
    }
}
