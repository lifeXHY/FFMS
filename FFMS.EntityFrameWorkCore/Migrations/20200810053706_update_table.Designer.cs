﻿// <auto-generated />
using System;
using FFMS.EntityFrameWorkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FFMS.EntityFrameWorkCore.Migrations
{
    [DbContext(typeof(FFMSDbContext))]
    [Migration("20200810053706_update_table")]
    partial class update_table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FFMS.EntityFrameWorkCore.Entitys.BasUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisPlayName")
                        .HasColumnName("DisPlayName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("IfAdmin")
                        .HasColumnName("IfAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("PassWord")
                        .HasColumnName("PassWord")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasColumnName("UserName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("BasUser");
                });

            modelBuilder.Entity("FFMS.EntityFrameWorkCore.Entitys.InfoLog", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnName("Content")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("DTCerate")
                        .HasColumnName("DTCerate")
                        .HasColumnType("datetime");

                    b.Property<string>("Title")
                        .HasColumnName("Title")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("InfoLog");
                });
#pragma warning restore 612, 618
        }
    }
}
