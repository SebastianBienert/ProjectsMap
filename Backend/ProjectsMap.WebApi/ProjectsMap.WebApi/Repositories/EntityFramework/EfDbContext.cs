﻿using System;
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

        public EfDbContext() : base("name=ProjectsMapDbContext")
        {
            Database.SetInitializer(new ProjectsMapDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //One to one or zero [Seat - Developer]
            modelBuilder.Entity<Seat>()
                .HasRequired(s => s.Developer)
                .WithMany(d => d.Seat)
                .HasForeignKey<int>(s => s.DeveloperId);
 

            //One to many [Room - seat]
            modelBuilder.Entity<Seat>()
                .HasRequired<Room>(s => s.Room)
                .WithMany(r => r.Seats)
                .HasForeignKey<int>(s => s.RoomId);


            //One to one or zero [User - Developer]
            modelBuilder.Entity<User>()
                .HasOptional(u => u.Developer)
                .WithRequired(d => d.User);

            //One to one or zero [Vertex - Seat]
            modelBuilder.Entity<Vertex>()
                .HasOptional(v => v.Seat)
                .WithRequired(s => s.Vertex);

            //Many to many [Vertex - Room]
            modelBuilder.Entity<Vertex>()
                .HasMany<Room>(v => v.Rooms)
                .WithMany(r => r.Vertexes)
                .Map(vr =>
                {
                    vr.MapLeftKey("VertexRefId");
                    vr.MapRightKey("RoomRefId");
                    vr.ToTable("VertexRoom");
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