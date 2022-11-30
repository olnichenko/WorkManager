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
    [Migration("20221130145617_Added_project_models")]
    partial class Addedprojectmodels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.Property<int>("PermissionsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RolesId")
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

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<long?>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("SolvedInVersionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("UserCreatedId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SolvedInVersionId");

                    b.HasIndex("UserCreatedId");

                    b.ToTable("Bugs");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Feature", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<long?>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("SolvedInVersionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("UserCreatedId")
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

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<long?>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("UserCreatedId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserCreatedId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Permission", b =>
                {
                    b.Property<int>("Id")
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

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("UserCreatedId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserCreatedId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Role", b =>
                {
                    b.Property<int>("Id")
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

                    b.Property<int?>("RoleId")
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

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<long?>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("UserCreatedId")
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
                        .HasForeignKey("UserCreatedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("SolvedInVersion");

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
                        .HasForeignKey("UserCreatedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
                        .HasForeignKey("UserCreatedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("UserCreated");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Project", b =>
                {
                    b.HasOne("WorkManagerDal.Models.User", "UserCreated")
                        .WithMany("Projects")
                        .HasForeignKey("UserCreatedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserCreated");
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
                        .HasForeignKey("UserCreatedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("UserCreated");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Project", b =>
                {
                    b.Navigation("Bugs");

                    b.Navigation("Features");

                    b.Navigation("Notes");

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

                    b.Navigation("Notes");

                    b.Navigation("Projects");

                    b.Navigation("Versions");
                });

            modelBuilder.Entity("WorkManagerDal.Models.Version", b =>
                {
                    b.Navigation("Bugs");

                    b.Navigation("Features");
                });
#pragma warning restore 612, 618
        }
    }
}
