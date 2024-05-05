using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models.ViewModel
{
    public class Trainee_with_dep_crs_lists_VM
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string grade { get; set; }
        public string address { get; set; }
        public int? dept_id { get; set; }

        public List<Name_Id_VM> deps = new List<Name_Id_VM>();
        public List<Name_Id_VM> courses = new List<Name_Id_VM>();
/*!! try make it only get , set*/        public List<int> trainee_courses_ids {  get; set; } = new List<int>(); 

    }
}
