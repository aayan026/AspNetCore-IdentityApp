using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace AspNetCoreIdentityApp.Web.ViewModels;

public class SignUpViewModel
{
    public SignUpViewModel()
    {
    }
    public SignUpViewModel(string username, string email, string phone, string password)
    {
        UserName = username;
        Email = email;
        Phone = phone;
        Password = password;
    }
    [Required(ErrorMessage = "Kullanıcı Adı Boş birakilamaz!")]
    [Display(Name = "Kullanıcı Adı : ")]
    public string UserName { get; set; }

    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz!")]
    [Required(ErrorMessage = "Email Boş birakilamaz!")]
    [Display(Name = "Email : ")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Telefon Boş birakilamaz!")]
    [Display(Name = "Telefon : ")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Şifre Boş birakilamaz!")]
    [Display(Name = "Şifre : ")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Şifre Tekrari Boş birakilamaz!")]
    [Compare(nameof(Password),ErrorMessage = "Şifreler Uyuşmuyor!")]
    [Display(Name = "Şifre Tekrar : ")]
    public string ConfirmPassword { get; set; }
}
