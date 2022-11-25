using System;
using System.Collections.Generic;

namespace HchApiPlatform.Models
{
    public partial class N
    {
        public string NsCode { get; set; } = null!;
        public string? NsName { get; set; }
        public string? NsType { get; set; }
        public string? UddFlag { get; set; }
        public byte? UddDeliverTime { get; set; }
        public string? DeptCode { get; set; }
        public string? OpenFlag { get; set; }
        public string? UddAutoDcFlag { get; set; }
        public byte? IdeSpreadStartTime { get; set; }
        public string? HospLocation { get; set; }
        public byte? UddProcessOrder { get; set; }
        public string? NsEnglishName { get; set; }
    }
}
