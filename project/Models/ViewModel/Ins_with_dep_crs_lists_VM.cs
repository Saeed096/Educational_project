using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models.ViewModel
{
    public class Ins_with_dep_crs_lists_VM
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? image { get; set; }
        public int salary { get; set; }
        public string? address { get; set; }
        public int? crs_id { get; set; }
        public int? dept_id { get; set; }

        public List<Name_Id_VM> deps_names_ids = new List<Name_Id_VM>();
        public List<Name_Id_VM> crs_names_ids = new List<Name_Id_VM> ();

    }
}
