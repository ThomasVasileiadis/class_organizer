namespace class_organizer.Data
{
    public class ScheduleModel
    {
        public ClassModel Class { get; set; } = new();
        public TeacherModel Teacher { get; set; } = new();
        public List<StudentModel> Students { get; set; } = new();
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}