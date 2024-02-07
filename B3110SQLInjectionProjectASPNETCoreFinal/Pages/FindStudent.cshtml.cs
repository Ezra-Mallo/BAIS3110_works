using B3110SQLInjectionProjectASPNETCoreFinal.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace B3110SQLInjectionProjectASPNETCoreFinal.Pages
{
    public class FindStudentModel : PageModel
    {
        public string Message { get; set; } = string.Empty;
        public bool MessageResponse { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Student ID must not be blank.")]
        [StringLength(10, ErrorMessage = "Student ID must not be more than 10 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Student ID can only contain numbers.")] 
        public string StudentID { get; set; } = string.Empty;
        public Student EnrolledStudent { get; set; } = new();
        public void OnGet()
        {
        }
        public void OnPost()
        {
         if(StudentID !=null)
            {
                if (ModelState.IsValid)            
                {
                    BCS RequestDirector = new();
                    EnrolledStudent = RequestDirector.FindStudent(StudentID);
                    if (EnrolledStudent != null)
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
