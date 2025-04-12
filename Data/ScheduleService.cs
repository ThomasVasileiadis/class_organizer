using System;
using System.Collections.Generic;
using System.Linq;

namespace class_organizer.Data
{
    public class ScheduleService
    {
        private readonly StudentService _studentService;
        private readonly TeacherService _teacherService;
        private readonly ClassService _classService;

        public ScheduleService(StudentService studentService, TeacherService teacherService, ClassService classService)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _classService = classService;
        }

        public List<ScheduleModel> GenerateWeeklySchedule()
        {
            var schedule = new List<ScheduleModel>();
            var timeSlots = GetTimeSlots();
            var daysOfWeek = Enum.GetValues(typeof(DayOfWeek))
                .Cast<DayOfWeek>()
                .Where(d => d != DayOfWeek.Saturday && d != DayOfWeek.Sunday)
                .ToList();

            var classes = _classService.GetClasses();
            var teachers = _teacherService.GetTeachers();
            var students = _studentService.GetStudents();

            var classIndex = 0;
            foreach (var day in daysOfWeek)
            {
                foreach (var timeSlot in timeSlots)
                {
                    if (classIndex >= classes.Count)
                    {
                        break; // No more classes to schedule
                    }

                    var currentClass = classes[classIndex];
                    var availableTeachers = teachers
                        .Where(t => t.CanTeachClass(currentClass.Name) && !IsTeacherBooked(schedule, day, timeSlot, t.Id))
                        .ToList();

                    if (availableTeachers.Any())
                    {
                        var teacher = availableTeachers.First(); // Simple strategy: pick the first available teacher
                        var enrolledStudents = students
                            .Where(s => s.ClassId == currentClass.Id && !IsStudentBooked(schedule, day, timeSlot, s.Id))
                            .ToList();

                        if (enrolledStudents.Any())
                        {
                            schedule.Add(new ScheduleModel
                            {
                                Class = currentClass,
                                Teacher = teacher,
                                StartTime = DateTime.Today.Add(timeSlot.startTime.ToTimeSpan()),
                                EndTime = DateTime.Today.Add(timeSlot.endTime.ToTimeSpan()),
                                Students = enrolledStudents
                            });
                        }
                    }

                    classIndex++;
                }
            }

            return schedule;
        }

        private bool IsTeacherBooked(List<ScheduleModel> schedule, DayOfWeek day, (TimeOnly startTime, TimeOnly endTime) timeSlot, int teacherId)
        {
            return schedule.Any(s =>
                s.Teacher.Id == teacherId &&
                s.StartTime.DayOfWeek == day &&
                ((timeSlot.startTime >= TimeOnly.FromDateTime(s.StartTime) && timeSlot.startTime < TimeOnly.FromDateTime(s.EndTime)) ||
                 (timeSlot.endTime > TimeOnly.FromDateTime(s.StartTime) && timeSlot.endTime <= TimeOnly.FromDateTime(s.EndTime)) ||
                 (TimeOnly.FromDateTime(s.StartTime) >= timeSlot.startTime && TimeOnly.FromDateTime(s.StartTime) < timeSlot.endTime) ||
                 (TimeOnly.FromDateTime(s.EndTime) > timeSlot.startTime && TimeOnly.FromDateTime(s.EndTime) <= timeSlot.endTime)));
        }

        private bool IsStudentBooked(List<ScheduleModel> schedule, DayOfWeek day, (TimeOnly startTime, TimeOnly endTime) timeSlot, int studentId)
        {
            return schedule.Any(s =>
                s.Students.Any(student => student.Id == studentId) &&
                s.StartTime.DayOfWeek == day &&
                ((timeSlot.startTime >= TimeOnly.FromDateTime(s.StartTime) && timeSlot.startTime < TimeOnly.FromDateTime(s.EndTime)) ||
                 (timeSlot.endTime > TimeOnly.FromDateTime(s.StartTime) && timeSlot.endTime <= TimeOnly.FromDateTime(s.EndTime)) ||
                 (TimeOnly.FromDateTime(s.StartTime) >= timeSlot.startTime && TimeOnly.FromDateTime(s.StartTime) < timeSlot.endTime) ||
                 (TimeOnly.FromDateTime(s.EndTime) > timeSlot.startTime && TimeOnly.FromDateTime(s.EndTime) <= timeSlot.endTime)));
        }

        private List<(TimeOnly startTime, TimeOnly endTime)> GetTimeSlots()
        {
            return new List<(TimeOnly, TimeOnly)>
            {
                (new TimeOnly(9, 0), new TimeOnly(10, 0)),
                (new TimeOnly(10, 0), new TimeOnly(11, 0)),
                (new TimeOnly(11, 0), new TimeOnly(12, 0)),
                (new TimeOnly(13, 0), new TimeOnly(14, 0)),
                (new TimeOnly(14, 0), new TimeOnly(15, 0)),
                (new TimeOnly(15, 0), new TimeOnly(16, 0)),
                (new TimeOnly(16, 0), new TimeOnly(17, 0))
            };
        }
    }
}