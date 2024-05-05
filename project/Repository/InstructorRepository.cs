using project.Models.ViewModel;
using project.Models;
using Microsoft.EntityFrameworkCore;

namespace project.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        public enum InstructorInclude
        {
            department,
            course,
            departmentCourse,
            none
        }
        ItiContext context;

        public InstructorRepository(ItiContext _context)
        {
            context = _context;
        }
        public List<Instructor> getAll()
        {
            return context.instructors.ToList();
        }

        public Instructor getById(int id , InstructorInclude ins_inc = InstructorInclude.none)
        {
            Instructor ins;
            switch(ins_inc)
            {
                case InstructorInclude.department:
                    ins = context.instructors.Include(i => i.department).Where(i => i.id == id).First();
                    break;
                case InstructorInclude.course:
                    ins = context.instructors.Include(i => i.course).Where(i => i.id == id).First();
                    break;
                case InstructorInclude.departmentCourse:
                    ins = context.instructors.Include(i => i.department).Include(i => i.course).Where(i => i.id == id).First();
                    break;
                default:
                    ins = context.instructors.Where(i => i.id == id).First();
                    break;
            }
            return ins;
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

        public void Save()
        {
            context.SaveChanges();
        }

        public List<Instructor> getInsListByCrsId(int id)
        {
            return context.instructors.Where(i => i.crs_id == id)
                 .ToList();
        }
    }
}
