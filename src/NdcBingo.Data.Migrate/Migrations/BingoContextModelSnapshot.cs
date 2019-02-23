﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NdcBingo.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NdcBingo.Data.Migrate.Migrations
{
    [DbContext(typeof(BingoContext))]
    partial class BingoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("NdcBingo.Data.Player", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_players");

                    b.ToTable("players");
                });

            modelBuilder.Entity("NdcBingo.Data.Square", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Text")
                        .HasColumnName("text");

                    b.HasKey("Id")
                        .HasName("pk_squares");

                    b.ToTable("squares");
                });

            modelBuilder.Entity("NdcBingo.Data.Talk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("EndTime")
                        .HasColumnName("end_time");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnName("start_time");

                    b.Property<int>("TimeZone")
                        .HasColumnName("time_zone");

                    b.HasKey("Id")
                        .HasName("pk_talks");

                    b.ToTable("talks");
                });
#pragma warning restore 612, 618
        }
    }
}
