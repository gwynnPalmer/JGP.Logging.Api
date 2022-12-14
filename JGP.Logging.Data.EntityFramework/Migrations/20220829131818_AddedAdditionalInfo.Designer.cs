// <auto-generated />
using System;
using JGP.Logging.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JGP.Logging.Data.EntityFramework.Migrations
{
    [DbContext(typeof(LogContext))]
    [Migration("20220829131818_AddedAdditionalInfo")]
    partial class AddedAdditionalInfo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("JGP.Logging.Core.AdditionalInfo", b =>
                {
                    b.Property<Guid>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("LogId");

                    b.Property<string>("Architecture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Architecture");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("IpAddress");

                    b.Property<string>("MachineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MachineName");

                    b.Property<string>("OperatingSystem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OperatingSystem");

                    b.Property<string>("OperatingSystemVersion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OperatingSystemVersion");

                    b.HasKey("LogId");

                    b.HasIndex("IpAddress")
                        .HasDatabaseName("IX_AdditionalInfo_IpAddress");

                    b.ToTable("AdditionalInfo", "dbo");
                });

            modelBuilder.Entity("JGP.Logging.Core.LogItem", b =>
                {
                    b.Property<Guid>("LogId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("LogId");

                    b.Property<string>("ErrorType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ErrorType");

                    b.Property<int>("LogLevel")
                        .HasColumnType("int")
                        .HasColumnName("LogLevel");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Message");

                    b.Property<DateTimeOffset>("OccurredOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2022, 8, 29, 13, 18, 18, 578, DateTimeKind.Unspecified).AddTicks(2555), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("OccurredOn");

                    b.Property<string>("Project")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Project");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Source");

                    b.Property<string>("StackTrace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("StackTrace");

                    b.HasKey("LogId");

                    b.HasIndex("ErrorType")
                        .HasDatabaseName("IX_LogItem_ErrorType");

                    b.HasIndex("Project")
                        .HasDatabaseName("IX_LogItem_Project");

                    b.ToTable("LogItems", "dbo");
                });

            modelBuilder.Entity("JGP.Logging.Core.LogItem", b =>
                {
                    b.HasOne("JGP.Logging.Core.AdditionalInfo", "AdditionalInfo")
                        .WithOne("LogItem")
                        .HasForeignKey("JGP.Logging.Core.LogItem", "LogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_AdditionalInfo_LogItems_LogId");

                    b.Navigation("AdditionalInfo");
                });

            modelBuilder.Entity("JGP.Logging.Core.AdditionalInfo", b =>
                {
                    b.Navigation("LogItem")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
