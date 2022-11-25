using System;
using System.Collections.Generic;

namespace HchApiPlatform.Models
{
    public partial class Bedisolate
    {
        public string IsolateType { get; set; } = null!;
        public string? IsolateDescription { get; set; }
        public string? IsolateFlag { get; set; }
        public string? SuspendFlag { get; set; }
        public string? ProgIsolateType { get; set; }
    }
}
