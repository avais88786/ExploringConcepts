using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebApplication1.Models.Mapping
{
    public class CCTransactionMap : EntityTypeConfiguration<CCTransaction>
    {
        public CCTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.CCTransactionId);

            // Properties
            this.Property(t => t.Reference)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Description)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CCTransaction");
            this.Property(t => t.CCTransactionId).HasColumnName("CCTransactionId");
            this.Property(t => t.RiskId).HasColumnName("RiskId");
            this.Property(t => t.Reference).HasColumnName("Reference");
            this.Property(t => t.Description).HasColumnName("Description");

            // Relationships
            this.HasRequired(t => t.Risk)
                .WithOptional();

        }
    }
}
