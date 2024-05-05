using Microsoft.AspNetCore.Mvc;
using project.Models.ViewModel;

namespace project.Models
{
    public class DepartmentBusiness
    {
        ItiContext context = new ItiContext();
        public List<Name_Id_VM> get_all_names_ids()
        {
            return context.departments.Select(d => new Name_Id_VM { name = d.name , id = d.id})
                               .ToList<Name_Id_VM>(); 
        }

       
    }
}
