using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC_DeleteLater.Models.Mapping
{
    public class RiskReferenceMap : EntityTypeConfiguration<RiskReference>
    {
        public RiskReferenceMap()
        {
            // Primary Key
            this.HasKey(t => t.RiskReferenceId);

            // Properties
            this.Property(t => t.Description)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("RiskReference");
            this.Property(t => t.RiskReferenceId).HasColumnName("RiskReferenceId");
            this.Property(t => t.RiskId).HasColumnName("RiskId");
            this.Property(t => t.Description).HasColumnName("Description");

            // Relationships
            this.HasRequired(t => t.Risk)
                .WithMany(t => t.RiskReferences)
                .HasForeignKey(d => d.RiskId);

        }
    }
}
