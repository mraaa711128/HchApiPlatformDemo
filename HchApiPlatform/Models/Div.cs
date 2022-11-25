using System;
using System.Collections.Generic;

namespace HchApiPlatform.Models
{
    public partial class Div
    {
        public string DivNo { get; set; } = null!;
        public string? DivShortName { get; set; }
        public string? DivFullName { get; set; }
        public string? NhOpdDiv { get; set; }
        public string? NhIpdDiv { get; set; }
        public string? NhDivSystem { get; set; }
        public string? NhDivFlag { get; set; }
        public string? DivSystem { get; set; }
        public string? DivProg { get; set; }
        public string? TraFlag { get; set; }
        public string? OpdDeptCode { get; set; }
        public string? ErDeptCode { get; set; }
        public string? IpdMedServiceFlag { get; set; }
        public string? AdmitFlag { get; set; }
        public string? ParentDivNo { get; set; }
        public string? DclinicFlag { get; set; }
        public string? OpdChargePermitFlag { get; set; }
        public byte? StatDays { get; set; }
        public string? InjOpdDeptCode { get; set; }
        public string? InjErDeptCode { get; set; }
        public string? DivNo5 { get; set; }
        public decimal? DivFvWeight { get; set; }
        public string? DeptCode { get; set; }
        public string? VrgDivNo { get; set; }
        public string? EnglishShortName { get; set; }
        public string? EnglishFullName { get; set; }
        public string? CostCenterFlag { get; set; }
        public string? DivRevenueBelong { get; set; }
    }
}
