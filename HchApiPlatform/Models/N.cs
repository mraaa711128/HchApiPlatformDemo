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
        public int? UddDeliverTime { get; set; }
        public string? DeptCode { get; set; }
        public string? OpenFlag { get; set; }
        public string? UddAutoDcFlag { get; set; }
        public int? IdeSpreadStartTime { get; set; }
        public string? HospLocation { get; set; }
        public int? UddProcessOrder { get; set; }
        public string? NsEnglishName { get; set; }
    }
}
