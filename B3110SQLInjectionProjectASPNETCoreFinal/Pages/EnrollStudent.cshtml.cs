using B3110SQLInjectionProjectASPNETCoreFinal.Domain;
using B3110SQLInjectionProjectASPNETCoreFinal.TechnicalServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations; 

namespace B3110SQLInjectionProjectASPNETCoreFinal.Pages
{
    public class EnrollStudentModel : PageModel
    {


       
        public string Message { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Student ID must not be blank.")]
        [StringLength(10, ErrorMessage = "Student ID must not be more than 10 characters.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Student ID can only contain numbers.")]

        public string StudentID { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "First Name must not be blank.")]
        [StringLength(25, ErrorMessage = "First Name must not be more than 25 characters.")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "First Name  can only contain alphabetical characters.")]

        public string FirstName { get; set; } = string.Empty;


        [BindProperty]
        [Required(ErrorMessage = "Last Name must not be blank.")]
        [StringLength(25, ErrorMessage = "Last Name must not be more than 25 characters.")]
        [RegularExpression("^[A-Za-z]*$", ErrorMessage = "Last Name  can only contain alphabetical characters.")] 
        public string LastName { get; set; } = string.Empty;


        [BindProperty]
        [StringLength(50, ErrorMessage = "Email must not be more than 50 characters.")]
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",ErrorMessage = "Enter a valid Email.")]
        public string Email { get; set; } = string.Empty;


        [BindProperty]
        [Required(ErrorMessage = "Program Code must not be blank.")]
        public string ProgramCode { get; set; } = string.Empty;

        

        [BindProperty]
        [Required(ErrorMessage = "Please select one.")]
        public string Term {  get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter your Major Code.")]
        public string MajorCode {  get; set; }

        public List<string> ProgramCodeList { get; set; } 
        public List<string> MajorCodeList { get; set; } 



        public void OnGet()
        {
            Programs ProgramCodeHandler = new();
            ProgramCodeList = ProgramCodeHandler.GetProgramCodes();

            TechnicalServices.Major MajorCodeHandler = new();
            MajorCodeList = MajorCodeHandler.GetMajorCodes();
            Message = "On Get";
            

        }

        public void OnPost()
        {
            Message = "On Post";
            if (ProgramCode != "-- Select Program Code --" && MajorCode != "-- Select Your Major --")
            {
                if (ModelState.IsValid)
                {
                    Student AcceptedStudent = new()
                    {
                        StudentID = StudentID,
                        FirstName = FirstName,
                        LastName = LastName,
                        Email = Email,
                        ProgramCode = ProgramCode,
                        Term = Term,
                        MajorCode = MajorCode
                    };

                    bool Confirmation;
                    BCS RequestDirector = new();
                    Confirmation = RequestDirector.EnrollStudent(AcceptedStudent, ProgramCode);
                    if (Confirmation)
                    {
                        Message = "Enrolling Student was successful.";
                    }
                    else
                    {
                        Message = "Enrolling Student was not successful.";
                    }

                }
            }
            else
            {
                Programs ProgramCodeHandler = new();
                ProgramCodeList = ProgramCodeHandler.GetProgramCodes();


                TechnicalServices.Major MajorCodeHandler = new();
                MajorCodeList = MajorCodeHandler.GetMajorCodes();
                Message = "On Get";
                ModelState.Clear();


            }
        }
            

    }
}
