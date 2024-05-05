using Microsoft.EntityFrameworkCore;
using project.Models.ViewModel;

namespace project.Models
{
    public class CourseBusiness
    {
        ItiContext context = new ItiContext();

        public List<Course> getAll(out bool is_remaining_courses, 
            string search_key = null, int page_num = 1)
        {
            List<Course> courses = context.courses.Where(c => c.is_deleted == false)
                .Skip((page_num - 1) * 5).Take(5).ToList();

            if (context.courses.Where(c => c.id > courses.Last().id)
                   .FirstOrDefault() != null)
            {
                is_remaining_courses = true;
            }

            else
                is_remaining_courses = false;

            if (search_key != null)
            {
               List<Course> filtered_courses = new List<Course>();

                filtered_courses = context.courses
                    .Where(c => c.name.ToLower().Contains(search_key.ToLower()) 
                    && c.is_deleted == false).ToList();
                return filtered_courses;
            }

            else
                return courses;
        }

        public Course getById(int id)
        {
            return context.courses.Include(c => c.department).Where(c => c.id == id).First();  // include mechanism of action >> get val of field that refer to department "f.k" >> search in department "Department table" for any record with p.k matches the f.k >> get it
        }

        public List<string> get_names()
        {
            return context.courses.Select(c => c.name).ToList();
        }

        public List<int> get_ids()
        {
            return context.courses.Select(c => c.id).ToList();
        }

        public Course get_course_by_id(int id)  
        {
            return context.courses.Where(c => c.id == id).FirstOrDefault();   // handle invalid id
        }


        public List<Name_Id_VM> get_all_names_ids()
        {
            return context.courses.Select(c => new Name_Id_VM { name = c.name, id = c.id })
                               .ToList<Name_Id_VM>();
        }

        public List<Name_Id_VM> get_all_names_ids_by_dept_id(int? id)
        {
            return context.courses.Where(c => c.dept_id == id)
                .Select(c => new Name_Id_VM { name = c.name, id = c.id }).ToList();
        }

        public List<Course> get_courses(int id)
        {
            List<Course> dept_courses = context.courses
                .Where(c => c.dept_id == id).ToList();

            return dept_courses;
        }
    }
}
