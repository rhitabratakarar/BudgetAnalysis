﻿// <auto-generated />
using BudgetAnalysisDbApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BudgetAnalysisDbApi.Migrations
{
    [DbContext(typeof(BudgetAnalysisDbContext))]
    [Migration("20241008131246_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BudgetAnalysisDbApi.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("ExpenseCost")
                        .HasColumnType("float");

                    b.Property<int>("ExpenseType")
                        .HasColumnType("int");

                    b.Property<int>("MonthId")
                        .HasColumnType("int");

                    b.Property<int>("YearId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MonthId");

                    b.HasIndex("YearId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("BudgetAnalysisDbApi.Models.Month", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MonthName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Months");
                });

            modelBuilder.Entity("BudgetAnalysisDbApi.Models.Year", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("YearCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Years");
                });

            modelBuilder.Entity("BudgetAnalysisDbApi.Models.Expense", b =>
                {
                    b.HasOne("BudgetAnalysisDbApi.Models.Month", "Month")
                        .WithMany()
                        .HasForeignKey("MonthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetAnalysisDbApi.Models.Year", "Year")
                        .WithMany()
                        .HasForeignKey("YearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Month");

                    b.Navigation("Year");
                });
#pragma warning restore 612, 618
        }
    }
}
