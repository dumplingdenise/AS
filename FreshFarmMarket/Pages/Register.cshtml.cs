using AspNetCore.ReCaptcha;
using FreshFarmMarket.Model;
using FreshFarmMarket.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
using System.Web;

namespace FreshFarmMarket.Pages
{
    public class RegisterModel : PageModel
    {
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }

        private IWebHostEnvironment _enviroment;

        [BindProperty]
        public Register RModel { get; set; }

        [BindProperty]
        public IFormFile? Upload { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment enviroment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _enviroment = enviroment;
        }

        public void OnGet()
        {
        }

        [ValidateReCaptcha]
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");

                var checkUser = await userManager.FindByEmailAsync(RModel.EmailAddress);
                if (checkUser != null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("{0} already exists.", RModel.EmailAddress);
                    return Page();
                };


                if (Upload != null)
                {
                    if (Upload.Length > 2 * 1024 * 1024)
                    {
                        TempData["FlashMessage.Type"] = "danger";
                        TempData["FlashMessage.Text"] = string.Format("Cannot upload this image");
                        return Page();
                    }
                    var uploadsFolder = "uploads";
                    var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                    var imagePath = Path.Combine(_enviroment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    RModel.ImageURL = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                }

                var checkPW = RModel.Password;
                var pwRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{12,}$";

                if (checkPW != null)
                {

                    var matched = Regex.IsMatch(checkPW, pwRegex);
                    if (matched = true)
                    {
                        var user = new ApplicationUser()
                        {
                            UserName = RModel.EmailAddress,
                            Email = HttpUtility.HtmlEncode(RModel.EmailAddress),
                            Name = HttpUtility.HtmlEncode(RModel.Name),
                            CreditCard = protector.Protect(RModel.CreditCard),
                            Gender = HttpUtility.HtmlEncode(RModel.Gender),
                            PhoneNumber = HttpUtility.HtmlEncode(RModel.MobileNo),
                            DeliveryAdd = HttpUtility.HtmlEncode(RModel.DeliveryAdd),
                            ImageURL = HttpUtility.HtmlEncode(RModel.ImageURL),
                            Aboutme = HttpUtility.HtmlEncode(RModel.Aboutme)
                        };

                        var result = await userManager.CreateAsync(user, RModel.Password);
                        if (result.Succeeded)
                        {
                            /*await signInManager.SignInAsync(user, false);*/
                            TempData["FlashMessage.Type"] = "success";
                            TempData["FlashMessage.Text"] = string.Format("{0} successfully registered.", RModel.EmailAddress);
                            return RedirectToPage("Login");
                        }
                    }
                    else
                    {
                        TempData["FlashMessage.Type"] = "danger";
                        TempData["FlashMessage.Text"] = string.Format("Password does not meet password complexity requirement. Registration failed, please try again");
                        return Page();
                    }
                }
                /*             foreach (var error in result.Errors)
                             {
                                 ModelState.AddModelError("", error.Description);
                             }*/
            }
            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = string.Format("Error Registration, Try Again.");
            return Page();
        }
    }
}
