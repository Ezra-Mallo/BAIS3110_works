using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace B3110SQLInjectionProjectASPNETCore.Pages
{
    public class CreateMajorModel : PageModel
    {
        public string UserRole { get; set; } = string.Empty;


        public void OnGet()
        {
            UserRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        }
    }
}
