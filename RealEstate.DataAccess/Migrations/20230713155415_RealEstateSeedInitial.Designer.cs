﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealEstate.DataAccess;

#nullable disable

namespace RealEstate.DataAccess.Migrations
{
    [DbContext(typeof(RealEstateDbContext))]
    [Migration("20230713155415_RealEstateSeedInitial")]
    partial class RealEstateSeedInitial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RealEstate.Domain.Entities.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Owners");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = true,
                            Address = "3071 Todds Lane San Antonio, TX 78205",
                            Birthday = new DateTime(1982, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "System",
                            CreatedOn = new DateTime(2023, 7, 13, 15, 54, 15, 119, DateTimeKind.Utc).AddTicks(6109),
                            ModifiedBy = "System",
                            ModifiedOn = new DateTime(2023, 7, 13, 15, 54, 15, 119, DateTimeKind.Utc).AddTicks(6109),
                            Name = "Sheri D. Coffelt",
                            Photo = "984bb1a4-46d8-4fbd-b9b1-fa78d395be7b.png"
                        },
                        new
                        {
                            Id = 2,
                            Active = true,
                            Address = "1641 Despard Street Atlanta, GA 30309",
                            Birthday = new DateTime(1994, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "System",
                            CreatedOn = new DateTime(2023, 7, 13, 15, 54, 15, 119, DateTimeKind.Utc).AddTicks(6109),
                            ModifiedBy = "System",
                            ModifiedOn = new DateTime(2023, 7, 13, 15, 54, 15, 119, DateTimeKind.Utc).AddTicks(6109),
                            Name = "Donnie K. Russell",
                            Photo = "ffc8ea6a-10c6-4997-916c-55b20910158c.png"
                        },
                        new
                        {
                            Id = 3,
                            Active = true,
                            Address = "1271 Hillcrest Circle Golden Valley, MN 55427",
                            Birthday = new DateTime(1986, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "System",
                            CreatedOn = new DateTime(2023, 7, 13, 15, 54, 15, 119, DateTimeKind.Utc).AddTicks(6109),
                            ModifiedBy = "System",
                            ModifiedOn = new DateTime(2023, 7, 13, 15, 54, 15, 119, DateTimeKind.Utc).AddTicks(6109),
                            Name = "Jeff S. Nelson",
                            Photo = "d73beddb-805f-47ca-baac-d9a2dbfb92ec.png"
                        });
                });

            modelBuilder.Entity("RealEstate.Domain.Entities.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CodeInternal")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("RealEstate.Domain.Entities.PropertyImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Enable")
                        .HasColumnType("bit");

                    b.Property<string>("File")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyImages");
                });

            modelBuilder.Entity("RealEstate.Domain.Entities.PropertyTrace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateSale")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Tax")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<decimal>("Value")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyTraces");
                });

            modelBuilder.Entity("RealEstate.Domain.Entities.Property", b =>
                {
                    b.HasOne("RealEstate.Domain.Entities.Owner", "Owner")
                        .WithMany("Properties")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("RealEstate.Domain.Entities.PropertyImage", b =>
                {
                    b.HasOne("RealEstate.Domain.Entities.Property", "Property")
                        .WithMany("PropertyImages")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("RealEstate.Domain.Entities.PropertyTrace", b =>
                {
                    b.HasOne("RealEstate.Domain.Entities.Property", "Property")
                        .WithMany("PropertyTraces")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("RealEstate.Domain.Entities.Owner", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("RealEstate.Domain.Entities.Property", b =>
                {
                    b.Navigation("PropertyImages");

                    b.Navigation("PropertyTraces");
                });
#pragma warning restore 612, 618
        }
    }
}
