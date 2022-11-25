using System;
using System.Collections.Generic;

namespace HchApiPlatform.Models
{
    public partial class Bed
    {
        public string BedNo { get; set; } = null!;
        public string EffectiveDateC { get; set; } = null!;
        public string? NsCode { get; set; }
        public string? WardNo { get; set; }
        public string? GradeCode { get; set; }
        public string? DivNo { get; set; }
        public string? NhBedNo { get; set; }
        public string? Location { get; set; }
        public string? StatisticFlag { get; set; }
        public string? AutoAssignFlag { get; set; }
        public string? MedServiceFlag { get; set; }
        public int? EffectiveDate { get; set; }
        public int? SuspendDate { get; set; }
        public string? VirtualBedFlag { get; set; }
        public string? DedicatedBedFlag { get; set; }
    }
}
