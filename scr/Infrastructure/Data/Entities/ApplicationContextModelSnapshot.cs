﻿// <auto-generated />
using System;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Entities
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Core.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Core.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdDepartment")
                        .HasColumnType("int");

                    b.Property<int>("IdPosition")
                        .HasColumnType("int");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PositionId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Core.Entities.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("IdEmployee")
                        .HasColumnType("int");

                    b.Property<int>("IdStatus")
                        .HasColumnType("int");

                    b.Property<string>("InventoryNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProcentYear")
                        .HasColumnType("int");

                    b.Property<int?>("StatusEquipmentId")
                        .HasColumnType("int");

                    b.Property<int>("Term")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("StatusEquipmentId");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("Core.Entities.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Core.Entities.StatusEquipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StatusEquipments");
                });

            modelBuilder.Entity("Core.Entities.Employee", b =>
                {
                    b.HasOne("Core.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("Core.Entities.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionId");

                    b.Navigation("Department");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("Core.Entities.Equipment", b =>
                {
                    b.HasOne("Core.Entities.Employee", "Employee")
                        .WithMany("Equipments")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("Core.Entities.StatusEquipment", "StatusEquipment")
                        .WithMany("Equipments")
                        .HasForeignKey("StatusEquipmentId");

                    b.Navigation("Employee");

                    b.Navigation("StatusEquipment");
                });

            modelBuilder.Entity("Core.Entities.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Core.Entities.Employee", b =>
                {
                    b.Navigation("Equipments");
                });

            modelBuilder.Entity("Core.Entities.Position", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Core.Entities.StatusEquipment", b =>
                {
                    b.Navigation("Equipments");
                });
#pragma warning restore 612, 618
        }
    }
}
