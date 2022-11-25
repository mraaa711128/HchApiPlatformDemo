using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HchApiPlatform.Models;

namespace HchApiPlatform.DbContexts
{
    public partial class UnimaxHoContext : DbContext
    {
        public UnimaxHoContext()
        {
        }

        public UnimaxHoContext(DbContextOptions<UnimaxHoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chart> Charts { get; set; } = null!;
        public virtual DbSet<Div> Divs { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("User ID=MAST;Password=MAST;Data Source=192.168.10.66:1521/HO");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MAST");

            modelBuilder.Entity<Chart>(entity =>
            {
                entity.HasKey(e => e.ChartNo);

                entity.ToTable("CHART", "CHART");

                entity.HasIndex(e => new { e.BirthDate, e.ChartNo }, "AK_CHART_BIRTHDATE");

                entity.HasIndex(e => new { e.IdNo, e.IdNoFlag, e.ChartNo }, "AK_CHART_ID");

                entity.HasIndex(e => new { e.PtName, e.ChartNo }, "AK_CHART_PTNAME");

                entity.Property(e => e.ChartNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CHART_NO")
                    .IsFixedLength();

                entity.Property(e => e.AbsentTimes)
                    .HasPrecision(3)
                    .HasColumnName("ABSENT_TIMES");

                entity.Property(e => e.ActivityFlag)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ACTIVITY_FLAG");

                entity.Property(e => e.ActivityFlagDate)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("ACTIVITY_FLAG_DATE")
                    .IsFixedLength();

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.AdmitBedNo)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("ADMIT_BED_NO")
                    .IsFixedLength();

                entity.Property(e => e.AdmitPtType)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ADMIT_PT_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.Agent)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("AGENT");

                entity.Property(e => e.AreaCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("AREA_CODE")
                    .IsFixedLength();

                entity.Property(e => e.BabyNbFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BABY_NB_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.BirthDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("BIRTH_DATE")
                    .IsFixedLength();

                entity.Property(e => e.BirthPlace)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("BIRTH_PLACE")
                    .IsFixedLength();

                entity.Property(e => e.BirthUnknowFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BIRTH_UNKNOW_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.BloodType)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("BLOOD_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.CreateClerk)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("CREATE_CLERK")
                    .IsFixedLength();

                entity.Property(e => e.CreateDate)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("CREATE_DATE")
                    .IsFixedLength();

                entity.Property(e => e.CreateTime)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CREATE_TIME")
                    .IsFixedLength();

                entity.Property(e => e.DentistFvFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DENTIST_FV_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.DiscntType)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DISCNT_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.DownloadAppFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DOWNLOAD_APP_FLAG");

                entity.Property(e => e.EMail)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("E_MAIL");

                entity.Property(e => e.EducationFlag)
                    .HasPrecision(1)
                    .HasColumnName("EDUCATION_FLAG");

                entity.Property(e => e.EmgContactor)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("EMG_CONTACTOR")
                    .IsFixedLength();

                entity.Property(e => e.EmgContactor1)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("EMG_CONTACTOR_1");

                entity.Property(e => e.EmgContactor2)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("EMG_CONTACTOR_2");

                entity.Property(e => e.EmgContactorRelation)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("EMG_CONTACTOR_RELATION")
                    .IsFixedLength();

                entity.Property(e => e.EmgContactorRelation1)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("EMG_CONTACTOR_RELATION_1");

                entity.Property(e => e.EmgContactorRelation2)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("EMG_CONTACTOR_RELATION_2");

                entity.Property(e => e.EmgContactorTel)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EMG_CONTACTOR_TEL");

                entity.Property(e => e.EmgContactorTel1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EMG_CONTACTOR_TEL_1");

                entity.Property(e => e.EmgContactorTel11)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EMG_CONTACTOR_TEL1");

                entity.Property(e => e.EmgContactorTel111)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EMG_CONTACTOR_TEL1_1");

                entity.Property(e => e.EmgContactorTel12)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EMG_CONTACTOR_TEL1_2");

                entity.Property(e => e.EmgContactorTel2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EMG_CONTACTOR_TEL_2");

                entity.Property(e => e.ExpiredDate)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("EXPIRED_DATE")
                    .IsFixedLength();

                entity.Property(e => e.ExpiredSourceFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("EXPIRED_SOURCE_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.ExpiredVs)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("EXPIRED_VS")
                    .IsFixedLength();

                entity.Property(e => e.FilingArea)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("FILING_AREA");

                entity.Property(e => e.FilingAreaDate)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("FILING_AREA_DATE")
                    .IsFixedLength();

                entity.Property(e => e.FilingAreaRemark)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FILING_AREA_REMARK")
                    .IsFixedLength();

                entity.Property(e => e.FirstViewDate)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_VIEW_DATE")
                    .IsFixedLength();

                entity.Property(e => e.HomeAddress)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("HOME_ADDRESS");

                entity.Property(e => e.HomeAreaCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("HOME_AREA_CODE")
                    .IsFixedLength();

                entity.Property(e => e.HospBookFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("HOSP_BOOK_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.IdNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_NO")
                    .IsFixedLength();

                entity.Property(e => e.IdNo2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_NO2");

                entity.Property(e => e.IdNoFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ID_NO_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.LastAdmitDate)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("LAST_ADMIT_DATE")
                    .IsFixedLength();

                entity.Property(e => e.LastDischargeDate)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("LAST_DISCHARGE_DATE")
                    .IsFixedLength();

                entity.Property(e => e.LastDischargeDiv)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("LAST_DISCHARGE_DIV")
                    .IsFixedLength();

                entity.Property(e => e.LastDiseaseCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("LAST_DISEASE_CODE")
                    .IsFixedLength();

                entity.Property(e => e.LastOpdDate)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("LAST_OPD_DATE")
                    .IsFixedLength();

                entity.Property(e => e.LastOpdDiv)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("LAST_OPD_DIV")
                    .IsFixedLength();

                entity.Property(e => e.LastSeparateDate)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("LAST_SEPARATE_DATE")
                    .IsFixedLength();

                entity.Property(e => e.MarriageStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MARRIAGE_STATUS")
                    .IsFixedLength();

                entity.Property(e => e.MergeDate)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("MERGE_DATE")
                    .IsFixedLength();

                entity.Property(e => e.MotherChartNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MOTHER_CHART_NO")
                    .IsFixedLength();

                entity.Property(e => e.OccupationType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("OCCUPATION_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.OccupationTypeOther)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("OCCUPATION_TYPE_OTHER");

                entity.Property(e => e.OriginTown)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ORIGIN_TOWN")
                    .IsFixedLength();

                entity.Property(e => e.PrimaryLanguage)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PRIMARY_LANGUAGE")
                    .IsFixedLength();

                entity.Property(e => e.PromptByEmailFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PROMPT_BY_EMAIL_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.PromptByMobileFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PROMPT_BY_MOBILE_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.Pronunciation)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PRONUNCIATION");

                entity.Property(e => e.PtName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PT_NAME")
                    .IsFixedLength();

                entity.Property(e => e.PtNameBase64)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("PT_NAME_BASE64");

                entity.Property(e => e.PtNameEnglish)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("PT_NAME_ENGLISH")
                    .IsFixedLength();

                entity.Property(e => e.PtType)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("PT_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.Ptremark1)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("PTREMARK_1")
                    .IsFixedLength();

                entity.Property(e => e.Ptremark2)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("PTREMARK_2")
                    .IsFixedLength();

                entity.Property(e => e.Ptremark3)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("PTREMARK_3")
                    .IsFixedLength();

                entity.Property(e => e.Ptremark4)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("PTREMARK_4")
                    .IsFixedLength();

                entity.Property(e => e.Ptremark5)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("PTREMARK_5")
                    .IsFixedLength();

                entity.Property(e => e.ScrapClerk)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("SCRAP_CLERK")
                    .IsFixedLength();

                entity.Property(e => e.ScrapDate)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("SCRAP_DATE")
                    .IsFixedLength();

                entity.Property(e => e.ScrapTime)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("SCRAP_TIME")
                    .IsFixedLength();

                entity.Property(e => e.SeparateQty)
                    .HasPrecision(2)
                    .HasColumnName("SEPARATE_QTY");

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SEX")
                    .IsFixedLength();

                entity.Property(e => e.SourceChartNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SOURCE_CHART_NO")
                    .IsFixedLength();

                entity.Property(e => e.SpecialMrFlag)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SPECIAL_MR_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.SpouseChartNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SPOUSE_CHART_NO");

                entity.Property(e => e.TaipeiChildCardNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TAIPEI_CHILD_CARD_NO")
                    .IsFixedLength();

                entity.Property(e => e.TargetChartNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TARGET_CHART_NO")
                    .IsFixedLength();

                entity.Property(e => e.TelHome)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TEL_HOME");

                entity.Property(e => e.TelMobile)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TEL_MOBILE")
                    .IsFixedLength();

                entity.Property(e => e.TelOffice)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TEL_OFFICE");

                entity.Property(e => e.TempMrFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TEMP_MR_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.VipDiscntType)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("VIP_DISCNT_TYPE");
            });

            modelBuilder.Entity<Div>(entity =>
            {
                entity.HasKey(e => e.DivNo);

                entity.ToTable("DIV");

                entity.Property(e => e.DivNo)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DIV_NO")
                    .IsFixedLength();

                entity.Property(e => e.AdmitFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ADMIT_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.CostCenterFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("COST_CENTER_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.DclinicFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DCLINIC_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("DEPT_CODE");

                entity.Property(e => e.DivFullName)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("DIV_FULL_NAME")
                    .IsFixedLength();

                entity.Property(e => e.DivFvWeight)
                    .HasColumnType("NUMBER(3,1)")
                    .HasColumnName("DIV_FV_WEIGHT");

                entity.Property(e => e.DivNo5)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DIV_NO_5")
                    .IsFixedLength();

                entity.Property(e => e.DivProg)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DIV_PROG")
                    .IsFixedLength();

                entity.Property(e => e.DivRevenueBelong)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DIV_REVENUE_BELONG")
                    .IsFixedLength();

                entity.Property(e => e.DivShortName)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("DIV_SHORT_NAME")
                    .IsFixedLength();

                entity.Property(e => e.DivSystem)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("DIV_SYSTEM")
                    .IsFixedLength();

                entity.Property(e => e.EnglishFullName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("ENGLISH_FULL_NAME");

                entity.Property(e => e.EnglishShortName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ENGLISH_SHORT_NAME");

                entity.Property(e => e.ErDeptCode)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("ER_DEPT_CODE");

                entity.Property(e => e.InjErDeptCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("INJ_ER_DEPT_CODE")
                    .IsFixedLength();

                entity.Property(e => e.InjOpdDeptCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("INJ_OPD_DEPT_CODE")
                    .IsFixedLength();

                entity.Property(e => e.IpdMedServiceFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("IPD_MED_SERVICE_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.NhDivFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NH_DIV_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.NhDivSystem)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NH_DIV_SYSTEM")
                    .IsFixedLength();

                entity.Property(e => e.NhIpdDiv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("NH_IPD_DIV")
                    .IsFixedLength();

                entity.Property(e => e.NhOpdDiv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("NH_OPD_DIV")
                    .IsFixedLength();

                entity.Property(e => e.OpdChargePermitFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("OPD_CHARGE_PERMIT_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.OpdDeptCode)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("OPD_DEPT_CODE");

                entity.Property(e => e.ParentDivNo)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("PARENT_DIV_NO")
                    .IsFixedLength();

                entity.Property(e => e.StatDays)
                    .HasPrecision(2)
                    .HasColumnName("STAT_DAYS");

                entity.Property(e => e.TraFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TRA_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.VrgDivNo)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("VRG_DIV_NO")
                    .HasDefaultValueSql("' '")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorNo)
                    .HasName("PK_DOCTORKS");

                entity.ToTable("DOCTOR");

                entity.Property(e => e.DoctorNo)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("DOCTOR_NO")
                    .IsFixedLength();

                entity.Property(e => e.AnticariousEndDate)
                    .HasPrecision(7)
                    .HasColumnName("ANTICARIOUS_END_DATE");

                entity.Property(e => e.AnticariousStartDate)
                    .HasPrecision(7)
                    .HasColumnName("ANTICARIOUS_START_DATE");

                entity.Property(e => e.CertificateNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CERTIFICATE_NO")
                    .IsFixedLength();

                entity.Property(e => e.CertificateNo1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CERTIFICATE_NO_1")
                    .IsFixedLength();

                entity.Property(e => e.CertificateNo2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CERTIFICATE_NO_2")
                    .IsFixedLength();

                entity.Property(e => e.CertificateNo3)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CERTIFICATE_NO_3")
                    .IsFixedLength();

                entity.Property(e => e.CertificateNo4)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CERTIFICATE_NO_4")
                    .IsFixedLength();

                entity.Property(e => e.CertificateNo5)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CERTIFICATE_NO_5")
                    .IsFixedLength();

                entity.Property(e => e.ChildAnticariousFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CHILD_ANTICARIOUS_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.ControlMedPermitNo)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("CONTROL_MED_PERMIT_NO")
                    .IsFixedLength();

                entity.Property(e => e.DivNo)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DIV_NO")
                    .IsFixedLength();

                entity.Property(e => e.DivNo1)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DIV_NO_1")
                    .IsFixedLength();

                entity.Property(e => e.DivNo2)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DIV_NO_2")
                    .IsFixedLength();

                entity.Property(e => e.DivNo3)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DIV_NO_3")
                    .IsFixedLength();

                entity.Property(e => e.DivNo4)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DIV_NO_4")
                    .IsFixedLength();

                entity.Property(e => e.DivNo5)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DIV_NO_5")
                    .IsFixedLength();

                entity.Property(e => e.DoctorGrade)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("DOCTOR_GRADE")
                    .IsFixedLength();

                entity.Property(e => e.DoctorName)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DOCTOR_NAME")
                    .IsFixedLength();

                entity.Property(e => e.DoctorPharmacistFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DOCTOR_PHARMACIST_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.EndDate)
                    .HasPrecision(7)
                    .HasColumnName("END_DATE");

                entity.Property(e => e.EndDate1)
                    .HasPrecision(7)
                    .HasColumnName("END_DATE_1");

                entity.Property(e => e.EndDate2)
                    .HasPrecision(7)
                    .HasColumnName("END_DATE_2");

                entity.Property(e => e.EndDate3)
                    .HasPrecision(7)
                    .HasColumnName("END_DATE_3");

                entity.Property(e => e.EndDate4)
                    .HasPrecision(7)
                    .HasColumnName("END_DATE_4");

                entity.Property(e => e.EndDate5)
                    .HasPrecision(7)
                    .HasColumnName("END_DATE_5");

                entity.Property(e => e.EnglishName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("ENGLISH_NAME")
                    .IsFixedLength();

                entity.Property(e => e.HospDoctor)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("HOSP_DOCTOR")
                    .IsFixedLength();

                entity.Property(e => e.IdNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_NO")
                    .IsFixedLength();

                entity.Property(e => e.IpdClinicFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("IPD_CLINIC_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.LicenseNo)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("LICENSE_NO")
                    .IsFixedLength();

                entity.Property(e => e.NhApplyEndApn)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NH_APPLY_END_APN")
                    .IsFixedLength();

                entity.Property(e => e.NhApplyEndApn2)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NH_APPLY_END_APN_2")
                    .IsFixedLength();

                entity.Property(e => e.NhApplyEndDate)
                    .HasPrecision(7)
                    .HasColumnName("NH_APPLY_END_DATE");

                entity.Property(e => e.NhApplyEndDate2)
                    .HasPrecision(7)
                    .HasColumnName("NH_APPLY_END_DATE_2");

                entity.Property(e => e.NhApplyStartApn)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NH_APPLY_START_APN")
                    .IsFixedLength();

                entity.Property(e => e.NhApplyStartApn2)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NH_APPLY_START_APN_2")
                    .IsFixedLength();

                entity.Property(e => e.NhApplyStartDate)
                    .HasPrecision(7)
                    .HasColumnName("NH_APPLY_START_DATE");

                entity.Property(e => e.NhApplyStartDate2)
                    .HasPrecision(7)
                    .HasColumnName("NH_APPLY_START_DATE_2");

                entity.Property(e => e.NhDoctorNo)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("NH_DOCTOR_NO")
                    .IsFixedLength();

                entity.Property(e => e.OpdClinicFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("OPD_CLINIC_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.ProjectAutoFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PROJECT_AUTO_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.Pronunciation)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PRONUNCIATION")
                    .HasDefaultValueSql("' '")
                    .IsFixedLength();

                entity.Property(e => e.StartDate)
                    .HasPrecision(7)
                    .HasColumnName("START_DATE");

                entity.Property(e => e.StartDate1)
                    .HasPrecision(7)
                    .HasColumnName("START_DATE_1");

                entity.Property(e => e.StartDate2)
                    .HasPrecision(7)
                    .HasColumnName("START_DATE_2");

                entity.Property(e => e.StartDate3)
                    .HasPrecision(7)
                    .HasColumnName("START_DATE_3");

                entity.Property(e => e.StartDate4)
                    .HasPrecision(7)
                    .HasColumnName("START_DATE_4");

                entity.Property(e => e.StartDate5)
                    .HasPrecision(7)
                    .HasColumnName("START_DATE_5");

                entity.Property(e => e.SuspendFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SUSPEND_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.VrgDoctorNo)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("VRG_DOCTOR_NO")
                    .HasDefaultValueSql("' '")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
