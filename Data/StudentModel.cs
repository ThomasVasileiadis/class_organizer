namespace class_organizer.Data
{
    public class StudentModel
    {
        public int student_id { get; set; }
        public string? student_name { get; set; }
        public string? student_lastname { get; set; }
        public string? student_email { get; set; }
        public int? student_phone { get; set; }
        public string? student_obligation { get; set; }
        public string? student_level { get; set; }

        // Add these properties
        public int ClassId { get; set; }
        public int Id { get; set; }
    }
}
