﻿// <auto-generated />
using System;
using BankPortalAggregator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BankPortalAggregator.Migrations
{
    [DbContext(typeof(BankContext))]
    [Migration("20190210133355_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("BankPortalAggregator.Models.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("BankPortalAggregator.Models.Deposit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ActiveDateFrom");

                    b.Property<DateTime>("ActiveDateTo");

                    b.Property<int>("BankId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("IsActive");

                    b.Property<decimal>("MaxSum");

                    b.Property<decimal>("MinSum");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("Deposits");
                });

            modelBuilder.Entity("BankPortalAggregator.Models.Deposit", b =>
                {
                    b.HasOne("BankPortalAggregator.Models.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
