using AspNetCoreIdentityApp.Web.Models;
using AspNetCoreIdentityApp.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AspNetCoreIdentityApp.Web.Extenisons;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;


namespace AspNetCoreIdentityApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _UserManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {

            _logger = logger;
            _UserManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {

            return View();
        }
        [HttpPost]
        public async Task <IActionResult> SignUp(SignUpViewModel request)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }      
            var identityResult = await _UserManager.CreateAsync(new() { UserName = request.UserName, PhoneNumber = request.Phone, Email = request.Email }, request.ConfirmPassword);

            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"]= "Uyelik kayit islemi basariyla gerceklemisdir";
                return RedirectToAction("Index");
            }
            foreach
             (IdentityError item in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);

            }
            return View();
        }

        public IActionResult SignIn()

        {
            return View();
        }

        [HttpPost]
        //return url, kullanıcı giriş yaptıktan sonra yönlendirilmek istenen sayfanın url'sidir. Eğer return url null ise, kullanıcı anasayfaya yönlendirilir.
        public async Task<IActionResult> SignIn(SignInViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //returnUrl null ise, kullanici ana sayfaya yonlendirilir.
            returnUrl ??= Url.Action("Index", "Home");

            var hasUser = await _UserManager.FindByEmailAsync(model.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya sifre yanlis");
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);

            //lockoutOnFailure parametresi true olarak ayarlandığında, kullanıcı belirli bir sayıda başarısız giriş denemesinden sonra kilitlenir. Bu, brute-force saldırılarına karşı koruma sağlar. Eğer lockoutOnFailure false olarak ayarlanırsa,
            //kullanıcı başarısız giriş denemelerinden sonra kilitlenmez ve sınırsız sayıda giriş denemesi yapabilir.
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>() { "3 dakika boyunca giris yapamazsınız." });
                return View();
            }

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelErrorList(new List<string>() { $"Email veya şifre yanlış", $"Başarısız giriş sayısı = " +
                    $"{await _UserManager.GetAccessFailedCountAsync(hasUser)}" });
                return View();
            }

            //if (hasUser.BirthDate.HasValue)
            //{
            //    await _signInManager.SignInWithClaimsAsync(hasUser, model.RememberMe, new[] { new Claim("birthdate", hasUser.BirthDate.Value.ToString()) });
            //}
            return Redirect(returnUrl!);

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
