using Microsoft.EntityFrameworkCore;
using project.Models.ViewModel;

namespace project.Models
{
    public class InstructorBusiness
    {
        ItiContext context = new ItiContext();
        //ItiContext context;
        //public InstructorBusiness(ItiContext context)
        //{
        //    this.context = context;
        //}
        public List<Instructor> getAll()
        {
            return context.instructors.ToList();
        }

        public Instructor getById(int id) 
        {
            return context.instructors.Include(i => i.department).Include(i => i.course).Where(i => i.id == id).First();
        }

        public List<Name_Id_VM> get_all_names_ids()
        {
            return context.instructors.Select(d => new Name_Id_VM { name = d.name, id = d.id })
                               .ToList();
        }

        public List<int> getInsIdsByCrsId(int id)
        {
           return context.instructors.Where(i => i.crs_id == id)
                .Select(i => i.id).ToList();
        }
    }
}
