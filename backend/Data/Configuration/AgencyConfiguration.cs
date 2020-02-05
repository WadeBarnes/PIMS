using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pims.Api.Data.Entities;

namespace Pims.Api.Data.Configuration
{
    /// <summary>
    /// AgencyConfiguration class, provides a way to configure addresses in the database.
    ///</summary>
    public class AgencyConfiguration : BaseEntityConfiguration<Agency>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<Agency> builder)
        {
            builder.ToTable("Agencies");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();

            builder.Property(m => m.Code).HasMaxLength(6);
            builder.Property(m => m.Code).IsRequired();

            builder.Property(m => m.Name).HasMaxLength(150);
            builder.Property(m => m.Name).IsRequired();

            builder.Property(m => m.Description).HasMaxLength(500);

            builder.HasOne(m => m.Parent).WithMany(m => m.Children).HasForeignKey(m => m.ParentId).OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasIndex(m => new { m.Code, m.ParentId }).IsUnique();
            builder.HasIndex(m => new { m.IsDisabled, m.Code, m.Name, m.ParentId });

            base.Configure(builder);
        }
        #endregion
    }
}
