using System.Text.Json.Serialization;

namespace HchApiPlatform.Models
{
    public class AdmitBed
    {
        [JsonPropertyOrder(0)]
        public string BedNo { get; set; }
        [JsonPropertyOrder(1)]
        public string? NsCode { get; set; }
        [JsonPropertyOrder(2)]
        public string? NsName { get; set; }
        [JsonPropertyOrder(3)]
        public string? WardNo { get; set; }
        [JsonPropertyOrder(4)]
        public string? Status { get; set; }
        [JsonPropertyOrder(5)]
        public string? StatusDesc { get; set; }
        [JsonPropertyOrder(6)]
        public string? AdmitNo { get; set; }
        [JsonPropertyOrder(7)]
        public string? ChartNo { get; set; }
    }

    public class AdmitBedDetail : AdmitBed { 
        [JsonPropertyOrder(6)]
        public string? AdmitNo { get; set; }
        [JsonPropertyOrder(7)]
        public string? ChartNo { get; set; }
        [JsonPropertyOrder(8)]
        public string? AdmitStatus { get; set; }
        [JsonPropertyOrder(9)]
        public string? AdmitStatusDesc { get; set; }
        [JsonPropertyOrder(10)]
        public string? Name { get; set; }
        [JsonPropertyOrder(11)]
        public string? IdNo { get; set; }
        [JsonPropertyOrder(12)]
        public DateTime? BirthDate { get; set; }
        [JsonPropertyOrder(13)]
        public string? Gender { get; set; }
        [JsonPropertyOrder(14)]
        public DateTime? CheckinDatetime { get; set; }
        [JsonPropertyOrder(15)]
        public string? DoctorNo { get; set; }
        [JsonPropertyOrder(16)]
        public string? DoctorName { get; set; }
        [JsonPropertyOrder(17)]
        public string? DivNo { get; set; }
        [JsonPropertyOrder(18)]
        public string? DivName { get; set; }
        [JsonPropertyOrder(19)]
        public string? PrivacyFlag { get; set; }
        [JsonPropertyOrder(20)]
        public string? ExclusiveRoomFlag { get; set; }
        [JsonPropertyOrder(21)]
        public string? ExclusiveRoomFlagDesc { get; set; }
        [JsonPropertyOrder(22)]
        public string? IsolateType { get; set; }
        [JsonPropertyOrder(23)]
        public string? IsolateTypeDesc { get; set; }


    }
}
