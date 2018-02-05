namespace KiotlogDBF

open Microsoft.EntityFrameworkCore

// https://stackoverflow.com/questions/5423768/c-sharp-to-f-ef-code-first

type KiotlogDBFContext (dbContextOptions: DbContextOptions<KiotlogDBFContext>) =
    inherit DbContext (dbContextOptions)

    // https://stackoverflow.com/questions/26775760/how-to-create-a-virtual-record-field-for-entity-framework-lazy-loading
    // abstract Devices : DbSet<Devices> with get, set
    // default val Devices = ctx.Set<Devices>() with get, set

    [<DefaultValue>] val mutable devices : DbSet<Devices>
    member public this.Devices with get() = this.devices and set(value) = this.devices <- value

    [<DefaultValue>] val mutable points : DbSet<Points>
    member public this.Points with get() = this.points and set(value) = this.points <- value

    [<DefaultValue>] val mutable sensors : DbSet<Sensors>
    member public this.Sensors with get() = this.sensors and set(value) = this.sensors <- value

    [<DefaultValue>] val mutable sensortypes : DbSet<SensorTypes>
    member public this.SensorTypes with get() = this.sensortypes and set(value) = this.sensortypes <- value

    [<DefaultValue>] val mutable conversions : DbSet<Conversions>
    member public this.Conversions with get() = this.conversions and set(value) = this.conversions <- value

    override __.OnConfiguring(optionsBuilder: DbContextOptionsBuilder) =
        if not optionsBuilder.IsConfigured then () else ()

    override __.OnModelCreating(modelBuilder: ModelBuilder) =
        modelBuilder.HasPostgresExtension("pgcrypto") |> ignore

        modelBuilder.Entity<Devices>(
            fun entity ->
                entity.HasKey("Id").HasName("devices_pkey") |> ignore
                entity.HasIndex("Device").HasName("devices_device_key").IsUnique |> ignore
                entity.Property("Id").HasDefaultValueSql("gen_random_uuid()") |> ignore
                entity.Property("auth").HasDefaultValueSql("'{}'::jsonb") |> ignore
                entity.Property("frame").HasDefaultValueSql("'{\"bigendian\": true, \"bitfields\": false}'::jsonb") |> ignore
                entity.Property("meta").HasDefaultValueSql("json_build_object('klsn', json_build_object('key', encode(gen_random_bytes(32), 'base64')), 'basic', json_build_object('token', encode(gen_random_bytes(32), 'base64')))") |> ignore
        ) |> ignore

        modelBuilder.Entity<Points>(
            fun entity ->
                entity.HasKey("Id").HasName("points_pkey") |> ignore
                entity.Property("Id").HasDefaultValueSql("gen_random_uuid()") |> ignore
                entity.Property("Data").HasDefaultValueSql("'{}'::jsonb") |> ignore
                entity.Property("Flags").HasDefaultValueSql("'{}'::jsonb") |> ignore
                entity.Property("Time").HasDefaultValueSql("now()") |> ignore
                // entity.HasOne(typedefof<Devices>, "Device").WithMany("Points").HasForeignKey("DeviceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("points_device_fkey") |> ignore
                entity.HasOne(fun p -> p.Device).WithMany("Points").HasForeignKey("DeviceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("points_device_fkey") |> ignore
            ) |> ignore

        modelBuilder.Entity<Sensors>(
            fun entity ->
                entity.HasKey("Id").HasName("sensors_pkey") |> ignore
                entity.Property("Id").HasDefaultValueSql("gen_random_uuid()")  |> ignore
                entity.Property("fmt").HasDefaultValueSql("'{}'::jsonb")  |> ignore
                entity.Property("meta").HasDefaultValueSql("'{}'::jsonb")  |> ignore
                entity.HasOne(fun s -> s.Device).WithMany("Sensors").HasForeignKey("DeviceId").HasConstraintName("sensors_device_id_fkey") |> ignore
                entity.HasOne(fun s -> s.SensorType).WithMany("Sensors").HasForeignKey("SensorTypeId").HasConstraintName("sensors_sensor_type_fkey")  |> ignore
                entity.HasOne(fun s -> s.Conversion).WithMany("Sensors").HasForeignKey("ConversionId").HasConstraintName("sensors_conversion_fkey") |> ignore
            ) |> ignore

        modelBuilder.Entity<SensorTypes>(
            fun entity ->
                entity.HasKey("Id").HasName("sensor_types_pkey") |> ignore
                entity.HasIndex("Name").HasName("sensor_types_name_key").IsUnique()  |> ignore
                entity.Property("Id").HasDefaultValueSql("gen_random_uuid()")  |> ignore
                entity.Property("meta").HasDefaultValueSql("'{}'::jsonb")  |> ignore
                entity.Property("Name").HasDefaultValueSql("'generic'::text") |> ignore
                entity.Property("Type").HasDefaultValueSql("'generic'::text") |> ignore
        ) |> ignore

        modelBuilder.Entity<Conversions>(
            fun entity ->
                entity.HasKey("Id").HasName("convertions_pkey") |> ignore
                entity.Property("Id").HasDefaultValueSql("gen_random_uuid()")  |> ignore
                entity.Property("Fun").HasDefaultValueSql("'id'::text") |> ignore
        ) |> ignore
