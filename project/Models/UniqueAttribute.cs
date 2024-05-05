using project.Models.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class UniqueAttribute : ValidationAttribute
    {
        ItiContext context = new ItiContext();
        //ItiContext context;
        //public UniqueAttribute(ItiContext context)
        //{
        //    this.context = context;
        //}
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
           
            string crs_name = value.ToString();
            Course_with_dep_ins_list_VM crsContext = validationContext.ObjectInstance as Course_with_dep_ins_list_VM;
            Course? crs = context.courses.FirstOrDefault(c => c.name == crs_name 
            && c.dept_id == crsContext.dept_id);

            if (crs != null && (
                (crsContext.id == 0) ||
                (crs.id != crsContext.id)
                )) 
            {
                return new ValidationResult("name must be unique");
            }
            else
                return ValidationResult.Success;
        }
    }
}
