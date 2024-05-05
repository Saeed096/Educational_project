namespace project.Models
{
    public class Department
    {
        public int id { get; set; }
        public string name { get; set; }
        public string manager { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Trainee> Trainees { get; set; }
    }
}
