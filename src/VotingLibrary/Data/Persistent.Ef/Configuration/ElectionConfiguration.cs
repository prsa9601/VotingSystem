using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VotingLibrary.Data.Entities;

namespace VotingLibrary.Data.Persistent.Ef.Configuration
{
    internal class ElectionConfiguration : IEntityTypeConfiguration<Election>
    {
        public void Configure(EntityTypeBuilder<Election> builder)
        {

            builder.HasKey(x => x.Id);
            //builder.HasIndex(b => b.UserId).IsUnique();

            
            builder.Property(b => b.Title).IsUnicode()
                .IsRequired();
            
            builder.Property(b => b.EndTime)
                .IsRequired();
            
            builder.Property(b => b.StartTime)
                .IsRequired();
        
        }
    }
}
