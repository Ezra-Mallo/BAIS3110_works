using B3110SQLInjectionProjectASPNETCoreFinal.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace B3110SQLInjectionProjectASPNETCoreFinal.Pages
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
