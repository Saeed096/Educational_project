using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace project.Models.ViewModel
{
    public class loginUserViewModel
    {
        [Required, MaxLength(10), MinLength(3), DisplayName("User name")]
        public string userName { get; set; }

        [Required, DataType(DataType.Password), DisplayName("Password")]
        public string password { get; set; }

        [DisplayName("Remember me")]
        public bool rememberMe { get; set; }  
    }
}
