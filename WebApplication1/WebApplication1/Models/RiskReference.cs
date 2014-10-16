using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class RiskReference
    {
        public int RiskReferenceId { get; set; }
        public int RiskId { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public Risk Risk { get; set; }
    }
}
