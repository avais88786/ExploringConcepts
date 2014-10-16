using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class CCTransaction
    {
        public int CCTransactionId { get; set; }
        public int RiskId { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public Risk Risk { get; set; }
    }
}
