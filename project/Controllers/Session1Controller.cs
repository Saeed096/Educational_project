using Microsoft.AspNetCore.Mvc;

namespace project.Controllers
{
    public class Session1Controller : Controller
    {
        public IActionResult set_session()
        {
            HttpContext.Session.SetString("data", "hello from session");
            return new EmptyResult();
        }
    }
}
