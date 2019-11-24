using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace WorkerProfileApi.Models
{
    public class WorkerProfileApiContext : DbContext
    {
        public WorkerProfileApiContext(DbContextOptions<WorkerProfileApiContext> options) : base(options) { }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileSkill> ProfileSkills { get; set; }

        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProfileSkill>()
                .ToTable("ProfileSkills")
                .HasKey(bc => new { bc.ProfileID, bc.SkillID });

            // builder.Entity<ProfileSkill>()
            //     .HasOne(bc => bc.Profile)
            //     .WithMany(b => b.ProfileSkills)
            //     .HasForeignKey(bc => bc.ProfileID);

            // builder.Entity<ProfileSkill>()
            //     .HasOne(bc => bc.Skill)
            //     .WithMany(c => c.ProfileSkills)
            //     .HasForeignKey(bc => bc.SkillID);

            builder.Entity<Skill>()
                .HasIndex(s => s.Name)
                .IsUnique();

            builder.Entity<Profile>()
                .HasIndex(s => s.UID);

            var skills = new List<Skill>()
            {
                new Skill() { ID = 1, Name = "Waiter" },
                new Skill() { ID = 2, Name = "Cook" },
                new Skill() { ID = 3, Name = "Builder" },
                new Skill() { ID = 4, Name = "Software Dev" }
            };

            builder.Entity<Skill>().HasData(
                skills[0], skills[1], skills[2], skills[3]
            );

            builder.Entity<Profile>().HasIndex(u => u.Name).IsUnique();

            builder.Entity<Profile>().HasData(
                new Profile() { ID = 1, Name = "Test Profiler1", UID = "userX", Address = "10 Downing St, Westminster, London SW1A 2AA, UK", Point = new Point(51.5033635, -0.1276248) { SRID = 4326 } },
                new Profile() { ID = 2, Name = "Test Profiler2", UID = "userX", Address = "Canada Square, Canary Wharf, London E14, UK", Point = new Point(51.5053154, -0.0168585) { SRID = 4326 } },
                new Profile() { ID = 3, Name = "Test Profiler3", UID = "userY", Address = "Caledonian Pl, Edinburgh EH11, UK", Point = new Point(55.9438541, -3.2191237) { SRID = 4326 } }
            );

            builder.Entity<ProfileSkill>().HasData(
                new ProfileSkill() { ProfileID = 1, SkillID = 1 },
                new ProfileSkill() { ProfileID = 1, SkillID = 2 },
                new ProfileSkill() { ProfileID = 2, SkillID = 2 },
                new ProfileSkill() { ProfileID = 3, SkillID = 3 }
            );

        }
    }
}