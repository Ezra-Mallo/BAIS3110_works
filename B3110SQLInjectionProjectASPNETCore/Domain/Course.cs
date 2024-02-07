namespace B3110SQLInjectionProjectASPNETCore.Domain
{
    public class Course
    {
        public string CourseCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ProgramCode { get; set; } = string.Empty;
        public string MajorCode { get; set; } = string.Empty;
        public int Credit { get; set; } 
        public string Term { get; set; } = string.Empty;
        public int  Semester { get; set; } 
    }
}
