using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using B3110SQLInjectionProjectASPNETCoreFinal.Domain;
using System.Text.RegularExpressions;
using B3110SQLInjectionProjectASPNETCoreFinal.TechnicalServices;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;


namespace B3110SQLInjectionProjectASPNETCoreFinal.Pages
{
    public class RegisterCoursesModel : PageModel
    {
        public bool Confirmation { get; set; } = false;
        public bool IsFindButtonDisabled { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public Student EnrolledStudentFound { get; set; } = new();
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
        [Required(ErrorMessage = "Student ID must not be blank.")]
        public int SemesterFind { get; set; }

        
        [BindProperty]
        public string StudentID { get; set; } = string.Empty;

        [BindProperty]
        public string ProgramCode { get; set; } = string.Empty;

        [BindProperty]
        public string CourseCode { get; set; } = string.Empty;

        [BindProperty]
        public string  MajorCode { get; set; } = string.Empty;

        [BindProperty]
        public string Term { get; set; } = string.Empty;

        [BindProperty]
        public int Semester { get; set; }


        [BindProperty]
        public int Scores { get; set; } = 0;
        public List<Course> CreditCourses { get; set; }
        public List<Course> CreditCourses2 { get; set; }

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

                    if (SemesterFind == 0)
                    {
                        ModelState.AddModelError("SemesterFind", "Student ID must not be blank.");
                    }


                    if (ModelState.IsValid)
                    {
                        EnrolledStudentFound = RequestDirector.FindStudent(StudentIDFind);

                        if (EnrolledStudentFound != null)
                        {
                            BCS CourseDirector = new();
                            BCS RegisterCourse = new();
                            CreditCourses = CourseDirector.FindCourse(EnrolledStudentFound.ProgramCode, EnrolledStudentFound.MajorCode, EnrolledStudentFound.Term, SemesterFind);
                            if (CreditCourses != null)
                            {
                                ExamScore examScore = new();


                                foreach (var course in CreditCourses)
                                {
                                    examScore = new()
                                    {
                                        StudentID = StudentIDFind,
                                        ProgramCode = course.ProgramCode,
                                        CourseCode = course.CourseCode,
                                        MajorCode = course.MajorCode,
                                        Term = course.Term,
                                        Semester = course.Semester,
                                        Scores = Scores
                                    };
                     
                                    Confirmation = RegisterCourse.RegisterCredit(examScore);
                                    IsFindButtonDisabled = true;
                                    ShowUpdateForm = true;
                                    Message = "Below courses has been registered.";
                                }
                            }
                        }
                        else
                        {
                            IsFindButtonDisabled = false;
                            ShowUpdateForm = false;
                            Message = "Records do not exist.";

                        }
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
