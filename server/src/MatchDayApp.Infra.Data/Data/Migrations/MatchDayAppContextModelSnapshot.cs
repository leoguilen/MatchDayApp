﻿// <auto-generated />
using System;
using MatchDayApp.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MatchDayApp.Infra.Data.Data.Migrations
{
    [DbContext(typeof(MatchDayAppContext))]
    partial class MatchDayAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MatchDayApp.Domain.Entidades.ConfirmacaoEmail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChaveConfirmacao")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Confirmado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RequisicaoEm")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("ConfirmacaoEmails");
                });

            modelBuilder.Entity("MatchDayApp.Domain.Entidades.Partida", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataPartida")
                        .HasColumnType("datetime2");

                    b.Property<int>("HorasPartida")
                        .HasColumnType("int");

                    b.Property<bool>("PrimeiroTimeConfirmado")
                        .HasColumnType("bit");

                    b.Property<Guid>("PrimeiroTimeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuadraFutebolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("SegundoTimeConfirmado")
                        .HasColumnType("bit");

                    b.Property<Guid>("SegundoTimeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StatusPartida")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PrimeiroTimeId");

                    b.HasIndex("QuadraFutebolId");

                    b.HasIndex("SegundoTimeId");

                    b.ToTable("Partida");
                });

            modelBuilder.Entity("MatchDayApp.Domain.Entidades.QuadraFutebol", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Imagem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<double>("PrecoHora")
                        .HasColumnType("float");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.Property<Guid>("UsuarioProprietarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioProprietarioId");

                    b.ToTable("QuadraFutebol");
                });

            modelBuilder.Entity("MatchDayApp.Domain.Entidades.Time", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("Imagem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("QtdIntegrantes")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioProprietarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioProprietarioId");

                    b.ToTable("Time");
                });

            modelBuilder.Entity("MatchDayApp.Domain.Entidades.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deletado")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("EmailConfirmado")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Usuario");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6e9fe21f-9406-42f0-a912-c2262d55b14c"),
                            Avatar = "",
                            CriadoEm = new DateTime(2020, 12, 24, 17, 8, 56, 904, DateTimeKind.Local).AddTicks(7447),
                            Deletado = false,
                            Email = "desenvolvimento.dev1@gmail.com",
                            EmailConfirmado = true,
                            Nome = "Administrador",
                            Salt = "ugYNNnYEnlQ=",
                            Senha = "jH/mbPAdoAPAbKdk2OB0fOOIgGxLffK8X3ADOusg4sE=",
                            Sobrenome = "Master",
                            Telefone = "+55 (11)1020-3040",
                            TipoUsuario = 3,
                            Username = "admin.master"
                        });
                });

            modelBuilder.Entity("MatchDayApp.Domain.Entidades.UsuarioTime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TimeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Aceito")
                        .HasColumnType("bit");

                    b.HasKey("Id", "UsuarioId", "TimeId");

                    b.HasIndex("TimeId");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("UsuarioTime");
                });

            modelBuilder.Entity("MatchDayApp.Domain.Entidades.Partida", b =>
                {
                    b.HasOne("MatchDayApp.Domain.Entidades.Time", "PrimeiroTime")
                        .WithMany()
                        .HasForeignKey("PrimeiroTimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MatchDayApp.Domain.Entidades.QuadraFutebol", "QuadraFutebol")
                        .WithMany()
                        .HasForeignKey("QuadraFutebolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MatchDayApp.Domain.Entidades.Time", "SegundoTime")
                        .WithMany()
                        .HasForeignKey("SegundoTimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MatchDayApp.Domain.Entidades.QuadraFutebol", b =>
                {
                    b.HasOne("MatchDayApp.Domain.Entidades.Usuario", "UsuarioProprietario")
                        .WithMany()
                        .HasForeignKey("UsuarioProprietarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MatchDayApp.Domain.Entidades.Time", b =>
                {
                    b.HasOne("MatchDayApp.Domain.Entidades.Usuario", "UsuarioProprietario")
                        .WithMany()
                        .HasForeignKey("UsuarioProprietarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MatchDayApp.Domain.Entidades.UsuarioTime", b =>
                {
                    b.HasOne("MatchDayApp.Domain.Entidades.Time", "Time")
                        .WithMany()
                        .HasForeignKey("TimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MatchDayApp.Domain.Entidades.Usuario", null)
                        .WithOne("UsuarioTime")
                        .HasForeignKey("MatchDayApp.Domain.Entidades.UsuarioTime", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
