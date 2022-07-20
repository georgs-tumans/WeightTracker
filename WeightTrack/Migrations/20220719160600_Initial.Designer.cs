﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeightTrack.Data;

#nullable disable

namespace WeightTrack.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220719160600_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("WeightTrack.Data.DB_Models.WeightEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT")
                        .HasComment("An optional description of the weight entry");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("WeightEntries");
                });

            modelBuilder.Entity("WeightTrack.Data.DB_Models.WeightTarget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT")
                        .HasComment("An optional description of the target entry");

                    b.Property<DateTime>("TargetDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("TargetWeight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("WeightTargets");
                });
#pragma warning restore 612, 618
        }
    }
}
