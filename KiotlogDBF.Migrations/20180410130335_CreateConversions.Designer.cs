﻿// <auto-generated />
using System;
using KiotlogDBF.Context;
using KiotlogDBF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KiotlogDBF.Migrations
{
    [DbContext(typeof(KiotlogDBFContext))]
    [Migration("20180410130335_CreateConversions")]
    partial class CreateConversions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:pgcrypto", "'pgcrypto', '', ''")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.0-preview1-28290");

            modelBuilder.Entity("KiotlogDBF.Models.Conversions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Fun")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("fun")
                        .HasDefaultValueSql("'id'::text");

                    b.HasKey("Id")
                        .HasName("convertions_pkey");

                    b.ToTable("conversions");
                });

            modelBuilder.Entity("KiotlogDBF.Models.Devices", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<JsonTypes.DevicesAuth>("Auth")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<JsonTypes.DevicesAuth, string>(v => default(string), v => default(JsonTypes.DevicesAuth)))
                        .HasColumnName("auth")
                        .HasColumnType("jsonb")
                        .HasDefaultValueSql("json_build_object('klsn', json_build_object('key', encode(gen_random_bytes(32), 'base64')), 'basic', json_build_object('token', encode(gen_random_bytes(32), 'base64')))");

                    b.Property<string>("Device")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("device")
                        .HasDefaultValueSql("'device'");

                    b.Property<JsonTypes.DevicesFrame>("Frame")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<JsonTypes.DevicesFrame, string>(v => default(string), v => default(JsonTypes.DevicesFrame)))
                        .HasColumnName("frame")
                        .HasColumnType("jsonb")
                        .HasDefaultValueSql(" '{\"bigendian\": true, \"bitfields\": false}'::jsonb ");

                    b.Property<JsonTypes.DevicesMeta>("Meta")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<JsonTypes.DevicesMeta, string>(v => default(string), v => default(JsonTypes.DevicesMeta)))
                        .HasColumnName("meta")
                        .HasColumnType("jsonb")
                        .HasDefaultValueSql("'{}'::jsonb");

                    b.HasKey("Id")
                        .HasName("devices_pkey");

                    b.HasAlternateKey("Device")
                        .HasName("devices_device_key");

                    b.ToTable("devices");
                });

            modelBuilder.Entity("KiotlogDBF.Models.Points", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Data")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("data")
                        .HasColumnType("jsonb")
                        .HasDefaultValueSql("'{}'::jsonb");

                    b.Property<Guid>("DeviceId")
                        .HasColumnName("device_id");

                    b.Property<string>("Flags")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("flags")
                        .HasColumnType("jsonb")
                        .HasDefaultValueSql("'{}'::jsonb");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("time")
                        .HasColumnType("timestamptz")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id")
                        .HasName("points_pkey");

                    b.HasIndex("DeviceId");

                    b.ToTable("points");
                });

            modelBuilder.Entity("KiotlogDBF.Models.Sensors", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<Guid>("ConversionId")
                        .HasColumnName("conversion_id");

                    b.Property<Guid>("DeviceId")
                        .HasColumnName("device_id")
                        .HasColumnType("uuid");

                    b.Property<JsonTypes.SensorsFmt>("Fmt")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<JsonTypes.SensorsFmt, string>(v => default(string), v => default(JsonTypes.SensorsFmt)))
                        .HasColumnName("fmt")
                        .HasColumnType("jsonb")
                        .HasDefaultValueSql("'{}'::jsonb");

                    b.Property<JsonTypes.SensorsMeta>("Meta")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<JsonTypes.SensorsMeta, string>(v => default(string), v => default(JsonTypes.SensorsMeta)))
                        .HasColumnName("meta")
                        .HasColumnType("jsonb")
                        .HasDefaultValueSql("'{}'::jsonb");

                    b.Property<Guid>("SensorTypeId")
                        .HasColumnName("sensor_type_id");

                    b.HasKey("Id")
                        .HasName("sensors_pkey");

                    b.HasIndex("ConversionId");

                    b.HasIndex("DeviceId");

                    b.HasIndex("SensorTypeId");

                    b.ToTable("sensors");
                });

            modelBuilder.Entity("KiotlogDBF.Models.SensorTypes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<JsonTypes.SensorTypesMeta>("Meta")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<JsonTypes.SensorTypesMeta, string>(v => default(string), v => default(JsonTypes.SensorTypesMeta)))
                        .HasColumnName("meta")
                        .HasColumnType("jsonb")
                        .HasDefaultValueSql("'{}'::jsonb");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("name")
                        .HasDefaultValueSql("'generic'::text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("type")
                        .HasDefaultValueSql("'generic'::text");

                    b.HasKey("Id")
                        .HasName("sensor_types_pkey");

                    b.HasAlternateKey("Name")
                        .HasName("sensor_types_name_key");

                    b.ToTable("sensor_types");
                });

            modelBuilder.Entity("KiotlogDBF.Models.Points", b =>
                {
                    b.HasOne("KiotlogDBF.Models.Devices", "Device")
                        .WithMany("Points")
                        .HasForeignKey("DeviceId")
                        .HasConstraintName("points_device_fkey");
                });

            modelBuilder.Entity("KiotlogDBF.Models.Sensors", b =>
                {
                    b.HasOne("KiotlogDBF.Models.Conversions", "Conversion")
                        .WithMany("Sensors")
                        .HasForeignKey("ConversionId")
                        .HasConstraintName("sensors_conversion_fkey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KiotlogDBF.Models.Devices", "Device")
                        .WithMany("Sensors")
                        .HasForeignKey("DeviceId")
                        .HasConstraintName("sensors_device_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KiotlogDBF.Models.SensorTypes", "SensorType")
                        .WithMany("Sensors")
                        .HasForeignKey("SensorTypeId")
                        .HasConstraintName("sensors_sensor_type_fkey")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
