using Microsoft.AspNetCore.Identity;

namespace project.Controllers
{
    public class AppUser : IdentityUser
    {
        public string address { get; set; }
    }
}
