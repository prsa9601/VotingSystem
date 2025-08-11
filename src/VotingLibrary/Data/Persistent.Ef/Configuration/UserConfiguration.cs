using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VotingLibrary.Data.Entities;

namespace VotingLibrary.Data.Persistent.Ef.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // تعیین کلید اصلی
            builder.HasKey(u => u.Id);

            builder.Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            // ایجاد ایندکس برای PhoneNumber (منحصربفرد)
            builder.HasIndex(u => u.PhoneNumber)
                .IsUnique();

            // تنظیمات FullName
            builder.Property(u => u.FullName)
                .HasMaxLength(100)
                .IsRequired(false);

            // تنظیمات Family
            //builder.Property(u => u.Family)
            //    .HasMaxLength(50)
            //    .IsRequired(false);

            // تنظیمات AccessVote
            builder.Property(u => u.AccessVote)
                .HasDefaultValue(false)
                .IsRequired();

            // تنظیمات Role (به عنوان enum)
            builder.Property(u => u.Role)
                .IsRequired()
                .HasConversion<string>() // ذخیره به صورت رشته در دیتابیس
                .HasMaxLength(20);

            // تنظیمات VoteId
            builder.Property(u => u.VotesId)
                .IsRequired();

        }
    }
}
