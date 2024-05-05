using Microsoft.AspNetCore.Mvc;

namespace project.Controllers
{
    public class Session2Controller : Controller
    {
        public ContentResult get_session()
        {
          string data = HttpContext.Session.GetString("data");
            return Content(data);
        }
    }
}
