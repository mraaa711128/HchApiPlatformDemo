using System;
using System.Collections.Generic;

namespace HchApiPlatform.Models
{
    public partial class AdmitBedStat
    {
        public string BedNo { get; set; } = null!;
        public string? NsCode { get; set; }
        public string? NsName { get; set; }
        public string? WardNo { get; set; }
        public string? Status { get; set; }
        public string? StatusDesc { get; set; }
        public string? AdmitNo { get; set; }
        public string? ChartNo { get; set; }
        public string? AdmitStatus { get; set; }
        public string? AdmitStatusDesc { get; set; }
        public string? Name { get; set; }
        public string? IdNo { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public DateTime? CheckinDateTime { get; set; }
        public string? DoctorNo { get; set; }
        public string? DoctorName { get; set; }
        public string? DivNo { get; set; }
        public string? DivName { get; set; }
        public string? PrivacyFlag { get; set; }
        public string? ExclusiveRoomFlag { get; set; }
        public string? ExclusiveRoomFlagDesc { get; set; }
        public string? IsolateType { get; set; }
        public string? IsolateTypeDesc { get; set; }
    }
}
