using FreshFarmMarket.Model;
using FreshFarmMarket.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FreshFarmMarket.Pages
{
    public class UserHomeModel : PageModel
    {
        public Register DModel { get; set; }
        private ApplicationUser all { get; }

        private UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public UserHomeModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            /*            var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                        var protector = dataProtectionProvider.CreateProtector("MySecretKey");

                        var user = userManager.FindByEmailAsync(LModel.EmailAddress);*/

            /*            var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                        var protector = dataProtectionProvider.CreateProtector("MySecretKey");


                        var user = await userManager.GetUserAsync(User);
                        protector.Unprotect(user.CreditCard);*/
/*            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("SSName")))
            {
                HttpContext.Session.GetString("SSName");
            }*/
            return Page();
        }


    }
}

