﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataService.Model
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class TimeTableEntities : DbContext
    {
        public TimeTableEntities()
            : base("name=TimeTableEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Chair> Chairs { get; set; }
        public virtual DbSet<ChairToSpecialization> ChairToSpecializations { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<HighSchool> HighSchools { get; set; }
        public virtual DbSet<Specialization> Specializations { get; set; }
        public virtual DbSet<Specialty> Specialties { get; set; }
        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<ClassroomProperty> ClassroomProperties { get; set; }
        public virtual DbSet<ClassroomToClassroomProperty> ClassroomToClassroomProperties { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<ClassroomPropertyView> ClassroomPropertyViews { get; set; }
    }

}
