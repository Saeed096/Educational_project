using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models
{
    public class Trainee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string grade { get; set; }
        public string address { get; set; }

        [ForeignKey("department")]
        public int? dept_id { get; set; }
        public virtual ICollection<CrsResult> CrsResults { get; set; }
        public Department department { get; set; } 
    }
}
