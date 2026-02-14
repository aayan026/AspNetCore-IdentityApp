using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Email Boş birakilamaz!")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz!")]
        [Display(Name = "Email : ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre Boş birakilamaz!")]
        [Display(Name = "Şifre : ")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public SignInViewModel()
        {

        }
        public SignInViewModel(string email,string password)
        {
            Email= email;
            Password= password;
        }
    }
}
