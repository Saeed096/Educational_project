using project.Models.ViewModel;
using project.Models;
using Microsoft.EntityFrameworkCore;

namespace project.Repository
{
   public enum CourseInclude
    {
        department,
        none
    }
    public class CourseRepository : IcourseRepository
    {
        ItiContext context;

        public CourseRepository(ItiContext _context)
        {
            context = _context;
        }

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

        public Course getById(int id , CourseInclude ci = CourseInclude.none)
        {
            Course crs;
            switch(ci)
            {
                case CourseInclude.department:
                    crs = context.courses.Include(c => c.department)
                        .Where(c => c.id == id).First();
                    break;
                default:
                    crs = context.courses.Where(c => c.id == id).First();
                    break;
            }
            return crs;
        }

        public List<string> get_names()
        {
            return context.courses.Select(c => c.name).ToList();
        }

        public List<int> get_ids()
        {
            return context.courses.Select(c => c.id).ToList();
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



        public void Add(Course crs)
        {
            context.Add(crs);
        }

        public void Save()
        {
            context.SaveChanges();
        }

         public Course get(Func<Course , bool> filter , CourseInclude ci = CourseInclude.none)  // use it in controller instead of all (get by id , get ...)
        {
           return context.courses.Where(filter).First();
        }
    }
}
