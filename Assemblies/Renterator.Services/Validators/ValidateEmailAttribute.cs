using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Renterator.Services.Validators
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ValidateEmailAttribute : DataTypeAttribute
    {
        private readonly Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.Compiled);

        public ValidateEmailAttribute()
            : base(DataType.EmailAddress)
        {

        }

        public override bool IsValid(object value)
        {
            string str = Convert.ToString(value, CultureInfo.CurrentCulture);
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }

            Match match = regex.Match(str);
            return ((match.Success && (match.Index == 0)) && (match.Length == str.Length));
        }
    }
}
