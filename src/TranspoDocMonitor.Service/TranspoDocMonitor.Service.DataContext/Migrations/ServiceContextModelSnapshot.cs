﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TranspoDocMonitor.Service.DataContext;

#nullable disable

namespace TranspoDocMonitor.Service.DataContext.Migrations
{
    [DbContext(typeof(ServiceContext))]
    partial class ServiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TranspoDocMonitor.Service.Domain.Identity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("TranspoDocMonitor.Service.Domain.Identity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hash");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.Property<string>("Surname")
                        .HasColumnType("text")
                        .HasColumnName("sur_name");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("TranspoDocMonitor.Service.Domain.Library.Dictionaries.DictionaryDocumentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DocumentName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DictionaryDocumentTypes");
                });

            modelBuilder.Entity("TranspoDocMonitor.Service.Domain.Library.Entities.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("AutoColor")
                        .HasColumnType("integer")
                        .HasColumnName("auto_color");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("make");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("model");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("registration_number");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year_of_issue");

                    b.HasKey("Id");

                    b.ToTable("vehicles", (string)null);
                });

            modelBuilder.Entity("TranspoDocMonitor.Service.Domain.Library.StagingTables.UserVehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid")
                        .HasColumnName("vehicle_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VehicleId");

                    b.ToTable("user_vehicle", (string)null);
                });

            modelBuilder.Entity("TranspoDocMonitor.Service.Domain.Library.StagingTables.VehicleDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateOfIssue")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_of_issue");

                    b.Property<Guid>("DictionaryDocumentTypeId")
                        .HasColumnType("uuid")
                        .HasColumnName("dictionary_document_type_id");

                    b.Property<int>("DocumentNumber")
                        .HasColumnType("integer")
                        .HasColumnName("document_number");

                    b.Property<DateTime>("ExpirationDateOfIssue")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expiration_date_of_Issue");

                    b.Property<Guid>("UserVehicleId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_vehicle_id");

                    b.HasKey("Id");

                    b.HasIndex("DictionaryDocumentTypeId");

                    b.HasIndex("UserVehicleId");

                    b.ToTable("transport_documents", (string)null);
                });

            modelBuilder.Entity("TranspoDocMonitor.Service.Domain.Identity.User", b =>
                {
                    b.HasOne("TranspoDocMonitor.Service.Domain.Identity.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TranspoDocMonitor.Service.Domain.Library.StagingTables.UserVehicle", b =>
                {
                    b.HasOne("TranspoDocMonitor.Service.Domain.Identity.User", "User")
                        .WithMany("UserVehicles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TranspoDocMonitor.Service.Domain.Library.Entities.Vehicle", "Vehicle")
                        .WithMany("UserVehicles")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("TranspoDocMonitor.Service.Domain.Library.StagingTables.VehicleDocument", b =>
                {
                    b.HasOne("TranspoDocMonitor.Service.Domain.Library.Dictionaries.DictionaryDocumentType", "DictionaryDocumentType")
                        .WithMany()
                        .HasForeignKey("DictionaryDocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TranspoDocMonitor.Service.Domain.Library.StagingTables.UserVehicle", "UserVehicle")
                        .WithMany("VehicleDocuments")
                        .HasForeignKey("UserVehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DictionaryDocumentType");

                    b.Navigation("UserVehicle");
                });

            modelBuilder.Entity("TranspoDocMonitor.Service.Domain.Identity.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("TranspoDocMonitor.Service.Domain.Identity.User", b =>
                {
                    b.Navigation("UserVehicles");
                });

            modelBuilder.Entity("TranspoDocMonitor.Service.Domain.Library.Entities.Vehicle", b =>
                {
                    b.Navigation("UserVehicles");
                });

            modelBuilder.Entity("TranspoDocMonitor.Service.Domain.Library.StagingTables.UserVehicle", b =>
                {
                    b.Navigation("VehicleDocuments");
                });
#pragma warning restore 612, 618
        }
    }
}
