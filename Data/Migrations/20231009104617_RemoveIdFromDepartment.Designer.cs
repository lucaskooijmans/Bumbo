﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Data;

#nullable disable

namespace bumbo.Migrations
{
    [DbContext(typeof(BumboContext))]
    [Migration("20231009104617_RemoveIdFromDepartment")]
    partial class RemoveIdFromDepartment
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Web.Models.Branch", b =>
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "City 1",
                            Name = "Branch 1",
                            Number = 123,
                            PostalCode = "12345",
                            Street = "Street 1"
                        });
                });

            modelBuilder.Entity("Web.Models.DailyExpectations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExpectedColli")
                        .HasColumnType("int");

                    b.Property<int>("ExpectedCustomers")
                        .HasColumnType("int");

                    b.Property<int>("ExpectedTemperature")
                        .HasColumnType("int");

                    b.Property<string>("ExpectedWeather")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Holiday")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("DailyExpectations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            ExpectedColli = 100,
                            ExpectedCustomers = 200,
                            ExpectedTemperature = 25,
                            ExpectedWeather = "sunny",
                            Holiday = ""
                        },
                        new
                        {
                            Id = 2,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Local),
                            ExpectedColli = 110,
                            ExpectedCustomers = 220,
                            ExpectedTemperature = 26,
                            ExpectedWeather = "sunny",
                            Holiday = ""
                        },
                        new
                        {
                            Id = 3,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 11, 0, 0, 0, 0, DateTimeKind.Local),
                            ExpectedColli = 120,
                            ExpectedCustomers = 240,
                            ExpectedTemperature = 27,
                            ExpectedWeather = "sunny",
                            Holiday = ""
                        },
                        new
                        {
                            Id = 4,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 12, 0, 0, 0, 0, DateTimeKind.Local),
                            ExpectedColli = 130,
                            ExpectedCustomers = 260,
                            ExpectedTemperature = 28,
                            ExpectedWeather = "sunny",
                            Holiday = ""
                        },
                        new
                        {
                            Id = 5,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 13, 0, 0, 0, 0, DateTimeKind.Local),
                            ExpectedColli = 140,
                            ExpectedCustomers = 280,
                            ExpectedTemperature = 29,
                            ExpectedWeather = "sunny",
                            Holiday = ""
                        },
                        new
                        {
                            Id = 6,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 14, 0, 0, 0, 0, DateTimeKind.Local),
                            ExpectedColli = 150,
                            ExpectedCustomers = 300,
                            ExpectedTemperature = 30,
                            ExpectedWeather = "sunny",
                            Holiday = ""
                        },
                        new
                        {
                            Id = 7,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Local),
                            ExpectedColli = 88,
                            ExpectedCustomers = 197,
                            ExpectedTemperature = 18,
                            ExpectedWeather = "rainy",
                            Holiday = "Christmas"
                        });
                });

            modelBuilder.Entity("Web.Models.DailyPrognosis", b =>
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 0,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 2,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 0,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 3,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 11, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 0,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 4,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 12, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 0,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 5,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 13, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 0,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 6,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 14, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 0,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 7,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 0,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 8,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 1,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 9,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 1,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 10,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 11, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 1,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 11,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 12, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 1,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 12,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 13, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 1,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 13,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 14, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 1,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 14,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 1,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 15,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 2,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 16,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 2,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 17,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 11, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 2,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 18,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 12, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 2,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 19,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 13, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 2,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 20,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 14, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 2,
                            NumberOfHours = 8
                        },
                        new
                        {
                            Id = 21,
                            BranchId = 1,
                            Date = new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Local),
                            Department = 2,
                            NumberOfHours = 8
                        });
                });

            modelBuilder.Entity("Web.Models.Department", b =>
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

                    b.HasData(
                        new
                        {
                            Name = 0,
                            BranchId = 1,
                            Meters = 100
                        },
                        new
                        {
                            Name = 1,
                            BranchId = 1,
                            Meters = 100
                        },
                        new
                        {
                            Name = 2,
                            BranchId = 1,
                            Meters = 100
                        });
                });

            modelBuilder.Entity("Web.Models.Norm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasMaxLength(45)
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("Norms");

                    b.HasData(
                        new
                        {
                            Id = 5,
                            BranchId = 1,
                            Type = 4,
                            Value = 30
                        },
                        new
                        {
                            Id = 1,
                            BranchId = 1,
                            Type = 1,
                            Value = 30
                        },
                        new
                        {
                            Id = 2,
                            BranchId = 1,
                            Type = 0,
                            Value = 5
                        },
                        new
                        {
                            Id = 3,
                            BranchId = 1,
                            Type = 3,
                            Value = 100
                        },
                        new
                        {
                            Id = 4,
                            BranchId = 1,
                            Type = 2,
                            Value = 30
                        });
                });

            modelBuilder.Entity("Web.Models.DailyExpectations", b =>
                {
                    b.HasOne("Web.Models.Branch", "Branch")
                        .WithMany("DailyExpection")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("Web.Models.DailyPrognosis", b =>
                {
                    b.HasOne("Web.Models.Branch", "Branch")
                        .WithMany("DailyPrognosis")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("Web.Models.Department", b =>
                {
                    b.HasOne("Web.Models.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("Web.Models.Norm", b =>
                {
                    b.HasOne("Web.Models.Branch", "Branch")
                        .WithMany("Norm")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("Web.Models.Branch", b =>
                {
                    b.Navigation("DailyExpection");

                    b.Navigation("DailyPrognosis");

                    b.Navigation("Norm");
                });
#pragma warning restore 612, 618
        }
    }
}
