using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.EntityFramework;

namespace ProjectsMap.WebApi.Repositories
{
    public class EfDbContext : DbContext
    {
        public DbSet<Developer> Developers { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Technology> Technologies { get; set; }

        public DbSet<Vertex> Vertexes { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Floor> Floors { get; set; }

        public DbSet<Wall> Walls { get; set; }

        public EfDbContext() : base("name=ProjectsMapDbContext")
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new ProjectsMapDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //One to one or zero [Seat - Developer]
            modelBuilder.Entity<Seat>()
                .HasOptional(s => s.Developer)
                .WithMany(d => d.Seat)
                .HasForeignKey<int?>(s => s.DeveloperId);
 

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

            //One to many relation [Company - Developer]
            modelBuilder.Entity<Developer>()
                .HasOptional<Company>(d => d.Company)
                .WithMany(c => c.Developers)
                .HasForeignKey(d => d.CompanyId);

            //One to many relation [Company - Projects]
            modelBuilder.Entity<Project>()
                .HasOptional(p => p.Company)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CompanyId);

            //One to one or zero [User - Developer]
            modelBuilder.Entity<User>()
                .HasOptional(u => u.Developer)
                .WithRequired(d => d.User);

            //One to one or zero [Vertex - Seat]
            modelBuilder.Entity<Vertex>()
                .HasOptional(v => v.Seat)
                .WithRequired(s => s.Vertex);
            //Composite Key
            modelBuilder.Entity<Vertex>()
                .HasKey(v => new {v.X, v.Y});


            modelBuilder.Entity<Wall>()
                .HasOptional(w => w.StartVertex)
                .WithMany(v => v.StartWalls)
                .HasForeignKey(w => new {w.StartVertexX, w.StartVertexY});

            modelBuilder.Entity<Wall>()
                .HasOptional(w => w.EndVertex)
                .WithMany(v => v.EndWalls)
                .HasForeignKey(w => new {w.EndVertexX, w.EndVertexY});

            modelBuilder.Entity<Wall>()
                .HasOptional(w => w.Floor)
                .WithMany(f => f.Walls)
                .HasForeignKey(w => w.FloorId);

            modelBuilder.Entity<Wall>()
                .HasMany<Room>(w => w.Rooms)
                .WithMany(r => r.Walls)
                .Map(wr =>
                {
                    wr.MapLeftKey("WallRefId");
                    wr.MapRightKey("RoomRefId");
                    wr.ToTable("WallRoom");
                });


            //Many to many [Developer - Technology]
            modelBuilder.Entity<Developer>()
                .HasMany<Technology>(d => d.Technologies)
                .WithMany(t => t.Developers)
                .Map(dt =>
                {
                    dt.MapLeftKey("DeveloperRefId");
                    dt.MapRightKey("TechnologyRefId");
                    dt.ToTable("DeveloperTechnology");
                });

            //Many to many [Developer - Projects]
            modelBuilder.Entity<Developer>()
                .HasMany<Project>(d => d.Projects)
                .WithMany(p => p.Developers)
                .Map(dp =>
                {
                    dp.MapLeftKey("DeveloperRefId");
                    dp.MapRightKey("ProjectRefId");
                    dp.ToTable("DeveloperProject");
                });

            //Project has one or zero produt owner [Project - developer]
            modelBuilder.Entity<Project>()
                .HasOptional<Developer>(d => d.ProductOwner);




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

            //Many to many [Project - Room]
            modelBuilder.Entity<Project>()
                .HasMany<Room>(p => p.Rooms)
                .WithMany(r => r.Projects)
                .Map(pr =>
                {
                    pr.MapLeftKey("ProjectRefId");
                    pr.MapRightKey("RoomRefId");
                    pr.ToTable("ProjectRoom");
                });
        }
    }
}