using B3110SQLInjectionProjectASPNETCore.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace B3110SQLInjectionProjectASPNETCore.Pages
{
    public class RemoveStudentModel : PageModel
    {
        public string Message { get; set; } = string.Empty;


        [BindProperty]
        [Required(ErrorMessage = "Student ID must not be blank.")]
        [StringLength(10, ErrorMessage = "Student ID must not be more than 10 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Student ID can only contain numbers.")] 
        public string StudentID { get; set; } = string.Empty;
        public void OnGet()
        {
        }
        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                bool Confirmation;
                Student EnrolledStudent = new();
                BCS RequestDirector = new();
                EnrolledStudent = RequestDirector.FindStudent(StudentID);
                if (EnrolledStudent != null)
                {
                    Confirmation = RequestDirector.RemoveStudent(StudentID);
                    if (Confirmation = true)
                        Message = "Record of Student ID " + StudentID + " has been deleted succesfully.";
                    else
                        Message = "Record of Student ID " + StudentID + " was not deleted.";
                }
                else
                    Message = "Record of Student ID " + StudentID + " does not exist.";
            }
        }

    }
}
