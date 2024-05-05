using project.Models.ViewModel;
using project.Models;
using static project.Repository.InstructorRepository;

namespace project.Repository
{
    public interface IInstructorRepository
    {
        public List<Instructor> getAll();


        public Instructor getById(int id, InstructorInclude ins_inc);


        public List<Name_Id_VM> get_all_names_ids();


        public List<int> getInsIdsByCrsId(int id);

        public void Save();

        public List<Instructor> getInsListByCrsId(int id);

    }
}