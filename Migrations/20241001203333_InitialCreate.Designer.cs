﻿// <auto-generated />
using System;
using Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace books_api.Migrations
{
    [DbContext(typeof(PostgresContext))]
    [Migration("20241001203333_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.1.24451.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Model.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .HasColumnType("text")
                        .HasColumnName("author");

                    b.Property<int?>("BorrowedBooksId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateRelase")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("daterelase");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsActived")
                        .HasColumnType("boolean")
                        .HasColumnName("isactived");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("BorrowedBooksId");

                    b.ToTable("book", (string)null);
                });

            modelBuilder.Entity("Model.BorrowedBooks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateBorrowed")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_borrowed");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("return_date");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("borrowedbooks", (string)null);
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("IsActived")
                        .HasColumnType("boolean")
                        .HasColumnName("isactived");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("Model.Book", b =>
                {
                    b.HasOne("Model.BorrowedBooks", null)
                        .WithMany("Books")
                        .HasForeignKey("BorrowedBooksId");
                });

            modelBuilder.Entity("Model.BorrowedBooks", b =>
                {
                    b.HasOne("Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.BorrowedBooks", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
