namespace class_organizer.Data
{
    public class TeacherModel
    {
        public int teacher_id { get; set; }
        public string? teacher_name { get; set; }
        public string? teacher_lastname { get; set; }
        public string? teacher_email { get; set; }
        public int? teacher_phone { get; set; }
        public string? teacher_working_days { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Add this method
        public bool CanTeachClass(string className)
        {
            // Logic to determine if the teacher can teach the class
            // For now, assume all teachers can teach all classes
            return true;
        }
    }
}
