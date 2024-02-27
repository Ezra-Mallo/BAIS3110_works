using B3110SQLInjectionProjectASPNETCore.Domain;
using B3110SQLInjectionProjectASPNETCore.TechnicalServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace B3110SQLInjectionProjectASPNETCore.Pages
{
    public class RecordStudentScoreModel : PageModel
    {


        public bool Confirmation { get; set; } = false;
        public bool IsFindButtonDisabled { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public bool ShowUpdateForm { get; set; } = false;

        [BindProperty]
        public string Submit { get; set; } = string.Empty;

        [BindProperty]
        public string FormModifyStudentForm { get; set; }


        [BindProperty]
        [Required(ErrorMessage = "Student ID must not be blank.")]
        [StringLength(10, ErrorMessage = "Student ID must not be more than 10 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Student ID can only contain numbers.")]
        public string StudentIDFind { get; set; } = string.Empty;


        [BindProperty]
        [Required(ErrorMessage = "Course Code must not be blank.")]
        [StringLength(8, ErrorMessage = "Course Code must not be more than 3 characters.")]
        [RegularExpression("^[A-Z]{4}[0-9]{4}$", ErrorMessage = "Course Code format is XXXX9999.")]
        public string CourseCodeFind { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Score must not be blank.")]
        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100.")]
        public int NewScore { get; set; }

        public int KeepScore { get; set; }


        public ExamScore ReturnRegisteredCourse = new();


        public string UserRole { get; set; } = string.Empty;


        
        public void OnGet()
        {
            UserRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;           

        }
        public void OnPost()
        {
            BCS RequestDirector = new();
            switch (Submit)
            {
                case "Find & Update":
                    ModelState.Clear();
                    Message = "*** OnPost *** ---> Find Model state";
                    
                    if (ModelState.IsValid)
                    {
                        
                        ReturnRegisteredCourse = RequestDirector.FindRegisterCourseForGrading(StudentIDFind, CourseCodeFind);

                        if (ReturnRegisteredCourse != null)
                        {
                            IsFindButtonDisabled = true;
                            ShowUpdateForm = true;
                            Message = "Below courses has been registered.";
                            BCS RegisterCourseGrade = new();
                            bool Confirmation;
                            Confirmation = RegisterCourseGrade.UpdateCourseGrading(StudentIDFind, CourseCodeFind, NewScore);                            
                                
                        }
                    
                    
                    } 
                    else  
                    {
                        IsFindButtonDisabled = false;
                        ShowUpdateForm = false;
                        Message = "Records do not exist.";

                    }
                    break;

                case "Close":

                    ModelState.Clear();
                    IsFindButtonDisabled = false;
                    ShowUpdateForm = false;
                    Message = "Records do not exist.";
                    break;
            }
        }
    }
}
