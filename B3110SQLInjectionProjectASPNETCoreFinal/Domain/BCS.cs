using B3110SQLInjectionProjectASPNETCoreFinal.TechnicalServices;

namespace B3110SQLInjectionProjectASPNETCoreFinal.Domain
{
    public class BCS
    {
        public bool CreateProgram(string programCode, string description)
        {
            bool Confirmation;
            Programs ProgramManager = new();

            Confirmation = ProgramManager.AddProgram(programCode, description);

            return Confirmation;
        }


        public bool RegisterCredit(ExamScore registerCredit)
        {
            bool Confirmation;
            ExamScores CreditDirector = new();
            Confirmation=CreditDirector.RegisterExamScore(registerCredit);
            return Confirmation;
        }


        public bool EnrollStudent(Student acceptedStudent, string programCode)
        {
            bool Confirmation;
            Students StudentManager = new();

            Confirmation = StudentManager.AddStudent(acceptedStudent, programCode);

            return Confirmation;
        }
        public Domain.Program FindProgram(string programCode)
        {
            Programs ProgramManager = new();
            Domain.Program ActiveProgram = ProgramManager.GetProgram(programCode);  // getter/Setter construct
            return ActiveProgram;
        }
        public List<Domain.Program> FindPrograms()
        {
            Programs ProgramManager = new();
            List<Domain.Program> allProgram = ProgramManager.GetPrograms();  // getter/Setter construct
            return allProgram;
        }
        public Student FindStudent(string studentID)
        {
            Students StudentManager = new();
            Student Enrolledstudent = StudentManager.GetStudent(studentID);  //student = student getter/Setter construct
            return Enrolledstudent;
        }

        public bool RemoveStudent(string studentID)
        {
            bool Success = false;

            Students StudentManager = new();
            Success = StudentManager.DeleteStudent(studentID);
            return Success;
        }
        public bool ModifyStudent(Student enrolledStudent)
        {
            bool Confirmation;

            Students StudentManager = new();
            Confirmation = StudentManager.UpdateStudent(enrolledStudent);  // getter/Setter construct
            return Confirmation;
        }


        public List<Course> FindCourse(string programCode, string majorCode, string term, int semester)
        {
            
            Courses  CourseManager = new();
            List<Course> CreditCourses = CourseManager.GetCourse(programCode, majorCode, term, semester);  // getter/Setter construct
            return CreditCourses;
        }

        public List<ExamScore> FindGradeParametizedStoredProcedure(string studentID, string term)
        {
            List<ExamScore> AllExamScores = new();
            ExamScores ExamManager = new();
            AllExamScores = ExamManager.CheckGradeParametizedStoredProcedure(studentID, term);

            return AllExamScores;

        }




        public List<ExamScore> FindGradeVulnerableAdapter(string studentID, string term)
        {
            List<ExamScore> AllExamScores = new();
            ExamScores ExamManager = new();
            AllExamScores = ExamManager.CheckGradeVulnerableAdapter(studentID, term);

            return AllExamScores;

        }
    }
}
