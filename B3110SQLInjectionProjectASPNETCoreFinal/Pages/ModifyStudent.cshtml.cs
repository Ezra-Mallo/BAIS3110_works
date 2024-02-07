using B3110SQLInjectionProjectASPNETCoreFinal.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
using B3110SQLInjectionProjectASPNETCoreFinal.TechnicalServices;
using System.ComponentModel.DataAnnotations;

namespace B3110SQLInjectionProjectASPNETCoreFinal.Pages
{
    public class ModifyStudentModel : PageModel
    {
        public bool Confirmation { get; set; } = false;
        public bool IsFindButtonDisabled { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public Student EnrolledStudentFound { get; set; } = new();
        public bool ShowUpdateForm { get; set; } = false;


        [BindProperty]
        [Required(ErrorMessage = "Student ID must not be blank.")]
        [StringLength(10, ErrorMessage = "Student ID must not be more than 10 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Student ID can only contain numbers.")] 
        public string StudentIDFind { get; set; } = string.Empty;


        [BindProperty]
        public string StudentID { get; set; } = string.Empty;

        [BindProperty]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string LastName { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string ProgramCode { get; set; } = string.Empty;

        [BindProperty]
        public string Term { get; set; } = string.Empty;

        [BindProperty]
        public string MajorCode { get; set; } = string.Empty;
        [BindProperty]
        public string Submit { get; set; } = string.Empty;



        public void OnGet()
        {
        }


        public void OnPost()
        {
            BCS RequestDirector = new();
            switch (Submit)
            {
                case "Find":
                    ModelState.Clear();
                    Message = "*** OnPost *** ---> Find Model state";
                    if (string.IsNullOrEmpty(StudentIDFind))
                    {
                        ModelState.AddModelError("StudentIDFind", "Student ID must not be blank.");
                    }
                    else if (StudentIDFind.Length > 10)
                    {
                        ModelState.AddModelError("StudentIDFind", "Student ID must be a maximum of 10 characters.");
                    }

                    if (ModelState.IsValid)
                    {
                        EnrolledStudentFound = RequestDirector.FindStudent(StudentIDFind);
                        if (EnrolledStudentFound != null)
                        {
                            Message = "*** OnPost *** ---> Found";
                            IsFindButtonDisabled = true;
                            ShowUpdateForm = true;
                            Message = "Below are the details of the student.";

                        }
                    }
                    else
                    {
                        IsFindButtonDisabled = false;
                        ShowUpdateForm = false;
                        Message = "Records do not exist.";

                    }
                    ModelState.Clear();
                    break;
                case "Modify":
                    ModelState.Clear();
                    if (string.IsNullOrEmpty(FirstName))
                    {
                        ModelState.AddModelError("FirstName", "First Name must not be blank.");
                    }

                    if (string.IsNullOrEmpty(LastName))
                    {
                        ModelState.AddModelError("LastName", "Last Name must not be blank.");
                    }

                    Regex emailRegex = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

                    if (Email != null && !emailRegex.IsMatch(Email))
                    {
                        ModelState.AddModelError("Email", "Enter a valid Email.");
                    }

                    if (ModelState.IsValid)
                    {

                        EnrolledStudentFound.StudentID = StudentID;
                        EnrolledStudentFound.FirstName = FirstName;
                        EnrolledStudentFound.LastName = LastName;
                        EnrolledStudentFound.Email = Email;
                        EnrolledStudentFound.Email = ProgramCode;
                        EnrolledStudentFound.Email = Term;
                        EnrolledStudentFound.Email = MajorCode;


                        //BCS RequestDirector = new(); 
                        Confirmation = RequestDirector.ModifyStudent(EnrolledStudentFound);
                        if (Confirmation == true)
                        {
                            ShowUpdateForm = false;
                            IsFindButtonDisabled = false;
                            Message = "Student record updated succesfully.";
                        }
                        else
                        {
                            ShowUpdateForm = true;
                            IsFindButtonDisabled = true;
                            Message = "Student record was not updated.";

                        }

                    }
                    break;
            }
        }

    }
}
