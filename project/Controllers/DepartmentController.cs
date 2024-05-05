using Microsoft.AspNetCore.Mvc;
using project.Models;

namespace project.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentBusiness DepartmentBusiness = new DepartmentBusiness();
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult get_courses_by_dept_id(int id)
        //{
        //    List<Course> dept_courses = DepartmentBusiness.get_courses(id);
        //    return Json(dept_courses);
        //}
    }
}
