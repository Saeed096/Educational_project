using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Models;
using project.Models.ViewModel;
using project.Repository;
using System.Security.Claims;
using static project.Repository.InstructorRepository;

namespace project.Controllers
{
    public class CourseController : Controller
    {
       // ItiContext context = new ItiContext();
        CrsResultBusiness crsResultBusiness = new CrsResultBusiness();
        DepartmentBusiness departmentBusiness = new DepartmentBusiness();
       // CourseBusiness courseBusiness = new CourseBusiness();
       // InstructorBusiness instructorBusiness = new InstructorBusiness();
        //public IActionResult showCourseResult(int crs_id)
        //{
        //   CourseName_TraineeName_Degree_Color_LIST_VM model = 
        //        crsResultBusiness.get_crs_results_details(crs_id);

        //    if (model.course_names.Count() != 0)
        //        return View("showCourseResult", model);

        //    else
        //        return NotFound(); 
        //}

        IcourseRepository courseRepo;
        IInstructorRepository instructorRepo;
        public CourseController(IcourseRepository _courseRepo , IInstructorRepository _instructorRepo)
        {
            courseRepo = _courseRepo;
            instructorRepo = _instructorRepo; 

        }
        /**/
        [Authorize]
        public IActionResult index(string search_key, int? deleted_id, int page_num = 1)
        {
            if(deleted_id.HasValue)
            ViewBag.deleted_id = deleted_id.Value;

            Claim claim = User.Claims             // get data from cookie 
                          .FirstOrDefault(c => c.Type == ClaimTypes.Name);
            string userName = claim.Value;

            List<Course> courses = courseRepo.getAll(out bool is_remaining_courses, 
                search_key, page_num);
            ViewBag.page_num = page_num;
            ViewBag.is_remaining_courses = is_remaining_courses;
            ViewBag.search_key = search_key;
            ViewBag.userName = userName;

            return View("index", courses);
        }

        public IActionResult detail(int id)
        {
            Course course = courseRepo.getById(id , CourseInclude.department);
            return View("detail", course);
        }

        public IActionResult add()
        {
            Course_with_dep_ins_list_VM model = new Course_with_dep_ins_list_VM();
            model.departments_names_ids = departmentBusiness.get_all_names_ids();
            model.instructors_names_ids = instructorRepo.get_all_names_ids();
            return View("add",model);
        }

        public IActionResult saveAdd(Course_with_dep_ins_list_VM model) 
        {
           if(ModelState.IsValid) 
            { 
              Course crs = new Course {name = model.name, degree = model.degree, 
                  hours = model.hours , minDegree = model.minDegree , dept_id = model.dept_id};
               
                try
                {
                    courseRepo.Add(crs);
                    courseRepo.Save();

                    foreach (int id in model.ins_ids)
                    {
                        Instructor instructor = instructorRepo.getById(id , InstructorInclude.departmentCourse);
                        instructor.crs_id = crs.id;
                        /**/
                      //  context.Entry(instructor).State = EntityState.Modified;   // why should change state explicitly?????? this is different context than the obj track this instructor
                        instructorRepo.Save();
                    }
                    List<Course> courses = courseRepo.getAll(out _);
                    return RedirectToAction("index");
                    //return View("index", courses);
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError("saveAddErr", ex.InnerException.Message);
                }
            }
            model.departments_names_ids = departmentBusiness.get_all_names_ids();
            model.instructors_names_ids = instructorRepo.get_all_names_ids();
            return View("add", model);
        }


        public IActionResult delete(int deleted_id) 
        {
            Course crs = courseRepo.get( c => c.id == deleted_id , CourseInclude.none);
            if(crs != null)
            {
                crs.is_deleted = true;
                courseRepo.Save();

                List<Instructor> instructors = instructorRepo.getInsListByCrsId(deleted_id);
                foreach(Instructor ins in instructors)
                {
                    ins.crs_id = null;
                    instructorRepo.Save();
                }
            }
            return RedirectToAction("index");
        }


        public IActionResult get_courses_by_dept_id(int id)
        {
            List<Course> dept_courses = courseRepo.get_courses(id);
            return Json(dept_courses);
        }


        public IActionResult CheckMinDegreeValidation(double minDegree , double degree)
        {
            if (minDegree > degree)
                return Json(false);
            else
                return Json(true);
        }

        public IActionResult checkDivisionOver3(int hours)
        {
            if(hours % 3 == 0)
                return Json(true);
            else
                return Json(false);
        }

        public IActionResult update(int id)
        {
            Course crs = courseRepo.getById(id, CourseInclude.department);
            Course_with_dep_ins_list_VM model = new Course_with_dep_ins_list_VM();

            if(crs != null) 
            {
                model.id = crs.id;
                model.name = crs.name;
                model.hours = crs.hours;
                model.degree = crs.degree;
                model.minDegree = crs.minDegree;
                model.dept_id = crs.dept_id;
                model.ins_ids = instructorRepo.getInsIdsByCrsId(id);
                model.departments_names_ids = departmentBusiness.get_all_names_ids();
                model.instructors_names_ids = instructorRepo.get_all_names_ids();
                return View("Update", model);
            }
            return NotFound();
        }

        public IActionResult saveUpdate(Course_with_dep_ins_list_VM model)
        {
            if (ModelState.IsValid)
            {
                Course crs = courseRepo.getById(model.id , CourseInclude.department);

                try
                {
                    crs.id = model.id;
                    crs.name = model.name;
                    crs.hours = model.hours;
                    crs.degree = model.degree;
                    crs.minDegree = model.minDegree;
                    crs.dept_id = model.dept_id;
                   // context.Entry(crs).State = EntityState.Modified;
                    courseRepo.Save();

                    foreach (int id in model.ins_ids)
                    {
                        Instructor ins = instructorRepo.getById(id , InstructorInclude.departmentCourse);
                        ins.crs_id = crs.id;
                      //  context.Entry(ins).State = EntityState.Modified;
                        instructorRepo.Save();
                    }
                    return RedirectToAction("Index");
                }

                catch(Exception ex) 
                {
                    ModelState.AddModelError("saveUpdateErr", ex.InnerException.Message);
                }

            }
            model.instructors_names_ids = instructorRepo.get_all_names_ids();
            model.departments_names_ids = departmentBusiness.get_all_names_ids();
            return View("Update", model);
        }


    }
}

