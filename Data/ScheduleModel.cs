namespace class_organizer.Data
{
    public class ScheduleModel
    {
        public ClassModel Class { get; set; }
        public TeacherModel Teacher { get; set; }
        public StudentModel Student { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}