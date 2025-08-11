using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VotingLibrary.Core.Common.Attribute
{
    public class IranianPhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var phoneNumber = value.ToString();

            // الگوهای معتبر برای شماره‌های ایرانی
            string mobilePattern = @"^(0098|\+98|0)?9[0-9]{9}$";
            string landlinePattern = @"^(0098|\+98|0)?[1-9][0-9]{1,9}$";

            // حذف فاصله‌ها و خط‌تیره‌ها
            phoneNumber = phoneNumber.Replace(" ", "").Replace("-", "");

            bool isValid = Regex.IsMatch(phoneNumber, mobilePattern) ||
                           Regex.IsMatch(phoneNumber, landlinePattern);

            if (!isValid)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "شماره تماس معتبر نیست. قالب‌های قابل قبول: 09123456789، 02112345678، +989123456789، 00989123456789";
        }
    }
}