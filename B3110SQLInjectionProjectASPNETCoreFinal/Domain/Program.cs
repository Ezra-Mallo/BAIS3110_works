using Microsoft.AspNetCore.Http.HttpResults;

namespace B3110SQLInjectionProjectASPNETCoreFinal.Domain
{
    public class Program
    {
        private List<Student> _enrolledStudents = new();        // or new List<Student>();


        public string ProgramCode { get; set; } = string.Empty;


        

        public string Description { get; set; } = string.Empty;
        public List<Student> EnrolledStudents
        {
            get { return _enrolledStudents; }
        }
    }
}
