using System;
using System.Collections.Generic;

namespace HchApiPlatform.Models
{
    public partial class Bedstatus
    {
        public string BedNo { get; set; } = null!;
        public string? Status { get; set; }
        public string? AdmitNo { get; set; }
        public string? AssignedChartNo { get; set; }
        public string? AssignedType { get; set; }
        public int? AssignedDate { get; set; }
        public int? AssignedTime { get; set; }
        public string? AssignedClerk { get; set; }
        public int? LastModifiedDate { get; set; }
        public int? LastModifiedTime { get; set; }
        public string? LastModifiedClerk { get; set; }
        public string? TelStatus { get; set; }
        public string? Sex { get; set; }
        public string? AssignedSex { get; set; }
    }
}
