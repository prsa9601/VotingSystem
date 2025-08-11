using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VotingLibrary.Data.Entities;

namespace VotingLibrary.Data.Persistent.Ef.Configuration
{
    internal class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            // تعیین کلید اصلی
            builder.HasKey(v => v.Id);

            // تنظیمات VoteNumber
            builder.Property(v => v.VoteNumber)
                .IsRequired();

            // تنظیمات UserId (رابطه با User)
            builder.Property(v => v.UserId)
                .IsRequired();

            //builder.HasOne<User>()
            //    .WithMany()
            //    .HasForeignKey(v => v.UserId)
            //    .OnDelete(DeleteBehavior.Restrict); // یا Cascade بسته به نیاز

            // تنظیمات CandidateId (رابطه با Candidate)
            builder.Property(v => v.CandidateId)
                .IsRequired();

            //builder.HasOne<Candidate>()
            //    .WithMany()
            //    .HasForeignKey(v => v.CandidateId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // تنظیمات VoteTime
            builder.Property(v => v.VoteTime)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()"); // یا CURRENT_TIMESTAMP برای SQLite

            // تنظیمات IsAdminVote
            builder.Property(v => v.IsAdminVote)
                .IsRequired()
                .HasDefaultValue(false);

            // تنظیمات Election (رابطه با Election)
            builder.Property(v => v.ElectionId)
                .IsRequired();

            // ایجاد ایندکس ترکیبی برای جلوگیری از رای تکراری
            builder.HasIndex(v => new { v.UserId, v.CandidateId, v.ElectionId })
                .IsUnique();

            // ایندکس برای بهبود عملکرد جستجو
            builder.HasIndex(v => v.UserId);
            builder.HasIndex(v => v.CandidateId);
            builder.HasIndex(v => v.ElectionId);
            builder.HasIndex(v => v.VoteTime);

        }
    }
}
