﻿// <auto-generated />
using System;
using Api.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.EF.Migrations
{
    [DbContext(typeof(ApiDbContextStubbed))]
    partial class ApiDbContextStubbedModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("Api.Entities.Dice", b =>
                {
                    b.Property<int>("NbFaces")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("NbFaces");

                    b.ToTable("dices");

                    b.HasData(
                        new
                        {
                            NbFaces = 2
                        },
                        new
                        {
                            NbFaces = 3
                        },
                        new
                        {
                            NbFaces = 4
                        },
                        new
                        {
                            NbFaces = 5
                        },
                        new
                        {
                            NbFaces = 6
                        });
                });

            modelBuilder.Entity("Api.Entities.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("profiles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cc6f9111-b174-4064-814b-ce7eb4169e80"),
                            Name = "Perret",
                            Surname = "Louis"
                        },
                        new
                        {
                            Id = new Guid("15a4a021-bea3-45e5-a8ff-58d6a5d902cd"),
                            Name = "Grienenberger",
                            Surname = "Côme"
                        },
                        new
                        {
                            Id = new Guid("5a3888f4-8bcf-4002-a86a-461515a4dd89"),
                            Name = "Malvezin",
                            Surname = "Neitah"
                        });
                });

            modelBuilder.Entity("Api.Entities.Throw", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("DiceId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Result")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("throws");

                    b.HasData(
                        new
                        {
                            Id = new Guid("aa6f9111-b174-4064-814b-ce7eb4169e80"),
                            DiceId = 2,
                            ProfileId = new Guid("cc6f9111-b174-4064-814b-ce7eb4169e80"),
                            Result = 1
                        },
                        new
                        {
                            Id = new Guid("dd6f9111-b174-4064-814b-ce7eb4169e80"),
                            DiceId = 2,
                            ProfileId = new Guid("cc6f9111-b174-4064-814b-ce7eb4169e80"),
                            Result = 2
                        },
                        new
                        {
                            Id = new Guid("4a4a5bfb-7e06-4e6b-b252-a8733593b612"),
                            DiceId = 4,
                            ProfileId = new Guid("15a4a021-bea3-45e5-a8ff-58d6a5d902cd"),
                            Result = 4
                        },
                        new
                        {
                            Id = new Guid("1fd5d81f-1fd8-497d-9895-e460d33d0a53"),
                            DiceId = 4,
                            ProfileId = new Guid("15a4a021-bea3-45e5-a8ff-58d6a5d902cd"),
                            Result = 3
                        },
                        new
                        {
                            Id = new Guid("668c5989-0569-4239-a1ce-319d77264d7e"),
                            DiceId = 3,
                            ProfileId = new Guid("5a3888f4-8bcf-4002-a86a-461515a4dd89"),
                            Result = 3
                        },
                        new
                        {
                            Id = new Guid("8e64d355-3951-44d3-b84b-a772df84c8a0"),
                            DiceId = 6,
                            ProfileId = new Guid("5a3888f4-8bcf-4002-a86a-461515a4dd89"),
                            Result = 5
                        });
                });

            modelBuilder.Entity("Api.Entities.Throw", b =>
                {
                    b.HasOne("Api.Entities.Profile", "Profile")
                        .WithMany("Throws")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Api.Entities.Profile", b =>
                {
                    b.Navigation("Throws");
                });
#pragma warning restore 612, 618
        }
    }
}
