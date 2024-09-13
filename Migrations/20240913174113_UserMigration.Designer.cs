﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AnonKey_Backend.Migrations
{
  [DbContext(typeof(AnonKey_Backend.Models.APIContext))]
  [Migration("20240913174113_UserMigration")]
  partial class UserMigration
  {
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
      modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

      modelBuilder.Entity("User", b =>
          {
            b.Property<string>("UserUuid")
                      .HasColumnType("TEXT");

            b.Property<string>("DisplayName")
                      .IsRequired()
                      .HasColumnType("TEXT");

            b.HasKey("UserUuid");

            b.ToTable("Users");
          });
#pragma warning restore 612, 618
    }
  }
}
