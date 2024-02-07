using B3110SQLInjectionProjectASPNETCore.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace B3110SQLInjectionProjectASPNETCore.Pages
{
    public class FindProgramModel : PageModel
    {
        public string Message { get; set; } = string.Empty;
        public bool MessageResponse { get; set; } = false;

        [BindProperty]
        [Required]
        public string ProgramCode { get; set; } = string.Empty;
        public Domain.Program ActiveProgram { get; set; } = new();


        public void OnPost()
        {
            if (ProgramCode != null)
            {
                if (ModelState.IsValid)
                {

                    BCS Requestdirector = new();
                    ActiveProgram = Requestdirector.FindProgram(ProgramCode);
                    if (ActiveProgram != null)
                    {
                        MessageResponse = true;
                        Message = "Below are the details of the student.";
                    }
                    else
                    {
                        MessageResponse = false;
                        Message = "Records does not exist.";
                    }
                }
            }
            
        }
    }
}
