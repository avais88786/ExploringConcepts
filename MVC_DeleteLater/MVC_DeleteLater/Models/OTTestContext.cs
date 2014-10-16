using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MVC_DeleteLater.Models.Mapping;

namespace MVC_DeleteLater.Models
{
    public partial class OTTestContext : DbContext
    {
        static OTTestContext()
        {
            Database.SetInitializer<OTTestContext>(null);
        }

        public OTTestContext()
            : base("Name=OTTestContext")
        {
        }

        public DbSet<CCTransaction> CCTransactions { get; set; }
        public DbSet<Risk> Risks { get; set; }
        public DbSet<RiskReference> RiskReferences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CCTransactionMap());
            modelBuilder.Configurations.Add(new RiskMap());
            modelBuilder.Configurations.Add(new RiskReferenceMap());
        }
    }
}
