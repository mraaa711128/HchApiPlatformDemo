using System.Security.Policy;

namespace HchApiPlatform.Models
{
    public class AdmitPatient
    {
        public string AdmitNo { get; set; }
        public string ChartNo { get; set; }
        public string Name { get; set; }
        public string IdNo { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public DateTime CheckinDatetime { get; set; }
        public string DoctorNo { get; set; }
        public string DoctorName { get; set; }
        public string BedNo { get; set; }
        public string NsCode { get; set; }
        public string NsName { get; set; }
        public string DivNo { get; set; }
        public string DivName { get; set; }
        public string PrivacyFlag { get; set; }
        public string ExclusiveRoomFlag { get; set; }
        public string ExclusiveRoomFlagDesc { get; set; }
        public string IsolateType { get; set; }
        public string IsolateTypeDesc { get; set; }

    }
}
