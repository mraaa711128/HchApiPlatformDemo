using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HchApiPlatform.Models;

namespace HchApiPlatform.DbContexts
{
    public partial class HchPlatformContext : DbContext
    {
        public HchPlatformContext()
        {
        }

        public HchPlatformContext(DbContextOptions<HchPlatformContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdmitBedStat> AdmitBedStats { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PlatformDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdmitBedStat>(entity =>
            {
                entity.HasKey(e => e.BedNo)
                    .HasName("PK__AdmitBed__A8A227CFBA7FA9BA");

                entity.ToTable("AdmitBedStat");

                entity.Property(e => e.BedNo)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AdmitNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AdmitStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AdmitStatusDesc).HasMaxLength(20);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.ChartNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CheckinDateTime).HasColumnType("datetime");

                entity.Property(e => e.DivName).HasMaxLength(12);

                entity.Property(e => e.DivNo)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DoctorName).HasMaxLength(10);

                entity.Property(e => e.DoctorNo)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ExclusiveRoomFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ExclusiveRoomFlagDesc).HasMaxLength(12);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.IdNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IsolateType)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IsolateTypeDesc).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.NsCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NsName).HasMaxLength(20);

                entity.Property(e => e.PrivacyFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StatusDesc).HasMaxLength(20);

                entity.Property(e => e.WardNo)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
