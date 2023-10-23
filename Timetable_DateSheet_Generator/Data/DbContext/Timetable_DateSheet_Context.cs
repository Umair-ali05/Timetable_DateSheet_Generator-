using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Data.DbContext
{
    public class Timetable_DateSheet_Context : IdentityDbContext<ApplicationUser>
    {
        public Timetable_DateSheet_Context(DbContextOptions<Timetable_DateSheet_Context> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Students>().HasAlternateKey(a => new { a.StudentEnrollment });
            builder.Entity<FacultyMembers>().HasOne(a => a.Department).WithMany(b => b.FacultyMembers).HasForeignKey(f => f.DepartmentID).HasPrincipalKey(p => p.DepartmentID);
            builder.Entity<Students>().HasOne(s => s.Program).WithMany(b => b.Students).HasForeignKey(f => f.ProgarmID).HasPrincipalKey(p => p.ProgramID);
            builder.Entity<Buildings>().HasOne(s => s.Institute).WithMany(b => b.Buildings).HasForeignKey(f => f.InstituteID).HasPrincipalKey(p => p.InstituteID);
            builder.Entity<Departments>().HasOne(s => s.Institute).WithMany(b => b.Departments).HasForeignKey(f => f.InstituteID).HasPrincipalKey(p => p.InstituteID);
            builder.Entity<Programs>().HasOne(s => s.Department).WithMany(b => b.Programs).HasForeignKey(f => f.DepartmentID).HasPrincipalKey(p => p.DepartmentID);
            builder.Entity<OfferedCourses>().HasOne(s => s.Department).WithMany(b => b.OfferedCourses).HasForeignKey(f => f.DepartmentID).HasPrincipalKey(p => p.DepartmentID);
            builder.Entity<OfferedCourses>().HasOne(s => s.Semester).WithMany(b => b.OfferedCourses).HasForeignKey(f => f.SemesterID).HasPrincipalKey(p => p.SemesterID);
            builder.Entity<OfferedCourses>().HasOne(s => s.FacultyMember).WithMany(b => b.OfferedCourses).HasForeignKey(f => f.FacultyMemberID).HasPrincipalKey(p => p.FacultyMemberID);
            builder.Entity<OfferedCourses>().HasOne(s => s.Program).WithMany(b => b.OfferedCourses).HasForeignKey(f => f.ProgramID).HasPrincipalKey(p => p.ProgramID);
            builder.Entity<RegisteredCourses>().HasOne(s => s.Student).WithMany(b => b.RegisteredCourses).HasForeignKey(f => f.StudentID).HasPrincipalKey(p => p.StudentID);
            builder.Entity<RegisteredCourses>().HasOne(s => s.OfferedCourse).WithMany(b => b.RegisteredCourses).HasForeignKey(f => f.OfferedCourseID).HasPrincipalKey(p => p.OfferedCourseID);
            builder.Entity<Rooms>().HasOne(s => s.Building).WithMany(b => b.Rooms).HasForeignKey(f => f.BuildingID).HasPrincipalKey(p => p.BuildingID);
            builder.Entity<TimeTables>().HasOne(s => s.Institute).WithMany(b => b.TimeTables).HasForeignKey(f => f.InstituteID).HasPrincipalKey(p => p.InstituteID);
            builder.Entity<TimeTables>().HasOne(s => s.Semester).WithMany(b => b.TimeTables).HasForeignKey(f => f.SemesterID).HasPrincipalKey(p => p.SemesterID);
            builder.Entity<TimeSlots>().HasOne(s => s.Time).WithMany(b => b.TimeSlots).HasForeignKey(f => f.TimeID).HasPrincipalKey(p => p.TimeID);
            builder.Entity<TimeSlots>().HasOne(s => s.TimeTable).WithMany(b => b.TimeSlots).HasForeignKey(f => f.TimeTableID).HasPrincipalKey(p => p.TimeTableID);
            builder.Entity<OfferedCourseTimeSlots>().HasOne(s => s.OfferedCourse).WithMany(b => b.OfferedCourseTimeSlots).HasForeignKey(f => f.OfferedCourseID).HasPrincipalKey(p => p.OfferedCourseID);
            builder.Entity<OfferedCourseTimeSlots>().HasOne(s => s.TimeSlots).WithMany(b => b.OfferedCourseTimeSlots).HasForeignKey(f => f.TimeSlotID).HasPrincipalKey(p => p.TimeSlotID);
            builder.Entity<OfferedCourseTimeSlots>().HasOne(s => s.Room).WithMany(b => b.OfferedCourseTimeSlots).HasForeignKey(f => f.RoomID).HasPrincipalKey(p => p.RoomID);
            builder.Entity<ProgramRegularTimings>().HasOne(s => s.Program).WithMany(b => b.ProgramRegularTimings).HasForeignKey(f => f.ProgramID).HasPrincipalKey(p => p.ProgramID);
            builder.Entity<ProgramRegularTimings>().HasOne(s => s.Time).WithMany(b => b.ProgramRegularTimings).HasForeignKey(f => f.TimeID).HasPrincipalKey(p => p.TimeID);
            builder.Entity<ProgramSpecialTimings>().HasOne(s => s.Program).WithMany(b => b.ProgramSpecialTimings).HasForeignKey(f => f.ProgramID).HasPrincipalKey(p => p.ProgramID);
            builder.Entity<ProgramSpecialTimings>().HasOne(s => s.Time).WithMany(b => b.ProgramSpecialTimings).HasForeignKey(f => f.TimeID).HasPrincipalKey(p => p.TimeID);
            builder.Entity<RoomAvailibilities>().HasOne(s => s.Department).WithMany(b => b.RoomAvailibilities).HasForeignKey(f => f.DepartmentID).HasPrincipalKey(p => p.DepartmentID);
            builder.Entity<RoomAvailibilities>().HasOne(s => s.Room).WithMany(b => b.RoomAvailibilities).HasForeignKey(f => f.RoomID).HasPrincipalKey(p => p.RoomID);
            builder.Entity<RoomAvailibilities>().HasOne(s => s.Time).WithMany(b => b.RoomAvailibilities).HasForeignKey(f => f.TimeID).HasPrincipalKey(p => p.TimeID);
            builder.Entity<FacultyMemberAvailabilities>().HasOne(s => s.FacultyMember).WithMany(b => b.FacultyMemberAvailabilities).HasForeignKey(f => f.FacultyMemberID).HasPrincipalKey(p => p.FacultyMemberID);
            builder.Entity<FacultyMemberAvailabilities>().HasOne(s => s.Time).WithMany(b => b.FacultyMemberAvailabilities).HasForeignKey(f => f.TimeID).HasPrincipalKey(p => p.TimeID);
            builder.Entity<Attendance>().HasOne(s => s.OfferedCourse).WithMany(b => b.Attendances).HasForeignKey(f => f.OfferedCourseID).HasPrincipalKey(p => p.OfferedCourseID);
            builder.Entity<StudentAttendance>().HasOne(s => s.RegisteredCourse).WithMany(b => b.StudentAttendances).HasForeignKey(f => f.RegisteredCourseID).HasPrincipalKey(p => p.RegisteredCourseID);
            builder.Entity<StudentAttendance>().HasOne(s => s.Attendance).WithMany(b => b.StudentAttendances).HasForeignKey(f => f.AttendanceID).HasPrincipalKey(p => p.AttendanceID);
            
        }
        public virtual DbSet<Buildings> Buildings { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<FacultyMemberAvailabilities> FacultyMemberAvailabilities { get; set; }
        public virtual DbSet<FacultyMembers> FacultyMembers { get; set; }
        public virtual DbSet<Institutes> Institutes { get; set; }
        public virtual DbSet<OfferedCourses> OfferedCourses { get; set; }
        public virtual DbSet<OfferedCourseTimeSlots> OfferedCourseTimeSlots { get; set; }
        public virtual DbSet<ProgramRegularTimings> ProgramRegularTimings { get; set; }
        public virtual DbSet<Programs> Programs { get; set; }
        public virtual DbSet<ProgramSpecialTimings> ProgramSpecialTimings { get; set; }
        public virtual DbSet<RegisteredCourses> RegisteredCourses { get; set; }
        public virtual DbSet<RoomAvailibilities> RoomAvailibilities { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Semesters> Semesters { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Times> Times { get; set; }
        public virtual DbSet<TimeSlots> TimeSlots { get; set; }
        public virtual DbSet<TimeTables> TimeTables { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<StudentAttendance> StudentAttendances { get; set; }        
    }
}

