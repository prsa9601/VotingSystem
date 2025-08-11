using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VotingLibrary.Data.Entities;

namespace VotingLibrary.Data.Persistent.Ef.Configuration
{
    internal class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            // تعیین کلید اصلی
            builder.HasKey(c => c.Id);

            // تنظیمات FullName
            builder.Property(c => c.FullName)
                .IsRequired(false)
                .HasMaxLength(100);

            // تنظیمات Family
            //builder.Property(c => c.Family)
            //    .IsRequired()
            //    .HasMaxLength(60);

            // تنظیمات IsWin
            builder.Property(c => c.IsWin)
                .HasDefaultValue(false)
                .IsRequired();

            // تنظیمات VotesId (به عنوان یک لیست از GUIDها)
            //builder.Property(c => c.VotesId)
            //    .HasConversion(
            //        v => string.Join(',', v),
            //        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
            //              .Select(Guid.Parse)
            //              .ToList())
            //    .Metadata.SetValueComparer(new ValueComparer<List<Guid>>(
            //        (c1, c2) => c1.SequenceEqual(c2),
            //        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            //        c => c.ToList()));

            //// تنظیمات ElectionsId (به عنوان یک لیست از GUIDها)
            //builder.Property(c => c.ElectionsId)
            //    .HasConversion(
            //        v => string.Join(',', v),
            //        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
            //              .Select(Guid.Parse)
            //              .ToList())
            //    .Metadata.SetValueComparer(new ValueComparer<List<Guid>>(
            //        (c1, c2) => c1.SequenceEqual(c2),
            //        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            //        c => c.ToList()));

            // ایجاد ایندکس برای نام و نام خانوادگی
            //builder.HasIndex(c => new { FullName = c.FullName });
        }
    }
}
