﻿// <auto-generated />
using System;
using BankPortalAggregator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankPortalAggregator.Migrations
{
    [DbContext(typeof(BankContext))]
    [Migration("20190518175324_DepositVariationAdded")]
    partial class DepositVariationAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BankApi.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ActiveDateFrom");

                    b.Property<DateTime>("ActiveDateTo");

                    b.Property<int?>("BankId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("IsActive");

                    b.Property<decimal>("MaxSum");

                    b.Property<decimal>("MinSum");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("BankPortalAggregator.Models.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("BankPortalAggregator.Models.Deposit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ActiveDateFrom");

                    b.Property<DateTime?>("ActiveDateTo");

                    b.Property<int?>("BankDepositId")
                        .HasColumnName("BankDepositId");

                    b.Property<int>("BankId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("IsActive");

                    b.Property<decimal?>("MaxSum");

                    b.Property<decimal>("MinSum");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("Deposits");
                });

            modelBuilder.Entity("BankPortalAggregator.Models.DepositVariation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepositId");

                    b.Property<decimal>("Percent");

                    b.Property<string>("Term");

                    b.HasKey("Id");

                    b.HasIndex("DepositId");

                    b.ToTable("DepositVariations");
                });

            modelBuilder.Entity("BankPortalAggregator.Models.Endpoint", b =>
                {
                    b.Property<int>("EndpointId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BankId");

                    b.Property<string>("EndpointUrl");

                    b.HasKey("EndpointId");

                    b.HasIndex("BankId");

                    b.ToTable("Endpoints");
                });

            modelBuilder.Entity("BankPortalAggregator.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccessToken");

                    b.Property<string>("Email");

                    b.Property<string>("IdToken");

                    b.Property<string>("Name");

                    b.Property<string>("RefreshToken");

                    b.Property<string>("Sub");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BankApi.Models.Product", b =>
                {
                    b.HasOne("BankPortalAggregator.Models.Bank")
                        .WithMany("Products")
                        .HasForeignKey("BankId");
                });

            modelBuilder.Entity("BankPortalAggregator.Models.Deposit", b =>
                {
                    b.HasOne("BankPortalAggregator.Models.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BankPortalAggregator.Models.DepositVariation", b =>
                {
                    b.HasOne("BankPortalAggregator.Models.Deposit", "Deposit")
                        .WithMany()
                        .HasForeignKey("DepositId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BankPortalAggregator.Models.Endpoint", b =>
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
