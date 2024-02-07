using static System.Runtime.InteropServices.JavaScript.JSType;

namespace B3110SQLInjectionProjectASPNETCore.Domain
{
    public class Student
    {
        private string _studentID = "";
        private string _firstName = string.Empty;

        public string StudentID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ProgramCode{ get; set; } = string.Empty;
        //public required Date  YearOfAdmission { get; set; } 
        public string Term { get; set; } = string.Empty;
        public string MajorCode { get; set; } = string.Empty;


    }
}
