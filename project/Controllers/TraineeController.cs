using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using project.Models;
using project.Models.ViewModel;

namespace project.Controllers
{
    public class TraineeController : Controller 
    {
        //ItiContext context;
        //public TraineeController(ItiContext context)
        //{
        //    this.context = context;
        //}
        TraineeBusiness traineeBusiness = new TraineeBusiness();
        CourseBusiness courseBusiness = new CourseBusiness();
        DepartmentBusiness departmentBusiness = new DepartmentBusiness();
        Stu_names_list_Crs_names_list_VM names_list = new Stu_names_list_Crs_names_list_VM();
        ItiContext context = new ItiContext();
        public ViewResult SearchForResult()
        {
/**/            List<string> stu_names = traineeBusiness.get_names();  // which is right >> can write query here????
/**/            List<int> stu_ids = traineeBusiness.get_ids();
                List<string> crs_names = courseBusiness.get_names();
/**/            List<int> crs_ids = courseBusiness.get_ids();

            for (int i = 0; i < traineeBusiness.get_names().Count; i++)
            {
                Name_Id_VM temp = new Name_Id_VM {name = stu_names[i], id = stu_ids[i] };
                names_list.stu_list.Add(temp);
            }

            for (int i = 0; i < courseBusiness.get_names().Count; i++)
            {
                Name_Id_VM temp = new Name_Id_VM { name = crs_names[i], id = crs_ids[i] };
                names_list.crs_list.Add(temp);
            }

            return View("SearchForResult" , names_list);
        }

        public IActionResult ShowResult(int stu_id, int crs_id)
        {
            CourseName_TraineeName_Degree_Color_VM temp = new CourseName_TraineeName_Degree_Color_VM(); 
            Course course = courseBusiness.get_course_by_id(crs_id);
           Trainee trainee = traineeBusiness.get_trainee_by_id(stu_id);
           double degree = traineeBusiness.get_result(stu_id, crs_id);


            if (course != null && trainee != null)
            {
                if (degree >= course.minDegree)
                    temp.color = "green";
                else
                    temp.color = "red";

                temp.degree = degree;
                temp.courseName = course.name;
                temp.traineeName = trainee.name;
                temp.image = trainee.image;
                return View("ShowResult", temp);
            }
            return NotFound();
        }


        public IActionResult show_all_results(int stu_id)
        {
           CourseName_TraineeName_Degree_Color_LIST_VM model 
                = traineeBusiness.get_all_results(stu_id);

            if (model.traineeNames.First() != null)
                return View("show_all_results", model);
            else
                return NotFound();
        }

        public IActionResult index()
        {
            List<Trainee> trainees = traineeBusiness.getAll();
            return View("index", trainees);
        }

        public IActionResult detail(int id)
        {
            Trainee trainee = traineeBusiness.get_trainee_by_id(id);
            if(trainee != null)
            return View("detail", trainee);
           
            else
                return NotFound();
        }

        public IActionResult update(int id)
        {
            Trainee trainee = traineeBusiness.get_trainee_by_id(id);
            Trainee_with_dep_crs_lists_VM model = new Trainee_with_dep_crs_lists_VM();
            model.id = trainee.id;
            model.name = trainee.name;
            model.address = trainee.address;
            model.dept_id = trainee.dept_id;
            model.image = trainee.image;
            model.grade = trainee.grade;
            model.deps = departmentBusiness.get_all_names_ids().ToList();
            model.courses = courseBusiness.get_all_names_ids().ToList();
            return View("update", model);
        }

        public IActionResult saveUpdate(Trainee_with_dep_crs_lists_VM model)
        {
            if(model.name != null && model.trainee_courses_ids.Count > 0 && model.dept_id != 0)
            {
                Trainee trainee = context.trainees.Where(t => t.id == model.id)
                    .FirstOrDefault();
                    trainee.name = model.name;
                    trainee.address = model.address;
                    trainee.dept_id = model.dept_id;
                    trainee.image = model.image;
                    trainee.grade = model.grade;
                    trainee.id = model.id;
                    context.Update(trainee);
                    context.SaveChanges();         // should i add degrees after adding student courses???????? no
                    return RedirectToAction("index");
            }
            model.deps = departmentBusiness.get_all_names_ids();
            model.courses = courseBusiness.get_all_names_ids();
            return View("update" , model);
        }
    }
}
