﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkManagerDal;

#nullable disable

namespace WorkManagerDal.Migrations
{
    [DbContext(typeof(WorkManagerDbContext))]
    [Migration("20230206100627_Added_comments")]
    partial class Addedcomments
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.Property<long>("PermissionsId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("RolesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PermissionsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("PermissionRole");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Bug", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("SolvedInVersionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("UserCreatedId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SolvedInVersionId");

                    b.HasIndex("UserCreatedId");

                    b.ToTable("Bugs");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("BugId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<long?>("FeatureId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("UserCreatedId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BugId");

                    b.HasIndex("FeatureId");

                    b.HasIndex("UserCreatedId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Feature", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("SolvedInVersionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("UserCreatedId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SolvedInVersionId");

                    b.HasIndex("UserCreatedId");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Note", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<long?>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("UserCreatedId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserCreatedId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Permission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("UserCreatedId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserCreatedId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("WorkManagerDal.Models.ProjectsToUsers", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectsToUsers");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("WorkManagerDal.Models.TimeSpent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("BugId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateFrom")
                        .HasColumnType("TEXT");

                    b.Property<long?>("FeatureId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HoursCount")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("UserCreatedId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BugId");

                    b.HasIndex("FeatureId");

                    b.HasIndex("UserCreatedId");

                    b.ToTable("TimeSpents");
                });

            modelBuilder.Entity("WorkManagerDal.Models.UploadedFile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("BugId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("CommentId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<long?>("FeatureId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileType")
                        .HasColumnType("TEXT");

                    b.Property<long?>("NoteId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("UserCreatedId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("VersionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BugId");

                    b.HasIndex("CommentId");

                    b.HasIndex("FeatureId");

                    b.HasIndex("NoteId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserCreatedId");

                    b.HasIndex("VersionId");

                    b.ToTable("UploadedFiles");
                });

            modelBuilder.Entity("WorkManagerDal.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateRegistration")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Version", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<long?>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("UserCreatedId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserCreatedId");

                    b.ToTable("Versions");
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.HasOne("WorkManagerDal.Models.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkManagerDal.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkManagerDal.Models.Bug", b =>
                {
                    b.HasOne("WorkManagerDal.Models.Project", "Project")
                        .WithMany("Bugs")
                        .HasForeignKey("ProjectId");

                    b.HasOne("WorkManagerDal.Models.Version", "SolvedInVersion")
                        .WithMany("Bugs")
                        .HasForeignKey("SolvedInVersionId");

                    b.HasOne("WorkManagerDal.Models.User", "UserCreated")
                        .WithMany("Bugs")
                        .HasForeignKey("UserCreatedId");

                    b.Navigation("Project");

                    b.Navigation("SolvedInVersion");

                    b.Navigation("UserCreated");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Comment", b =>
                {
                    b.HasOne("WorkManagerDal.Models.Bug", "Bug")
                        .WithMany("Comments")
                        .HasForeignKey("BugId");

                    b.HasOne("WorkManagerDal.Models.Feature", "Feature")
                        .WithMany("Comments")
                        .HasForeignKey("FeatureId");

                    b.HasOne("WorkManagerDal.Models.User", "UserCreated")
                        .WithMany()
                        .HasForeignKey("UserCreatedId");

                    b.Navigation("Bug");

                    b.Navigation("Feature");

                    b.Navigation("UserCreated");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Feature", b =>
                {
                    b.HasOne("WorkManagerDal.Models.Project", "Project")
                        .WithMany("Features")
                        .HasForeignKey("ProjectId");

                    b.HasOne("WorkManagerDal.Models.Version", "SolvedInVersion")
                        .WithMany("Features")
                        .HasForeignKey("SolvedInVersionId");

                    b.HasOne("WorkManagerDal.Models.User", "UserCreated")
                        .WithMany("Features")
                        .HasForeignKey("UserCreatedId");

                    b.Navigation("Project");

                    b.Navigation("SolvedInVersion");

                    b.Navigation("UserCreated");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Note", b =>
                {
                    b.HasOne("WorkManagerDal.Models.Project", "Project")
                        .WithMany("Notes")
                        .HasForeignKey("ProjectId");

                    b.HasOne("WorkManagerDal.Models.User", "UserCreated")
                        .WithMany("Notes")
                        .HasForeignKey("UserCreatedId");

                    b.Navigation("Project");

                    b.Navigation("UserCreated");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Project", b =>
                {
                    b.HasOne("WorkManagerDal.Models.User", "UserCreated")
                        .WithMany("Projects")
                        .HasForeignKey("UserCreatedId");

                    b.Navigation("UserCreated");
                });

            modelBuilder.Entity("WorkManagerDal.Models.ProjectsToUsers", b =>
                {
                    b.HasOne("WorkManagerDal.Models.Project", "Project")
                        .WithMany("UsersHasAccess")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkManagerDal.Models.User", "User")
                        .WithMany("ProjectsHasAccess")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkManagerDal.Models.TimeSpent", b =>
                {
                    b.HasOne("WorkManagerDal.Models.Bug", "Bug")
                        .WithMany("TimeSpents")
                        .HasForeignKey("BugId");

                    b.HasOne("WorkManagerDal.Models.Feature", "Feature")
                        .WithMany("TimeSpents")
                        .HasForeignKey("FeatureId");

                    b.HasOne("WorkManagerDal.Models.User", "UserCreated")
                        .WithMany("TimeSpents")
                        .HasForeignKey("UserCreatedId");

                    b.Navigation("Bug");

                    b.Navigation("Feature");

                    b.Navigation("UserCreated");
                });

            modelBuilder.Entity("WorkManagerDal.Models.UploadedFile", b =>
                {
                    b.HasOne("WorkManagerDal.Models.Bug", "Bug")
                        .WithMany("Files")
                        .HasForeignKey("BugId");

                    b.HasOne("WorkManagerDal.Models.Comment", "Comment")
                        .WithMany("Files")
                        .HasForeignKey("CommentId");

                    b.HasOne("WorkManagerDal.Models.Feature", "Feature")
                        .WithMany("Files")
                        .HasForeignKey("FeatureId");

                    b.HasOne("WorkManagerDal.Models.Note", "Note")
                        .WithMany()
                        .HasForeignKey("NoteId");

                    b.HasOne("WorkManagerDal.Models.Project", "Project")
                        .WithMany("Files")
                        .HasForeignKey("ProjectId");

                    b.HasOne("WorkManagerDal.Models.User", "UserCreated")
                        .WithMany("Files")
                        .HasForeignKey("UserCreatedId");

                    b.HasOne("WorkManagerDal.Models.Version", "Version")
                        .WithMany("Files")
                        .HasForeignKey("VersionId");

                    b.Navigation("Bug");

                    b.Navigation("Comment");

                    b.Navigation("Feature");

                    b.Navigation("Note");

                    b.Navigation("Project");

                    b.Navigation("UserCreated");

                    b.Navigation("Version");
                });

            modelBuilder.Entity("WorkManagerDal.Models.User", b =>
                {
                    b.HasOne("WorkManagerDal.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Version", b =>
                {
                    b.HasOne("WorkManagerDal.Models.Project", "Project")
                        .WithMany("Versions")
                        .HasForeignKey("ProjectId");

                    b.HasOne("WorkManagerDal.Models.User", "UserCreated")
                        .WithMany("Versions")
                        .HasForeignKey("UserCreatedId");

                    b.Navigation("Project");

                    b.Navigation("UserCreated");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Bug", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Files");

                    b.Navigation("TimeSpents");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Comment", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Feature", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Files");

                    b.Navigation("TimeSpents");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Project", b =>
                {
                    b.Navigation("Bugs");

                    b.Navigation("Features");

                    b.Navigation("Files");

                    b.Navigation("Notes");

                    b.Navigation("UsersHasAccess");

                    b.Navigation("Versions");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("WorkManagerDal.Models.User", b =>
                {
                    b.Navigation("Bugs");

                    b.Navigation("Features");

                    b.Navigation("Files");

                    b.Navigation("Notes");

                    b.Navigation("Projects");

                    b.Navigation("ProjectsHasAccess");

                    b.Navigation("TimeSpents");

                    b.Navigation("Versions");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Version", b =>
                {
                    b.Navigation("Bugs");

                    b.Navigation("Features");

                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
