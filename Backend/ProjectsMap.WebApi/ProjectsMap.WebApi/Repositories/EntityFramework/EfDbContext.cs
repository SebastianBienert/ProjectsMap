using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectsMap.WebApi.Infrastructure;
using ProjectsMap.WebApi.Migrations;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.EntityFramework;


namespace ProjectsMap.WebApi.Repositories
{
    [DbConfigurationType(typeof(DataContextConfiguration))]
    public class EfDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Technology> Technologies { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<Floor> Floors { get; set; }

        public DbSet<Wall> Walls { get; set; }

        public DbSet<ProjectRole> ProjectRoles { get; set; }

        public EfDbContext() : base("name=ProjectsMapDbContext", throwIfV1Schema: false)
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 600;
            this.Configuration.LazyLoadingEnabled = false;
             Database.SetInitializer(new ProjectsMapDbInitializer());
        }

        public static EfDbContext Create()
        {
            return new EfDbContext();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //One to one or zero [Seat - Employee]
            modelBuilder.Entity<Seat>()
                .HasOptional(s => s.Employee)
                .WithOptionalDependent(d => d.Seat);

            //One to many [Room - seat]
            modelBuilder.Entity<Seat>()
                .HasRequired<Room>(s => s.Room)
                .WithMany(r => r.Seats)
                .HasForeignKey<int>(s => s.RoomId);

            //One to many relation [Floor - Room]
            modelBuilder.Entity<Room>()
                .HasOptional<Floor>(r => r.Floor)
                .WithMany(f => f.Rooms)
                .HasForeignKey<int?>(r => r.FloorId);

            //One to many relation [Building - Floor]
            modelBuilder.Entity<Floor>()
                .HasRequired<Building>(f => f.Building)
                .WithMany(b => b.Floors)
                .HasForeignKey(f => f.BuildingId);


            //One to zero one relation [ApplicationUser - Employee]
            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(au => au.Employee)
                .WithRequired(e => e.ApplicationUser);

            //One to many [Floor - Wall]
            modelBuilder.Entity<Wall>()
                .HasOptional(w => w.Floor)
                .WithMany(f => f.Walls)
                .HasForeignKey(w => w.FloorId);

            //Many to many [Wall - Room]
            modelBuilder.Entity<Wall>()
                .HasMany<Room>(w => w.Rooms)
                .WithMany(r => r.Walls)
                .Map(wr =>
                {
                    wr.MapLeftKey("WallRefId");
                    wr.MapRightKey("RoomRefId");
                    wr.ToTable("WallRoom");
                });


            //Many to many [Employee - Technology]
            modelBuilder.Entity<Employee>()
                .HasMany<Technology>(d => d.Technologies)
                .WithMany(t => t.Employees)
                .Map(dt =>
                {
                    dt.MapLeftKey("EmployeeRefId");
                    dt.MapRightKey("TechnologyRefId");
                    dt.ToTable("EmployeeTechnology");
                });

            //Dev - manager
            modelBuilder.Entity<Employee>()
                .HasOptional(d => d.Manager)
                .WithMany()
                .HasForeignKey(d => d.ManagerId);

            //CUSTOM MANY TO MANY [PROJECT - DEV] WITH ROLE
            modelBuilder.Entity<ProjectRole>()
                .HasRequired(r => r.Employee)
                .WithMany(p => p.ProjectRoles)
                .HasForeignKey(r => r.EmployeeId);

            modelBuilder.Entity<ProjectRole>()
                .HasRequired(r => r.Project)
                .WithMany(p => p.ProjectRoles)
                .HasForeignKey(r => r.ProjectId);


            //Many to many [Project - Technology]
            modelBuilder.Entity<Project>()
                .HasMany<Technology>(p => p.Technologies)
                .WithMany(t => t.Projects)
                .Map(pt =>
                {
                    pt.MapLeftKey("ProjectRefId");
                    pt.MapRightKey("TechnologyRefId");
                    pt.ToTable("ProjectTechnology");
                });

            //Unique index on Technology Name
            modelBuilder.Entity<Technology>()
                .HasIndex(t => t.Name)
                .IsUnique();

            //Composite Key Employee [devID, companyID]
            modelBuilder.Entity<Employee>()
                .HasKey(d => d.Id);

            //Composite Key ProjectRole [devID, projectID]
            modelBuilder.Entity<ProjectRole>()
                .HasKey(pr => new {pr.EmployeeId, pr.ProjectId});

        }
    }
}