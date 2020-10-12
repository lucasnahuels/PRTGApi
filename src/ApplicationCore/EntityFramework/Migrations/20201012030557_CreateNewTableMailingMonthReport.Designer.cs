﻿// <auto-generated />
using System;
using ApplicationCore.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApplicationCore.EntityFramework.Migrations
{
    [DbContext(typeof(PrtgDbContext))]
    [Migration("20201012030557_CreateNewTableMailingMonthReport")]
    partial class CreateNewTableMailingMonthReport
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ApplicationCore.Models.Contract", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BlackAndWhiteLimitSet")
                        .HasColumnType("integer");

                    b.Property<float>("BlackAndWhitePrice")
                        .HasColumnType("real");

                    b.Property<int>("ColorLimitSet")
                        .HasColumnType("integer");

                    b.Property<float>("ColorPrice")
                        .HasColumnType("real");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<float>("SurplusBlackAndWhitePrice")
                        .HasColumnType("real");

                    b.Property<float>("SurplusColorPrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("ApplicationCore.Models.ContractDevice", b =>
                {
                    b.Property<long>("ContractId")
                        .HasColumnType("bigint");

                    b.Property<string>("ObjId")
                        .HasColumnType("text");

                    b.HasKey("ContractId", "ObjId");

                    b.HasIndex("ObjId");

                    b.ToTable("ContractDevices");
                });

            modelBuilder.Entity("ApplicationCore.Models.ContractEmployee", b =>
                {
                    b.Property<long>("ContractId")
                        .HasColumnType("bigint");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.HasKey("ContractId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("ContractEmployees");
                });

            modelBuilder.Entity("ApplicationCore.Models.ContractUser", b =>
                {
                    b.Property<long>("ContractId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("ContractId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ContractUsers");
                });

            modelBuilder.Entity("ApplicationCore.Models.Device", b =>
                {
                    b.Property<string>("ObjId")
                        .HasColumnType("text");

                    b.HasKey("ObjId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("ApplicationCore.Models.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ApplicationCore.Models.Owner", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("ApplicationCore.Models.Reports.DailyContadoresDataDevices", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BlackAndWhiteCopies")
                        .HasColumnType("integer");

                    b.Property<int>("ColorCopies")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateToday")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("DailyContadores");
                });

            modelBuilder.Entity("ApplicationCore.Models.Reports.DailyTonersDataDevices", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BlackTonersUsed")
                        .HasColumnType("integer");

                    b.Property<int>("CyanTonersUsed")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateToday")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<int>("MagentaTonersUsed")
                        .HasColumnType("integer");

                    b.Property<int>("YellowTonersUsed")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("DailyToners");
                });

            modelBuilder.Entity("ApplicationCore.Models.Reports.MailingMonthReport", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ContractId")
                        .HasColumnType("text");

                    b.Property<string>("DeviceId")
                        .HasColumnType("text");

                    b.Property<bool>("IsColor")
                        .HasColumnType("boolean");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("MailingMonthReport");
                });

            modelBuilder.Entity("ApplicationCore.Models.Reports.TonersUsed", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BlackTonersUsed")
                        .HasColumnType("integer");

                    b.Property<int>("CyanTonersUsed")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<int>("MagentaTonersUsed")
                        .HasColumnType("integer");

                    b.Property<int>("YellowTonersUsed")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TonersUsed");
                });

            modelBuilder.Entity("ApplicationCore.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ApplicationCore.Models.Contract", b =>
                {
                    b.HasOne("ApplicationCore.Models.Owner", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationCore.Models.ContractDevice", b =>
                {
                    b.HasOne("ApplicationCore.Models.Contract", "Contract")
                        .WithMany("ContractDevices")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Models.Device", "Device")
                        .WithMany("ContractDevices")
                        .HasForeignKey("ObjId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationCore.Models.ContractEmployee", b =>
                {
                    b.HasOne("ApplicationCore.Models.Contract", "Contract")
                        .WithMany("ContractEmployees")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Models.Employee", "Employee")
                        .WithMany("ContractEmployees")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationCore.Models.ContractUser", b =>
                {
                    b.HasOne("ApplicationCore.Models.Contract", "Contract")
                        .WithMany("ContractUsers")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Models.User", "User")
                        .WithMany("ContractUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationCore.Models.Employee", b =>
                {
                    b.HasOne("ApplicationCore.Models.Owner", "Owner")
                        .WithMany("Employees")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
