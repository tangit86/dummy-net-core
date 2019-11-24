﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using WorkerProfileApi.Models;

namespace WorkerProfileApi.Migrations
{
    [DbContext(typeof(WorkerProfileApiContext))]
    [Migration("20191114193253_init2")]
    partial class init2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WorkerProfileApi.Models.Profile", b =>
            {
                b.Property<int>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Address")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(120)")
                    .HasMaxLength(120);

                b.Property<Point>("Point")
                    .HasColumnType("geography");

                b.Property<string>("UID")
                    .HasColumnType("nvarchar(50)")
                    .HasMaxLength(50);

                b.HasKey("ID");

                b.HasIndex("Name")
                    .IsUnique()
                    .HasFilter("[Name] IS NOT NULL");

                b.ToTable("Profiles");

                b.HasData(
                    new
                    {
                        ID = 1,
                            Address = "10 Downing St, Westminster, London SW1A 2AA, UK",
                            Name = "Test Profiler1",
                            Point = (NetTopologySuite.Geometries.Point) new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (51.5033635 -0.1276248)"),
                            UID = "userX"
                    },
                    new
                    {
                        ID = 2,
                            Address = "Canada Square, Canary Wharf, London E14, UK",
                            Name = "Test Profiler2",
                            Point = (NetTopologySuite.Geometries.Point) new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (51.5053154 -0.0168585)"),
                            UID = "userX"
                    },
                    new
                    {
                        ID = 3,
                            Address = "Caledonian Pl, Edinburgh EH11, UK",
                            Name = "Test Profiler3",
                            Point = (NetTopologySuite.Geometries.Point) new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (55.9438541 -3.2191237)"),
                            UID = "userY"
                    });
            });

            modelBuilder.Entity("WorkerProfileApi.Models.ProfileSkill", b =>
            {
                b.Property<int>("ProfileID")
                    .HasColumnType("int");

                b.Property<int>("SkillID")
                    .HasColumnType("int");

                b.HasKey("ProfileID", "SkillID");

                b.HasIndex("SkillID");

                b.ToTable("ProfileSkills");

                b.HasData(
                    new
                    {
                        ProfileID = 1,
                            SkillID = 1
                    },
                    new
                    {
                        ProfileID = 1,
                            SkillID = 2
                    },
                    new
                    {
                        ProfileID = 2,
                            SkillID = 2
                    },
                    new
                    {
                        ProfileID = 3,
                            SkillID = 3
                    });
            });

            modelBuilder.Entity("WorkerProfileApi.Models.Skill", b =>
            {
                b.Property<int>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(50)")
                    .HasMaxLength(50);

                b.HasKey("ID");

                b.HasIndex("Name")
                    .IsUnique()
                    .HasFilter("[Name] IS NOT NULL");

                b.ToTable("Skills");

                b.HasData(
                    new
                    {
                        ID = 1,
                            Name = "Waiter"
                    },
                    new
                    {
                        ID = 2,
                            Name = "Cook"
                    },
                    new
                    {
                        ID = 3,
                            Name = "Builder"
                    },
                    new
                    {
                        ID = 4,
                            Name = "Software Dev"
                    });
            });

            modelBuilder.Entity("WorkerProfileApi.Models.ProfileSkill", b =>
            {
                b.HasOne("WorkerProfileApi.Models.Profile", "Profile")
                    .WithMany("ProfileSkills")
                    .HasForeignKey("ProfileID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("WorkerProfileApi.Models.Skill", "Skill")
                    .WithMany("ProfileSkills")
                    .HasForeignKey("SkillID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
#pragma warning restore 612, 618
        }
    }
}