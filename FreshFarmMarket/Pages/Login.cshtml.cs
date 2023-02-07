using AspNetCore.ReCaptcha;
using FreshFarmMarket.Model;
using FreshFarmMarket.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreshFarmMarket.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }

        private UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public const string SessionKeyName = "_Name";
        private readonly ILogger<LoginModel> logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<LoginModel> logger)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.logger = logger;
        }



        public void OnGet()
        {
        }

        [ValidateReCaptcha]
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(LModel.EmailAddress, LModel.Password,
                LModel.RememberMe, true);

                var user = await userManager.FindByEmailAsync(LModel.EmailAddress);
                if (identityResult.Succeeded)
                {
                    if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
                    {
                        HttpContext.Session.SetString(SessionKeyName, LModel.EmailAddress);
                    }
                    var name = HttpContext.Session.GetString(SessionKeyName);
                    logger.LogInformation("Session Name: {0}", name);
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("Hello {0}! Logged in.", user.Name);
                    return RedirectToPage("UserHome");
                }
                if (identityResult.IsLockedOut)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("User {0} has reached max attempt. Account is locked out.", user.Email);
                    return Page();
                }
            }
            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = string.Format("Username or Password incorrect");
            return Page();
        }
    }
}
