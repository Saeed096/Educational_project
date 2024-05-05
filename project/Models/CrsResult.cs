using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models
{
    public class CrsResult
    {
        public int id { get; set; }
        public double degree { get; set; }

        [ForeignKey("course")]
        public int? crs_id { get; set; }

        [ForeignKey("trainee")]
        public int? trainee_id { get; set; }

        public Course course { get; set; }
        public Trainee trainee { get; set; }
    }
}
