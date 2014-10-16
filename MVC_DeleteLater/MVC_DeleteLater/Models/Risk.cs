using System;
using System.Collections.Generic;

namespace MVC_DeleteLater.Models
{
    public partial class Risk
    {
        public Risk()
        {
            this.RiskReferences = new List<RiskReference>();
        }

        public int RiskId { get; set; }
        public string Description { get; set; }
        public virtual CCTransaction CCTransaction { get; set; }
        public virtual ICollection<RiskReference> RiskReferences { get; set; }
    }
}
