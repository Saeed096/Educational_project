using project.Models.ViewModel;
using project.Models;

namespace project.Repository
{
    public interface IcourseRepository
    {

        public List<Course> getAll(out bool is_remaining_courses,
           string search_key = null, int page_num = 1);
      

        public Course getById(int id , CourseInclude ci);
       

        public List<string> get_names();


        public List<int> get_ids();


        public List<Name_Id_VM> get_all_names_ids();


        public List<Name_Id_VM> get_all_names_ids_by_dept_id(int? id);


        public List<Course> get_courses(int id);

        public void Add(Course crs);

        public void Save();

        public Course get(Func<Course,bool> filter, CourseInclude ci);



    }
}