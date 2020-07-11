﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApi.EntityFramework;

namespace WebApi.EntityFramework.Migrations
{
    [DbContext(typeof(PrtgDbContext))]
    partial class PrtgDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("WebApi.Models.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("WebApi.Models.Contract", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<float>("BlackAndWhitePrice")
                        .HasColumnType("real");

                    b.Property<int>("BlackAndWhiteSheets")
                        .HasColumnType("integer");

                    b.Property<float>("ColorPrice")
                        .HasColumnType("real");

                    b.Property<int>("ColorSheets")
                        .HasColumnType("integer");

                    b.Property<long?>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<float>("ExcedenteBlackAndWhitePrice")
                        .HasColumnType("real");

                    b.Property<float>("ExcedenteColorPrice")
                        .HasColumnType("real");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<long>("PrinterId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("WebApi.Models.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ContractId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ContractId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("WebApi.Models.Reports.WeeklyPrinter", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BlackAndWhiteCopies")
                        .HasColumnType("integer");

                    b.Property<int>("ColorCopies")
                        .HasColumnType("integer");

                    b.Property<long>("PrinterId")
                        .HasColumnType("bigint");

                    b.Property<int>("Week")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("WeeklyPrinters");
                });

            modelBuilder.Entity("WebApi.Models.Contract", b =>
                {
                    b.HasOne("WebApi.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("WebApi.Models.Employee", b =>
                {
                    b.HasOne("WebApi.Models.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId");

                    b.HasOne("WebApi.Models.Contract", null)
                        .WithMany("Employees")
                        .HasForeignKey("ContractId");
                });
#pragma warning restore 612, 618
        }
    }
}