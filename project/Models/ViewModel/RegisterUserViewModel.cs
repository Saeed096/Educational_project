using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace project.Models.ViewModel
{
    public class RegisterUserViewModel
    {
        [Required , MaxLength(10) , MinLength(3), DisplayName("User name")]
        public string userName { get; set; }

        [Required , DataType(DataType.Password), DisplayName("Password")]
        public string password { get; set; }

        [Required , Compare("password") , DataType(DataType.Password), 
            Display(Name ="Confirm password")]
        public string confirmPassword { get; set; }

        [Required, DisplayName("Address")]
        public string address { get; set; } 
    }
}
