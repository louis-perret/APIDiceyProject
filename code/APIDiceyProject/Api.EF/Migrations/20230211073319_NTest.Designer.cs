﻿// <auto-generated />
using System;
using Api.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.EF.Migrations
{
    [DbContext(typeof(ApiDbContextStubbed))]
    [Migration("20230211073319_NTest")]
    partial class NTest
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            Id = new Guid("76a6268b-cc5f-4971-90d9-079f3720c0b9"),
                            Name = "Perret",
                            Surname = "Louis"
                        },
                        new
                        {
                            Id = new Guid("dcce4943-25ea-4929-a23a-427fd0a62cb7"),
                            Name = "Grienenberger",
                            Surname = "Côme"
                        },
                        new
                        {
                            Id = new Guid("6af0ae0f-fb3a-4604-810a-9517d8f6a741"),
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

                    b.Property<int>("Result")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("throws");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1c85e2ad-608d-4596-9885-43639928342f"),
                            DiceId = 2,
                            Result = 1
                        },
                        new
                        {
                            Id = new Guid("81486c85-a254-41ef-8c7f-25b9769bb89e"),
                            DiceId = 2,
                            Result = 2
                        },
                        new
                        {
                            Id = new Guid("660ed218-93a5-4bcf-a66c-8884a40f8633"),
                            DiceId = 4,
                            Result = 4
                        },
                        new
                        {
                            Id = new Guid("3274b913-13e7-482a-a264-f061f95bbfef"),
                            DiceId = 4,
                            Result = 3
                        },
                        new
                        {
                            Id = new Guid("a4e046a5-1bd1-4ab2-87dd-e6b77f8f5f3c"),
                            DiceId = 3,
                            Result = 3
                        },
                        new
                        {
                            Id = new Guid("358e1804-e7e5-47db-8567-538bc48073b5"),
                            DiceId = 6,
                            Result = 5
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
