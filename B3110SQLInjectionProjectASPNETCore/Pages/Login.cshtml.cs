using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using B3110SQLInjectionProjectASPNETCore.Domain;
using B3110SQLInjectionProjectASPNETCore.TechnicalServices;


namespace B3110SQLInjectionProjectASPNETCore.Pages
{
    public class LoginModel : PageModel
    {


        private string _userName { set; get; } = string.Empty;
        private string _password { set; get; } = string.Empty;



        public string Message { set; get; } = string.Empty;

        [Required]
        [StringLength(10, ErrorMessage = "Not more than 10 characters")]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "Only Alphabets and Numbers are allowerd.")]
        [BindProperty]
        public string Username
        {
            get { return _userName; }
            set { _userName = value; }
        }
        

        [Required]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "Not more than 10 characters")]
        [BindProperty]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }


        

        public UserProfile CurrentUserProfile = new();
        public string UserRole { get; set; } = string.Empty;


        public void OnGet()
        {
            UserRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                
                BCS ProfileDirecrtor = new();
                CurrentUserProfile = ProfileDirecrtor.checkPassword(Username, Password);

                if (CurrentUserProfile != null)
                {


                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, CurrentUserProfile.Username),
                            new Claim(ClaimTypes.Email, CurrentUserProfile.Email),                            
                        };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, CurrentUserProfile.Role));
                
                    AuthenticationProperties authProperties = new AuthenticationProperties
                    {
                        //Refreshing the authentication session should be allowed.
                        AllowRefresh = true,

                        // Set the time at which the authentication ticket expires. A value set here overrides the
                        // ExpireTimeSpan option of CookieAuthenticationOptions set with AddCookie.
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),

                        // Persist the session across requests. This sets whether the authentication session is persisted across
                        // multiple requests. When used with cookies, controls whether the cookie's lifetime is absolute
                        // (matching the lifetime of the authentication ticket) or session‐based.
                        IsPersistent = true,


                        // // Set the time of issuance. The time at which the authentication ticket was issued.
                        IssuedUtc = DateTimeOffset.UtcNow,

                        // Set the path to redirect URI after successful login.  The full path or absolute URI to be used
                        // as an http redirect response value.
                        RedirectUri = "/Admin/Index?UserRole=" + CurrentUserProfile.Role
                    };

                    // Set the UserRole property
                    UserRole = CurrentUserProfile.Role;
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToPage("/Admin/Index", new { UserRole = CurrentUserProfile.Role });
                }
            }

            Message = "Invalid attempt";
            return Page();
        }


    }
}
