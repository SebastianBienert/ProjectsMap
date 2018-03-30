using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectsMap.WebApi.Infrastructure;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.EntityFramework;


namespace ProjectsMap.WebApi.Repositories
{
    public class EfDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Technology> Technologies { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Floor> Floors { get; set; }

        public DbSet<Wall> Walls { get; set; }

        public DbSet<ProjectRole> ProjectRoles { get; set; }

        public EfDbContext() : base("name=ProjectsMapDbContext", throwIfV1Schema: false)
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new ProjectsMapDbInitializer());
        }

        public static EfDbContext Create()
        {
            return new EfDbContext();
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

            //One to many relation [Company - Building]
            modelBuilder.Entity<Building>()
                .HasOptional<Company>(b => b.Company)
                .WithMany(c => c.Buildings)
                .HasForeignKey(b => b.CompanyId);

            //One to many relation [Company - Employee]
            modelBuilder.Entity<Employee>()
                .HasRequired<Company>(d => d.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(d => d.CompanyId);

            //One to many relation [Company - Projects]
            modelBuilder.Entity<Project>()
                .HasOptional(p => p.Company)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CompanyId);

            //One to zero one relation [ApplicationUser - Employee]
            modelBuilder.Entity<ApplicationUser>()
                .HasRequired(u => u.Employee)
                .WithOptional(e => e.ApplicationUser);

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
                    dt.MapLeftKey("EmployeeRefId", "EmployeeCompanyRefId");
                    dt.MapRightKey("TechnologyRefId");
                    dt.ToTable("EmployeeTechnology");
                });

            //Dev - manager
            modelBuilder.Entity<Employee>()
                .HasOptional(d => d.Manager)
                .WithMany()
                .HasForeignKey(d => new {d.ManagerId, d.ManagerCompanyId});

            //CUSTOM MANY TO MANY [PROJECT - DEV] WITH ROLE
            modelBuilder.Entity<ProjectRole>()
                .HasRequired(r => r.Employee)
                .WithMany(p => p.ProjectRoles)
                .HasForeignKey(r => new {r.EmployeeId, r.EmployeeCompanyId} );

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
                .HasKey(d => new { d.EmployeeId, d.CompanyId });

            //Composite Key ProjectRole [devID, projectID]
            modelBuilder.Entity<ProjectRole>()
                .HasKey(pr => new {pr.EmployeeId, pr.ProjectId});

        }
    }
}