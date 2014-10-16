using System;
using System.Collections.Generic;

namespace MVC_DeleteLater.Models
{
    public partial class RiskReference
    {
        public int RiskReferenceId { get; set; }
        public int RiskId { get; set; }
        public string Description { get; set; }
        public virtual Risk Risk { get; set; }
    }
}
