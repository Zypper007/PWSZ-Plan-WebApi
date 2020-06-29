﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PWSZ_Plan_WebApi.Data;

namespace PWSZ_Plan_WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.Class", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdateBy")
                        .HasColumnType("int");

                    b.Property<int>("LecturerID")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Recurrence")
                        .HasColumnType("time");

                    b.Property<int>("RoomID")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Start")
                        .HasColumnType("time");

                    b.Property<int>("SubjectID")
                        .HasColumnType("int");

                    b.Property<int>("TotalHours")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("LecturerID");

                    b.HasIndex("RoomID");

                    b.HasIndex("SubjectID");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.Information", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorID")
                        .HasColumnType("int");

                    b.Property<int>("ClassID")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdateBy")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.HasIndex("ClassID");

                    b.ToTable("Informations");
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.Institute", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdateBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Institutes");
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.Major", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("InstituteID")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdateBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("InstituteID");

                    b.ToTable("Majors");
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.Plan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndSessionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdateBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartSessionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("YearID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("YearID");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.PlanClass", b =>
                {
                    b.Property<int>("PlanID")
                        .HasColumnType("int");

                    b.Property<int>("ClassID")
                        .HasColumnType("int");

                    b.HasKey("PlanID", "ClassID");

                    b.HasIndex("ClassID");

                    b.ToTable("PlanClass");
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.Room", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdateBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.Specialization", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdateBy")
                        .HasColumnType("int");

                    b.Property<int>("MajorID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("MajorID");

                    b.ToTable("Specyfications");
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.Subject", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdateBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("HashCode")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("InstituteID")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdateBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Permission")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("InstituteID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.Year", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdateBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("SpecializationID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SpecializationID");

                    b.ToTable("Years");
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.Class", b =>
                {
                    b.HasOne("PWSZ_Plan_WebApi.Models.User", "Lecturer")
                        .WithMany("LecturerClasses")
                        .HasForeignKey("LecturerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PWSZ_Plan_WebApi.Models.Room", "Room")
                        .WithMany("Classes")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PWSZ_Plan_WebApi.Models.Subject", "Subject")
                        .WithMany("Classes")
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.Information", b =>
                {
                    b.HasOne("PWSZ_Plan_WebApi.Models.User", "Author")
                        .WithMany("Informations")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PWSZ_Plan_WebApi.Models.Class", "Class")
                        .WithMany("Informations")
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.Major", b =>
                {
                    b.HasOne("PWSZ_Plan_WebApi.Models.Institute", "Institute")
                        .WithMany("Majors")
                        .HasForeignKey("InstituteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.Plan", b =>
                {
                    b.HasOne("PWSZ_Plan_WebApi.Models.Year", "Year")
                        .WithMany("Plans")
                        .HasForeignKey("YearID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.PlanClass", b =>
                {
                    b.HasOne("PWSZ_Plan_WebApi.Models.Class", "Class")
                        .WithMany("Plans")
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PWSZ_Plan_WebApi.Models.Plan", "Plan")
                        .WithMany("Classes")
                        .HasForeignKey("PlanID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.Specialization", b =>
                {
                    b.HasOne("PWSZ_Plan_WebApi.Models.Major", "Major")
                        .WithMany("Specializations")
                        .HasForeignKey("MajorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.User", b =>
                {
                    b.HasOne("PWSZ_Plan_WebApi.Models.Institute", "Institute")
                        .WithMany("Managers")
                        .HasForeignKey("InstituteID")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("PWSZ_Plan_WebApi.Models.Year", b =>
                {
                    b.HasOne("PWSZ_Plan_WebApi.Models.Specialization", "Specialization")
                        .WithMany("Years")
                        .HasForeignKey("SpecializationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}