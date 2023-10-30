﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PagamentosApi.Data;

#nullable disable

namespace PagamentosApi.Migrations
{
    [DbContext(typeof(PagamentoContext))]
    [Migration("20231026010734_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PagamentosApi.Models.Banco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CodigoDoBanco")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NomeDoBanco")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("PorcentualDeJuros")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CodigoDoBanco")
                        .IsUnique();

                    b.HasIndex("NomeDoBanco")
                        .IsUnique();

                    b.ToTable("Bancos");
                });

            modelBuilder.Entity("PagamentosApi.Models.Boleto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BancoId")
                        .HasColumnType("integer");

                    b.Property<string>("CPFOrCNPJ")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)");

                    b.Property<DateTime>("DataVencimento")
                        .HasColumnType("date");

                    b.Property<string>("NomeBeneficiario")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NomePagador")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Observacao")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<double>("Valor")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("BancoId");

                    b.ToTable("Boletos");
                });

            modelBuilder.Entity("PagamentosApi.Models.Boleto", b =>
                {
                    b.HasOne("PagamentosApi.Models.Banco", "Banco")
                        .WithMany("Boletos")
                        .HasForeignKey("BancoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Banco");
                });

            modelBuilder.Entity("PagamentosApi.Models.Banco", b =>
                {
                    b.Navigation("Boletos");
                });
#pragma warning restore 612, 618
        }
    }
}
