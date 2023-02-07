using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FreshFarmMarket.Model
{
	public class ApplicationUser : IdentityUser
	{
        [Required, MinLength(3, ErrorMessage = "Enter at least 3 characters for full name")]
        [Display(Name = "Full name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Credit card number")]
        public string CreditCard { get; set; }

        [Required, MaxLength(1)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

/*        [Required(ErrorMessage = "Mobile number cannot be empty"), MinLength(8, ErrorMessage = "Please enter 8 digits for mobile number"), MaxLength(8, ErrorMessage = "Please only enter 8 digits for mobile number")]
        [Display(Name = "Mobile number")]
        public string MobileNo { get; set; }*/

        [Required]
        [Display(Name = "Delivery address")]
        public string DeliveryAdd { get; set; }
        /*
                [Required(ErrorMessage = "Email address cannot be empty")]
                [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a proper email address")]
                [Display(Name = "Email address")]
                [DataType(DataType.EmailAddress)]
                public string EmailAddress { get; set; }

                [Required(ErrorMessage = "Password cannot be empty")]
                [MinLength(12, ErrorMessage = "Please enter at least 12 characters with at least 1 Upper case, 1 lower case, 1 number and 1 special case")]
                //  [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{12,}$", ErrorMessage = "Password must be at least 12 characters long and contain at least an upper case, lower case, number and symbol.")]
                [Display(Name = "Password")]
                *//* [DataType(DataType.Password)]*//*
                public string Password { get; set; }

                [Required]
                *//*    [DataType(DataType.Password)]*//*
                [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
                [Display(Name = "Confirm password")]
                public string ConfirmPassword { get; set; }*/

        public string? ImageURL { get; set; }

        [Required, Display(Name = "About me", Prompt = "About me..."), MaxLength(1000)]
        public string Aboutme { get; set; }
    }
}
