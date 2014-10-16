using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebApplication1.Models.Mapping
{
    public class RiskMap : EntityTypeConfiguration<Risk>
    {
        public RiskMap()
        {
            // Primary Key
            this.HasKey(t => t.RiskId);

            // Properties
            this.Property(t => t.Description)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Risk");
            this.Property(t => t.RiskId).HasColumnName("RiskId");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
