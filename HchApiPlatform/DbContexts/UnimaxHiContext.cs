using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HchApiPlatform.Models;

namespace HchApiPlatform.DbContexts
{
    public partial class UnimaxHiContext : DbContext
    {
        public UnimaxHiContext()
        {
        }

        public UnimaxHiContext(DbContextOptions<UnimaxHiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bed> Beds { get; set; } = null!;
        public virtual DbSet<Bedisolate> Bedisolates { get; set; } = null!;
        public virtual DbSet<N> Ns { get; set; } = null!;
        public virtual DbSet<Ptipd> Ptipds { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("User ID=MAST;Password=MAST;Data Source=192.168.10.67:1521/HI");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MAST");

            modelBuilder.Entity<Bed>(entity =>
            {
                entity.HasKey(e => new { e.BedNo, e.EffectiveDateC });

                entity.ToTable("BED", "IPD");

                entity.HasIndex(e => new { e.NsCode, e.WardNo, e.BedNo }, "AK_BED_2_NS_CODE");

                entity.HasIndex(e => e.NhBedNo, "AK_BED_3_NH_BED_NO");

                entity.Property(e => e.BedNo)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("BED_NO")
                    .IsFixedLength();

                entity.Property(e => e.EffectiveDateC)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("EFFECTIVE_DATE_C")
                    .IsFixedLength();

                entity.Property(e => e.AutoAssignFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("AUTO_ASSIGN_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.DedicatedBedFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DEDICATED_BED_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.DivNo)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DIV_NO")
                    .IsFixedLength();

                entity.Property(e => e.EffectiveDate)
                    .HasPrecision(7)
                    .HasColumnName("EFFECTIVE_DATE");

                entity.Property(e => e.GradeCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("GRADE_CODE")
                    .IsFixedLength();

                entity.Property(e => e.Location)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION")
                    .IsFixedLength();

                entity.Property(e => e.MedServiceFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MED_SERVICE_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.NhBedNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("NH_BED_NO")
                    .IsFixedLength();

                entity.Property(e => e.NsCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("NS_CODE")
                    .IsFixedLength();

                entity.Property(e => e.StatisticFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATISTIC_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.SuspendDate)
                    .HasPrecision(7)
                    .HasColumnName("SUSPEND_DATE");

                entity.Property(e => e.VirtualBedFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VIRTUAL_BED_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.WardNo)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("WARD_NO")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Bedisolate>(entity =>
            {
                entity.HasKey(e => e.IsolateType);

                entity.ToTable("BEDISOLATE", "IPD");

                entity.Property(e => e.IsolateType)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ISOLATE_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.IsolateDescription)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ISOLATE_DESCRIPTION")
                    .IsFixedLength();

                entity.Property(e => e.IsolateFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ISOLATE_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.ProgIsolateType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PROG_ISOLATE_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.SuspendFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SUSPEND_FLAG")
                    .IsFixedLength();
            });

            modelBuilder.Entity<N>(entity =>
            {
                entity.HasKey(e => e.NsCode);

                entity.ToTable("NS", "IPD");

                entity.Property(e => e.NsCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("NS_CODE")
                    .IsFixedLength();

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("DEPT_CODE")
                    .IsFixedLength();

                entity.Property(e => e.HospLocation)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("HOSP_LOCATION")
                    .IsFixedLength();

                entity.Property(e => e.IdeSpreadStartTime)
                    .HasPrecision(4)
                    .HasColumnName("IDE_SPREAD_START_TIME");

                entity.Property(e => e.NsEnglishName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NS_ENGLISH_NAME")
                    .IsFixedLength();

                entity.Property(e => e.NsName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NS_NAME")
                    .IsFixedLength();

                entity.Property(e => e.NsType)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("NS_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.OpenFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("OPEN_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.UddAutoDcFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("UDD_AUTO_DC_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.UddDeliverTime)
                    .HasPrecision(4)
                    .HasColumnName("UDD_DELIVER_TIME");

                entity.Property(e => e.UddFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("UDD_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.UddProcessOrder)
                    .HasPrecision(2)
                    .HasColumnName("UDD_PROCESS_ORDER");
            });

            modelBuilder.Entity<Ptipd>(entity =>
            {
                entity.HasKey(e => new { e.AdmitNo, e.CutNo });

                entity.ToTable("PTIPD", "IPD");

                entity.Property(e => e.AdmitNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ADMIT_NO")
                    .IsFixedLength();

                entity.Property(e => e.CutNo)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CUT_NO")
                    .IsFixedLength();

                entity.Property(e => e.AccidentFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ACCIDENT_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.AddWardFeeFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ADD_WARD_FEE_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.AdmitAgreementFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ADMIT_AGREEMENT_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.AdmitDate)
                    .HasPrecision(7)
                    .HasColumnName("ADMIT_DATE");

                entity.Property(e => e.AdmitDivNo)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("ADMIT_DIV_NO")
                    .IsFixedLength();

                entity.Property(e => e.AdmitIndexCardNo)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ADMIT_INDEX_CARD_NO")
                    .IsFixedLength();

                entity.Property(e => e.AdmitOrigin)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ADMIT_ORIGIN")
                    .IsFixedLength();

                entity.Property(e => e.AdmitRemark)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("ADMIT_REMARK")
                    .IsFixedLength();

                entity.Property(e => e.AdmitTime)
                    .HasPrecision(6)
                    .HasColumnName("ADMIT_TIME");

                entity.Property(e => e.Alias)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ALIAS")
                    .IsFixedLength();

                entity.Property(e => e.ApplyDivNo)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("APPLY_DIV_NO")
                    .IsFixedLength();

                entity.Property(e => e.ApplyDoctorNo)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("APPLY_DOCTOR_NO")
                    .IsFixedLength();

                entity.Property(e => e.AssignedDoctorNo)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ASSIGNED_DOCTOR_NO")
                    .IsFixedLength();

                entity.Property(e => e.BabyFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BABY_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.BcClinicalStage)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BC_CLINICAL_STAGE")
                    .IsFixedLength();

                entity.Property(e => e.BcOrigin)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BC_ORIGIN")
                    .IsFixedLength();

                entity.Property(e => e.BedNo)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("BED_NO")
                    .IsFixedLength();

                entity.Property(e => e.BillingSeq)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("BILLING_SEQ")
                    .IsFixedLength();

                entity.Property(e => e.BirthDate)
                    .HasPrecision(7)
                    .HasColumnName("BIRTH_DATE");

                entity.Property(e => e.BurdenCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("BURDEN_CODE")
                    .IsFixedLength();

                entity.Property(e => e.ChartNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CHART_NO")
                    .IsFixedLength();

                entity.Property(e => e.CnsNo)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("CNS_NO")
                    .IsFixedLength();

                entity.Property(e => e.ConsultCount)
                    .HasPrecision(3)
                    .HasColumnName("CONSULT_COUNT");

                entity.Property(e => e.CutFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CUT_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.CvaStartDate)
                    .HasPrecision(7)
                    .HasColumnName("CVA_START_DATE");

                entity.Property(e => e.DiagnosisDischargeDate)
                    .HasPrecision(7)
                    .HasColumnName("DIAGNOSIS_DISCHARGE_DATE");

                entity.Property(e => e.DischargeDate)
                    .HasPrecision(7)
                    .HasColumnName("DISCHARGE_DATE");

                entity.Property(e => e.DischargeMedNo)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("DISCHARGE_MED_NO")
                    .IsFixedLength();

                entity.Property(e => e.DischargeReason)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DISCHARGE_REASON")
                    .IsFixedLength();

                entity.Property(e => e.DischargeRemark)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("DISCHARGE_REMARK")
                    .IsFixedLength();

                entity.Property(e => e.DischargeTime)
                    .HasPrecision(6)
                    .HasColumnName("DISCHARGE_TIME");

                entity.Property(e => e.DiscntType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DISCNT_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.DivNo)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DIV_NO")
                    .IsFixedLength();

                entity.Property(e => e.DocumentNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DOCUMENT_NO")
                    .IsFixedLength();

                entity.Property(e => e.DrgCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("DRG_CODE")
                    .IsFixedLength();

                entity.Property(e => e.ExclusiveWardFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("EXCLUSIVE_WARD_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.ExpectedDischargeDate)
                    .HasPrecision(7)
                    .HasColumnName("EXPECTED_DISCHARGE_DATE");

                entity.Property(e => e.ExpectedDischargeTime)
                    .HasPrecision(6)
                    .HasColumnName("EXPECTED_DISCHARGE_TIME");

                entity.Property(e => e.FeeEndDate)
                    .HasPrecision(7)
                    .HasColumnName("FEE_END_DATE");

                entity.Property(e => e.FeeEndTime)
                    .HasPrecision(6)
                    .HasColumnName("FEE_END_TIME");

                entity.Property(e => e.FeeStartDate)
                    .HasPrecision(7)
                    .HasColumnName("FEE_START_DATE");

                entity.Property(e => e.FeeStartTime)
                    .HasPrecision(6)
                    .HasColumnName("FEE_START_TIME");

                entity.Property(e => e.FixFeeCloseDate)
                    .HasPrecision(7)
                    .HasColumnName("FIX_FEE_CLOSE_DATE");

                entity.Property(e => e.FixfeeSeq)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("FIXFEE_SEQ")
                    .IsFixedLength();

                entity.Property(e => e.FreePrepaidFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("FREE_PREPAID_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.IdCardFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ID_CARD_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.IdsCareStage)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("IDS_CARE_STAGE")
                    .IsFixedLength();

                entity.Property(e => e.IdsPaidStage)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("IDS_PAID_STAGE")
                    .IsFixedLength();

                entity.Property(e => e.IsolateType)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ISOLATE_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.LastKorderDate)
                    .HasPrecision(7)
                    .HasColumnName("LAST_KORDER_DATE");

                entity.Property(e => e.LastModifiedClerk)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("LAST_MODIFIED_CLERK")
                    .IsFixedLength();

                entity.Property(e => e.LastModifiedDate)
                    .HasPrecision(7)
                    .HasColumnName("LAST_MODIFIED_DATE");

                entity.Property(e => e.LastModifiedTime)
                    .HasPrecision(6)
                    .HasColumnName("LAST_MODIFIED_TIME");

                entity.Property(e => e.MdcCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("MDC_CODE")
                    .IsFixedLength();

                entity.Property(e => e.MealCloseDate)
                    .HasPrecision(7)
                    .HasColumnName("MEAL_CLOSE_DATE");

                entity.Property(e => e.MedicalSheetFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MEDICAL_SHEET_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.Memo)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("MEMO")
                    .IsFixedLength();

                entity.Property(e => e.MergeAdmitNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MERGE_ADMIT_NO")
                    .IsFixedLength();

                entity.Property(e => e.MergeCutNo)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("MERGE_CUT_NO")
                    .IsFixedLength();

                entity.Property(e => e.MergeFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MERGE_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.MotherAdmitNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MOTHER_ADMIT_NO")
                    .IsFixedLength();

                entity.Property(e => e.NativeFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NATIVE_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.NextUniqueSeq)
                    .HasPrecision(5)
                    .HasColumnName("NEXT_UNIQUE_SEQ");

                entity.Property(e => e.NhApplyFlag1)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NH_APPLY_FLAG_1")
                    .IsFixedLength();

                entity.Property(e => e.NhApplyFlag2)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NH_APPLY_FLAG_2")
                    .IsFixedLength();

                entity.Property(e => e.NhApplyYyymm1)
                    .HasPrecision(5)
                    .HasColumnName("NH_APPLY_YYYMM_1");

                entity.Property(e => e.NhApplyYyymm2)
                    .HasPrecision(5)
                    .HasColumnName("NH_APPLY_YYYMM_2");

                entity.Property(e => e.NhAuditApplyType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NH_AUDIT_APPLY_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.NhCardFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NH_CARD_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.NhCaseType)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("NH_CASE_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.NhClinicSeq)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("NH_CLINIC_SEQ")
                    .IsFixedLength();

                entity.Property(e => e.NhDrgCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("NH_DRG_CODE")
                    .IsFixedLength();

                entity.Property(e => e.NhOkFlag1)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NH_OK_FLAG_1")
                    .IsFixedLength();

                entity.Property(e => e.NhOkFlag2)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NH_OK_FLAG_2")
                    .IsFixedLength();

                entity.Property(e => e.NhReadmit14DaysFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NH_READMIT_14_DAYS_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.NhSuppleApplyType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NH_SUPPLE_APPLY_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.NotCheckReadmitFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NOT_CHECK_READMIT_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.OtherDoctorNo1)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("OTHER_DOCTOR_NO_1")
                    .IsFixedLength();

                entity.Property(e => e.OtherDoctorNo2)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("OTHER_DOCTOR_NO_2")
                    .IsFixedLength();

                entity.Property(e => e.OtherDoctorNo3)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("OTHER_DOCTOR_NO_3")
                    .IsFixedLength();

                entity.Property(e => e.PaidType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PAID_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.PrivacyFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PRIVACY_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.ProcedureCloseDate)
                    .HasPrecision(7)
                    .HasColumnName("PROCEDURE_CLOSE_DATE");

                entity.Property(e => e.PtType)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("PT_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.RNo)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("R_NO")
                    .IsFixedLength();

                entity.Property(e => e.RecomputeFixFeeFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("RECOMPUTE_FIX_FEE_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.RecomputeMealFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("RECOMPUTE_MEAL_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.RecomputeProcedureFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("RECOMPUTE_PROCEDURE_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.RecomputeTelFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("RECOMPUTE_TEL_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.Remark)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("REMARK");

                entity.Property(e => e.RevOrFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("REV_OR_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.SeriousCardNo)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("SERIOUS_CARD_NO")
                    .IsFixedLength();

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .IsFixedLength();

                entity.Property(e => e.TelCloseDate)
                    .HasPrecision(7)
                    .HasColumnName("TEL_CLOSE_DATE");

                entity.Property(e => e.TpnCloseDate)
                    .HasPrecision(7)
                    .HasColumnName("TPN_CLOSE_DATE");

                entity.Property(e => e.TransferInHosp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TRANSFER_IN_HOSP")
                    .IsFixedLength();

                entity.Property(e => e.TransferOutHosp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TRANSFER_OUT_HOSP")
                    .IsFixedLength();

                entity.Property(e => e.TwDrgUsableFlag)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("TW_DRG_USABLE_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.UdCloseDate)
                    .HasPrecision(7)
                    .HasColumnName("UD_CLOSE_DATE");

                entity.Property(e => e.VsNo)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("VS_NO")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
