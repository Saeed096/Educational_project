using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace project.Models.ViewModel
{
    public class Course_with_dep_ins_list_VM
    {
        public int id { get; set; }

        [Display(Name="Course name")]  
        [Required,MinLength(3,ErrorMessage = "name must has 3 characters at least")
            , MaxLength(20)]
        [Unique]   
        // custom attr is not recognized by jquery "will not be shown in html tag" like remote and u doesnot make ajax call inside it so u cannot get partial response as we do in remote so must refresh >> full request >> can go to server >> then this attr work >> see impact >> if no problem cont process >> if err return with the err msg to be dispalyed in div "asp-validation-summary" 
        public string name { get; set; }


        [Required(ErrorMessage ="required field") ,
            Range(50,100, ErrorMessage ="degree must be between 50 , 100")]
        public double degree { get; set; }

        [Required]   //when try set min deg >> go to required attr first >> check its isvalid >> if false return its err msg in span
        [Remote("CheckMinDegreeValidation" , "Course" , ErrorMessage ="min degree can not be greater than degree"    // remote is understandable via jquery "will be shown on inspect "in html tag"" >> ajax >> go to server >> get partial response >> no need for refresh , remote attr take its field with it to the e.p
            , AdditionalFields = "degree")]
        public double minDegree { get; set; }

        [Remote("checkDivisionOver3" , "Course" , ErrorMessage ="Hours must be from 3 multiples")]
        public int hours { get; set; }
        public int? dept_id { get; set; }

        public List<Name_Id_VM> departments_names_ids = new List<Name_Id_VM>();
        public List<Name_Id_VM> instructors_names_ids = new List<Name_Id_VM>();
       public List<int> ins_ids {  get; set; } = new List<int>(); // or get; set; only >> mapping works with prop "get; set; is a must"

    }
}
