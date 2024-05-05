using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models
{
    public class Course
    {
        public int id { get; set; }
        public string name { get; set; }

        public double degree { get; set; } 
        public double minDegree { get; set; }
        public int hours { get; set; } 

        [ForeignKey("department")]
        public int? dept_id { get; set; } 
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<CrsResult> CrsResults { get; set; }

        public Department department { get; set; }

        public bool is_deleted { get; set; }
    }
}
