using B3110SQLInjectionProjectASPNETCore.TechnicalServices;

namespace B3110SQLInjectionProjectASPNETCore.Domain
{
    public class ExamScore
    {
        public string StudentID { get; set; } = string.Empty;
        public string ProgramCode { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public string MajorCode { get; set; } = string.Empty;
        public string Term { get; set; } = string.Empty;
        public int  Semester { get; set; } 
        public int Scores { get; set; } 

    }
}
