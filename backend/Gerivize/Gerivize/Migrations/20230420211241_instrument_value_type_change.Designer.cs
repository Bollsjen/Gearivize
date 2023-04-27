﻿// <auto-generated />
using System;
using Gerivize.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gerivize.Migrations
{
    [DbContext(typeof(GearivizeLocalContext))]
    [Migration("20230420211241_instrument_value_type_change")]
    partial class instrument_value_type_change
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb3_general_ci")
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb3");

            modelBuilder.Entity("Gerivize.Models.Efmigrationshistory", b =>
                {
                    b.Property<string>("MigrationId")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("ProductVersion")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.HasKey("MigrationId")
                        .HasName("PRIMARY");

                    b.ToTable("__efmigrationshistory", (string)null);

                    MySqlEntityTypeBuilderExtensions.HasCharSet(b, "utf8mb4");
                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_0900_ai_ci");
                });

            modelBuilder.Entity("Gerivize.Models.Instrument", b =>
                {
                    b.Property<string>("ANumber")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("a_number");

                    b.Property<string>("CalibrationReportNumber")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("calibration_report_number");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("comment");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("creation_date");

                    b.Property<bool>("ExternalCalibration")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("external_calibration");

                    b.Property<string>("Image")
                        .HasColumnType("longtext")
                        .HasColumnName("image_source");

                    b.Property<bool>("Inactive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("inactive");

                    b.Property<string>("InstrumentName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("instrument_name");

                    b.Property<string>("InternalCalibration")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("internal_calibration");

                    b.Property<DateTime>("LastCalibratedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("last_calibrated_date");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("manufacturer");

                    b.Property<DateTime>("NextCalibrationDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("next_calibration_date");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("note");

                    b.Property<string>("ReferenceCalibrationInstruction")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("reference_calibration_instruction");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("serial_number");

                    b.Property<int>("Test")
                        .HasColumnType("int")
                        .HasColumnName("test");

                    b.Property<Guid?>("TestTemplateId")
                        .HasColumnType("char(36)")
                        .HasColumnName("test_template_id");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("type");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("char(36)")
                        .HasColumnName("user_id");

                    b.Property<int>("Value")
                        .HasColumnType("int")
                        .HasColumnName("value");

                    b.HasKey("ANumber");

                    b.HasIndex("TestTemplateId");

                    b.HasIndex("UserId");

                    b.ToTable("instrument");
                });

            modelBuilder.Entity("Gerivize.Models.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<string>("ANumber")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("a_number");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("creation_date");

                    b.Property<bool>("HasReacted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("has_reacted");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("ANumber");

                    b.HasIndex("UserId");

                    b.ToTable("notification");
                });

            modelBuilder.Entity("Gerivize.Models.TestTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("test_template");
                });

            modelBuilder.Entity("Gerivize.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("creation_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("password");

                    b.Property<bool>("Responsible")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("responsible");

                    b.Property<bool>("SuperUser")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("super_user");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Gerivize.Models.Instrument", b =>
                {
                    b.HasOne("Gerivize.Models.TestTemplate", "TestTemplate")
                        .WithMany("Instrument")
                        .HasForeignKey("TestTemplateId");

                    b.HasOne("Gerivize.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("TestTemplate");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gerivize.Models.Notification", b =>
                {
                    b.HasOne("Gerivize.Models.Instrument", "Instrument")
                        .WithMany()
                        .HasForeignKey("ANumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gerivize.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instrument");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gerivize.Models.TestTemplate", b =>
                {
                    b.Navigation("Instrument");
                });
#pragma warning restore 612, 618
        }
    }
}
