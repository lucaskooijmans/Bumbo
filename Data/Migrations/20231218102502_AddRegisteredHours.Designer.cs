﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace bumbo.Migrations
{
    [DbContext(typeof(BumboContext))]
    [Migration("20231218102502_AddRegisteredHours")]
    partial class AddRegisteredHours
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Data.Models.Absence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Absences");
                });

            modelBuilder.Entity("Data.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Data.Models.Availability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Availabilities");
                });

            modelBuilder.Entity("Data.Models.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.HasKey("Id");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("Data.Models.DailyExpectations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ExpectedColli")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("ExpectedCustomers")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("ExpectedTemperature")
                        .HasColumnType("int");

                    b.Property<string>("ExpectedWeather")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Holiday")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("DailyExpectations");
                });

            modelBuilder.Entity("Data.Models.DailyPrognosis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Department")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfHours")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("DailyPrognosis");
                });

            modelBuilder.Entity("Data.Models.Department", b =>
                {
                    b.Property<int>("Name")
                        .HasMaxLength(45)
                        .HasColumnType("int");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int>("Meters")
                        .HasColumnType("int");

                    b.HasKey("Name");

                    b.HasIndex("BranchId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Data.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BSN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Data.Models.EmployeeDepartment", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId", "DepartmentId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("EmployeeDepartments");
                });

            modelBuilder.Entity("Data.Models.Norm", b =>
                {
                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Type");

                    b.HasIndex("BranchId");

                    b.ToTable("Norms");
                });

            modelBuilder.Entity("Data.Models.RegisteredHour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ApprovedById")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("EmployeeId");

                    b.ToTable("RegisteredHours");
                });

            modelBuilder.Entity("Data.Models.ReplacementRequest", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("ShiftId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("EmployeeId", "ShiftId");

                    b.HasIndex("ShiftId");

                    b.ToTable("ReplacementRequests");
                });

            modelBuilder.Entity("Data.Models.SchoolHours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("Hours")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("SchoolHours");
                });

            modelBuilder.Entity("Data.Models.Shift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentName")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("DepartmentName");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Data.Models.Absence", b =>
                {
                    b.HasOne("Data.Models.Employee", "Employee")
                        .WithMany("Absences")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Data.Models.Availability", b =>
                {
                    b.HasOne("Data.Models.Employee", "Employee")
                        .WithMany("Availabilities")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Data.Models.DailyExpectations", b =>
                {
                    b.HasOne("Data.Models.Branch", "Branch")
                        .WithMany("DailyExpection")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("Data.Models.DailyPrognosis", b =>
                {
                    b.HasOne("Data.Models.Branch", "Branch")
                        .WithMany("DailyPrognosis")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("Data.Models.Department", b =>
                {
                    b.HasOne("Data.Models.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("Data.Models.Employee", b =>
                {
                    b.HasOne("Data.Models.Branch", "Branch")
                        .WithMany("Employees")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.AppUser", "User")
                        .WithOne("Employee")
                        .HasForeignKey("Data.Models.Employee", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Branch");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Models.EmployeeDepartment", b =>
                {
                    b.HasOne("Data.Models.Department", "Department")
                        .WithMany("EmployeeDepartments")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Data.Models.Employee", "Employee")
                        .WithMany("EmployeeDepartments")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Data.Models.Norm", b =>
                {
                    b.HasOne("Data.Models.Branch", "Branch")
                        .WithMany("Norm")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("Data.Models.RegisteredHour", b =>
                {
                    b.HasOne("Data.Models.Employee", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById");

                    b.HasOne("Data.Models.Employee", "Employee")
                        .WithMany("RegisteredHours")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ApprovedBy");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Data.Models.ReplacementRequest", b =>
                {
                    b.HasOne("Data.Models.Employee", "Employee")
                        .WithMany("replacementRequests")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.Shift", "Shift")
                        .WithMany("ReplacementRequests")
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Shift");
                });

            modelBuilder.Entity("Data.Models.SchoolHours", b =>
                {
                    b.HasOne("Data.Models.Employee", "Employee")
                        .WithMany("SchoolHours")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Data.Models.Shift", b =>
                {
                    b.HasOne("Data.Models.Branch", "Branch")
                        .WithMany("Shifts")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Data.Models.Department", "Department")
                        .WithMany("Shifts")
                        .HasForeignKey("DepartmentName")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Data.Models.Employee", "Employee")
                        .WithMany("Shifts")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Branch");

                    b.Navigation("Department");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Models.AppUser", b =>
                {
                    b.Navigation("Employee")
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Models.Branch", b =>
                {
                    b.Navigation("DailyExpection");

                    b.Navigation("DailyPrognosis");

                    b.Navigation("Employees");

                    b.Navigation("Norm");

                    b.Navigation("Shifts");
                });

            modelBuilder.Entity("Data.Models.Department", b =>
                {
                    b.Navigation("EmployeeDepartments");

                    b.Navigation("Shifts");
                });

            modelBuilder.Entity("Data.Models.Employee", b =>
                {
                    b.Navigation("Absences");

                    b.Navigation("Availabilities");

                    b.Navigation("EmployeeDepartments");

                    b.Navigation("RegisteredHours");

                    b.Navigation("SchoolHours");

                    b.Navigation("Shifts");

                    b.Navigation("replacementRequests");
                });

            modelBuilder.Entity("Data.Models.Shift", b =>
                {
                    b.Navigation("ReplacementRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
