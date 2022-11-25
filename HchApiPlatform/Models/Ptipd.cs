using System;
using System.Collections.Generic;

namespace HchApiPlatform.Models
{
    public partial class Ptipd
    {
        public string AdmitNo { get; set; } = null!;
        public string CutNo { get; set; } = null!;
        public string? ChartNo { get; set; }
        public string? Status { get; set; }
        public int? AdmitDate { get; set; }
        public int? AdmitTime { get; set; }
        public int? DischargeDate { get; set; }
        public int? DischargeTime { get; set; }
        public int? FeeStartDate { get; set; }
        public int? FeeStartTime { get; set; }
        public int? FeeEndDate { get; set; }
        public int? FeeEndTime { get; set; }
        public int? BirthDate { get; set; }
        public string? BedNo { get; set; }
        public string? AdmitDivNo { get; set; }
        public string? DivNo { get; set; }
        public string? VsNo { get; set; }
        public string? RNo { get; set; }
        public string? AssignedDoctorNo { get; set; }
        public string? PtType { get; set; }
        public string? DiscntType { get; set; }
        public string? AdmitOrigin { get; set; }
        public string? TransferInHosp { get; set; }
        public string? TransferOutHosp { get; set; }
        public string? NhClinicSeq { get; set; }
        public string? PaidType { get; set; }
        public string? BurdenCode { get; set; }
        public string? NhCaseType { get; set; }
        public string? NhDrgCode { get; set; }
        public string? MotherAdmitNo { get; set; }
        public byte? ConsultCount { get; set; }
        public string? DischargeMedNo { get; set; }
        public int? FixFeeCloseDate { get; set; }
        public int? UdCloseDate { get; set; }
        public int? MealCloseDate { get; set; }
        public int? ExpectedDischargeDate { get; set; }
        public int? DiagnosisDischargeDate { get; set; }
        public string? DischargeReason { get; set; }
        public string? ExclusiveWardFlag { get; set; }
        public string? AccidentFlag { get; set; }
        public string? CutFlag { get; set; }
        public string? PrivacyFlag { get; set; }
        public string? NhCardFlag { get; set; }
        public string? AdmitAgreementFlag { get; set; }
        public string? IdCardFlag { get; set; }
        public string? MedicalSheetFlag { get; set; }
        public string? BabyFlag { get; set; }
        public string? NativeFlag { get; set; }
        public string? NhReadmit14DaysFlag { get; set; }
        public string? NhOkFlag1 { get; set; }
        public string? NhApplyFlag1 { get; set; }
        public short? NhApplyYyymm1 { get; set; }
        public string? NhOkFlag2 { get; set; }
        public string? NhApplyFlag2 { get; set; }
        public short? NhApplyYyymm2 { get; set; }
        public string? NhSuppleApplyType { get; set; }
        public int? LastModifiedDate { get; set; }
        public int? LastModifiedTime { get; set; }
        public string? LastModifiedClerk { get; set; }
        public short? NextUniqueSeq { get; set; }
        public string? BillingSeq { get; set; }
        public string? RevOrFlag { get; set; }
        public string? RecomputeFixFeeFlag { get; set; }
        public string? RecomputeMealFlag { get; set; }
        public int? LastKorderDate { get; set; }
        public string? AddWardFeeFlag { get; set; }
        public int? TpnCloseDate { get; set; }
        public string? MergeAdmitNo { get; set; }
        public string? MergeCutNo { get; set; }
        public string? DocumentNo { get; set; }
        public string? AdmitIndexCardNo { get; set; }
        public string? ApplyDoctorNo { get; set; }
        public string? Alias { get; set; }
        public string? FixfeeSeq { get; set; }
        public string? FreePrepaidFlag { get; set; }
        public string? OtherDoctorNo1 { get; set; }
        public string? OtherDoctorNo2 { get; set; }
        public string? OtherDoctorNo3 { get; set; }
        public string? Memo { get; set; }
        public string? SeriousCardNo { get; set; }
        public string? AdmitRemark { get; set; }
        public string? DischargeRemark { get; set; }
        public string? CnsNo { get; set; }
        public int? ProcedureCloseDate { get; set; }
        public string? RecomputeProcedureFlag { get; set; }
        public int? TelCloseDate { get; set; }
        public string? RecomputeTelFlag { get; set; }
        public int? ExpectedDischargeTime { get; set; }
        public string? MergeFlag { get; set; }
        public string? IdsCareStage { get; set; }
        public string? IdsPaidStage { get; set; }
        public string? BcClinicalStage { get; set; }
        public string? BcOrigin { get; set; }
        public string? IsolateType { get; set; }
        public string? MdcCode { get; set; }
        public string? DrgCode { get; set; }
        public string? ApplyDivNo { get; set; }
        public string? Remark { get; set; }
        public string? TwDrgUsableFlag { get; set; }
        public int? CvaStartDate { get; set; }
        public string? NotCheckReadmitFlag { get; set; }
        public string? NhAuditApplyType { get; set; }
    }
}
