using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace project.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> _roleManager )
        {
            roleManager = _roleManager;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> addAdminRole()
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Instructor";
            await roleManager.CreateAsync(role);
            return View("register", "account");
        }
    }
}
