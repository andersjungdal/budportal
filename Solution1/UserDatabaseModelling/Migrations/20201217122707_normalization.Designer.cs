﻿// <auto-generated />
using System;
using DatabaseModelling.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DatabaseModelling.Migrations
{
    [DbContext(typeof(SecurityDbContext))]
    [Migration("20201217122707_normalization")]
    partial class normalization
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DatabaseModelling.DbModels.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PublicIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductionPlanTemplateId")
                        .HasColumnType("int");

                    b.Property<Guid>("PublicIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("RawBidTemplateId")
                        .HasColumnType("int");

                    b.Property<int?>("RoadenId")
                        .HasColumnType("int");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("XmlIdentifier")
                        .HasColumnType("bigint");

                    b.Property<int?>("ZoneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductionPlanTemplateId");

                    b.HasIndex("RawBidTemplateId");

                    b.HasIndex("RoadenId");

                    b.HasIndex("ZoneId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.LoggedData.ProductionPlanLogged.ProductionPlanCell", b =>
                {
                    b.Property<int>("ColumnId")
                        .HasColumnType("int");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<int?>("ProductionPlanColumnId")
                        .HasColumnType("int");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.HasKey("ColumnId", "Index");

                    b.HasIndex("ProductionPlanColumnId");

                    b.ToTable("ProductionPlanCells");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.LoggedData.ProductionPlanLogged.ProductionPlanColumn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProductionPlanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductionPlanId");

                    b.ToTable("ProductionPlanColumns");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.LoggedData.RawBidLogged.RawBidCell", b =>
                {
                    b.Property<int>("ColumnId")
                        .HasColumnType("int");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<double>("Prize")
                        .HasColumnType("float");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<int?>("RawBidColumnId")
                        .HasColumnType("int");

                    b.HasKey("ColumnId", "Index");

                    b.HasIndex("RawBidColumnId");

                    b.ToTable("RawBidCells");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.LoggedData.RawBidLogged.RawBidColumn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("RawBidId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RawBidId");

                    b.ToTable("RawBidColumns");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.ProductionPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PublicIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.Property<string>("XmlString")
                        .HasColumnType("xml");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("ProductionPlans");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.RawBid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PublicIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.Property<string>("XmlString")
                        .HasColumnType("xml");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("RawBids");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.Roaden", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Road")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roads");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<byte>("Role")
                        .HasColumnType("tinyint");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.XmlTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("PublicIdentifire")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("XMLTemplate")
                        .HasColumnType("xml");

                    b.HasKey("Id");

                    b.ToTable("XmlTemplates");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.Zone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.Company", b =>
                {
                    b.HasOne("DatabaseModelling.DbModels.XmlTemplate", "ProductionPlanTemplate")
                        .WithMany()
                        .HasForeignKey("ProductionPlanTemplateId");

                    b.HasOne("DatabaseModelling.DbModels.XmlTemplate", "RawBidTemplate")
                        .WithMany()
                        .HasForeignKey("RawBidTemplateId");

                    b.HasOne("DatabaseModelling.DbModels.Roaden", "Roaden")
                        .WithMany()
                        .HasForeignKey("RoadenId");

                    b.HasOne("DatabaseModelling.DbModels.Zone", "Zone")
                        .WithMany()
                        .HasForeignKey("ZoneId");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.LoggedData.ProductionPlanLogged.ProductionPlanCell", b =>
                {
                    b.HasOne("DatabaseModelling.DbModels.LoggedData.ProductionPlanLogged.ProductionPlanColumn", null)
                        .WithMany("Rows")
                        .HasForeignKey("ProductionPlanColumnId");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.LoggedData.ProductionPlanLogged.ProductionPlanColumn", b =>
                {
                    b.HasOne("DatabaseModelling.DbModels.ProductionPlan", "ProductionPlan")
                        .WithMany()
                        .HasForeignKey("ProductionPlanId");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.LoggedData.RawBidLogged.RawBidCell", b =>
                {
                    b.HasOne("DatabaseModelling.DbModels.LoggedData.RawBidLogged.RawBidColumn", null)
                        .WithMany("Rows")
                        .HasForeignKey("RawBidColumnId");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.LoggedData.RawBidLogged.RawBidColumn", b =>
                {
                    b.HasOne("DatabaseModelling.DbModels.RawBid", "RawBid")
                        .WithMany()
                        .HasForeignKey("RawBidId");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.ProductionPlan", b =>
                {
                    b.HasOne("DatabaseModelling.DbModels.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId");

                    b.HasOne("DatabaseModelling.DbModels.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("DatabaseModelling.DbModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.RawBid", b =>
                {
                    b.HasOne("DatabaseModelling.DbModels.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId");

                    b.HasOne("DatabaseModelling.DbModels.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("DatabaseModelling.DbModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DatabaseModelling.DbModels.User", b =>
                {
                    b.HasOne("DatabaseModelling.DbModels.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("DatabaseModelling.DbModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("DatabaseModelling.DbModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseModelling.DbModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("DatabaseModelling.DbModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
