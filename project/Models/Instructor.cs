using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models
{
    public class Instructor
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? image { get; set; }
        public int salary { get; set; }
        public string? address { get; set; }

        [ForeignKey("course")]
        public int? crs_id { get; set; }
        [ForeignKey("department")]
        public int? dept_id { get; set; }

        public Course course { get; set; }
        public Department department { get; set; }
    }
}
