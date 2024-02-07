using B3110SQLInjectionProjectASPNETCore.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace B3110SQLInjectionProjectASPNETCore.Pages
{
    public class FindProgramsModel : PageModel
    {

        public List<Domain.Program> allPrograms { get; set; } = new();
                   
        public void OnGet()
        {
            BCS Requestdirector = new();
            allPrograms = Requestdirector.FindPrograms();
        }

        public void OnPost() 
        {


        }
    }
}
