using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.Models.ViewModel;
using System.Security.Claims;

namespace project.Controllers
{
    //[Authorize]
    //[AllowAnonymous]   // attr on contro..
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ItiContext itiContext;

        public AccountController(UserManager<AppUser> _usermanager , SignInManager<AppUser> _signInManager
            , ItiContext _itiContext)
        {
            userManager = _usermanager;
            signInManager = _signInManager;
            itiContext = _itiContext;
        }
        [HttpGet]
        public IActionResult register() 
        {
          return View(); 
        }

        [HttpPost]
        public async Task <IActionResult> register(RegisterUserViewModel model)  
        {
            if(ModelState.IsValid) 
            {
                AppUser user = new AppUser();
                user.UserName = model.userName;
                user.PasswordHash = model.password;
                user.address = model.address;
              IdentityResult result = await userManager.CreateAsync(user , user.PasswordHash);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    return View("login");
                }

                foreach (var item in result.Errors)
                    ModelState.AddModelError(string.Empty, item.Description);
            }
                return View();
        }

        [HttpGet]
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task <IActionResult> login(loginUserViewModel model)
        {
            if(ModelState.IsValid) 
            {
              AppUser appuser = await userManager.FindByNameAsync(model.userName);   //user name is unique in identity module

                if(appuser != null) 
                {
                    bool matched = await userManager.CheckPasswordAsync(appuser, model.password);
                    if (matched) 
                    {
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("address", appuser.UserName));
                       await signInManager.SignInWithClaimsAsync(appuser, model.rememberMe , claims);   // these claims not related to d.b >> but to set extra info to cookie >> relation bet memory and browser "send data"
                        if(User.IsInRole("Admin"))
                        return RedirectToAction("index", "course");

                        return RedirectToAction("index", "home"); 

                    }
                }

            }

            return View();
        }


        public async Task<IActionResult> logout() 
        {
           await signInManager.SignOutAsync();
            return View("login");
        }

        [HttpGet]
        [Authorize (Roles ="Admin")]   // if not authenticated >> def.. >> send u to login  >> check identity.isauthenticated "valid cookie"
        public IActionResult addAdmin()
        {
            //var query = from
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            string currentAdminId = claim.Value;

            string adminRoleId = itiContext.UserRoles.Where(ur => ur.UserId == currentAdminId)
                .Select(ur => ur.RoleId).FirstOrDefault();
            //itiContext.Users.Where()
            
            List<string> userNames = new List<string>();

            var query = 
                from user in itiContext.Users join
                userRole in itiContext.UserRoles
                on user.Id equals userRole.UserId
                where (userRole.RoleId != adminRoleId)
                select new { userName = user.UserName };

            foreach(var names in query)
            {
                userNames.Add(names.userName);
            }

            return View(userNames); 
        }  


        public async Task<IActionResult> confirmMakeAdmin(string userName)
        {
          AppUser user = await userManager.FindByNameAsync(userName);
          if(user != null)
            {
                await userManager.RemoveFromRoleAsync(user, "User");
                await userManager.AddToRoleAsync(user, "Admin");
                return RedirectToAction("addadmin");
            }
            return RedirectToAction("addadmin");
        }


       
    }
}
