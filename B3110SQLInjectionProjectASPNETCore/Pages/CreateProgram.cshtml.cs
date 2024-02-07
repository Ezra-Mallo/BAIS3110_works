using B3110SQLInjectionProjectASPNETCore.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace B3110SQLInjectionProjectASPNETCore.Pages
{
    public class CreateProgramModel : PageModel
    {

        public string Message { get; set; } = string.Empty;
        public bool MessageResponse { get; set; }


        [BindProperty]

        public string ProgramCode { get; set; } = string.Empty;


        [BindProperty]
        public string Description { get; set; } = string.Empty;

        public void OnGet()
        {
        }
        public void OnPost()
        {

            if (ProgramCode == null || (ProgramCode.Length == 0))
            {
                ModelState.AddModelError("ProgramCode", "Program Code must not be blank. ");
            }
            if (ProgramCode.Length > 10)
            {
                ModelState.AddModelError("ProgramCode", "Program Code must be a maximum of 10 characters.");
            }

            if (Description == null || (Description.Length == 0))
            {
                ModelState.AddModelError("Description", "Description must not be blank. ");
            }
            if (Description.Length > 60)
            {
                ModelState.AddModelError("Description", "Description must be a maximum of 60 characters.");
            }




            if (ModelState.IsValid)
            {
                bool Confirmation;
                BCS RequestDirector = new();
                Confirmation = RequestDirector.CreateProgram(ProgramCode, Description);
                if (Confirmation)
                {
                    Message = "Program created succesfully.";
                    MessageResponse = true;
                    ModelState.Clear();
                }

                else
                {
                    Message = "Program was not created .";
                    MessageResponse = false;
                }

            }            
        }
    }
}
