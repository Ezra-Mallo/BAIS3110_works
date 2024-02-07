using B3110SQLInjectionProjectASPNETCore.Domain;
using B3110SQLInjectionProjectASPNETCore.TechnicalServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace B3110SQLInjectionProjectASPNETCore.Pages
{
    public class CheckGradeModel : PageModel
    {
        public string Message { get; set; } = string.Empty;
        public bool MessageResponse { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Student ID must not be blank.")]
        [StringLength(10, ErrorMessage = "Student ID must not be more than 10 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Student ID can only contain numbers.")]
        public string StudentIDFind { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select one.")]
        public string TermFind { get; set; } = string.Empty;

        [BindProperty]
        public List<ExamScore> CurrentExamescores { get; set; } = new();
        [BindProperty]
        public Student EnrolledStudent { get; set; } = new();



        public void OnGet()
        {
        }
        
    
    public void OnPost()
        {
            if (StudentIDFind != null)
            {
                if (ModelState.IsValid)
                {

                    BCS RequestDirector = new();
                    EnrolledStudent = RequestDirector.FindStudent(StudentIDFind);
                    if (EnrolledStudent != null)
                    {
                        BCS ExamDirector = new();
                        CurrentExamescores = ExamDirector.FindGradeParametizedStoredProcedure(StudentIDFind, TermFind);
                        if (CurrentExamescores != null)
                        {
                            MessageResponse = true;
                            Message = "Below are your grades.";
                        }
                        else
                        {
                            MessageResponse = false;
                            Message = "Students Exam Records does not exist.";
                        }
                    }
                    else 
                    {
                        MessageResponse = false;
                        Message = "Student profile does not exist.";                   
                    }


                    
                }
            }
        }

    }
}
