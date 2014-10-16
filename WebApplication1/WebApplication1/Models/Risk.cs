using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Risk
    {
        public Risk()
        {
          //  this.RiskReferences = new List<RiskReference>();
        }

        public int RiskId { get; set; }
        public string Description { get; set; }
        //public CCTransaction CCTransaction { get; set; }
        //public ICollection<RiskReference> RiskReferences { get; set; }
    }
}
