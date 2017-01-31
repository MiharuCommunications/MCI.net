namespace Miharu.Wpf.Validations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DecimalAttribute : ValidationAttribute
    {
        public decimal? Maximum { get; set; }

        public decimal? Minimum { get; set; }

        public string OverMaximumErrorMessage { get; set; }

        public string UnderMinimumErrorMessage { get; set; }

        public DecimalAttribute()
        {
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value as string;

            if (str == null)
            {
                return new ValidationResult("文字列ではありません");
            }

            if (str == string.Empty)
            {
                return ValidationResult.Success;
            }

            decimal d;
            if (!decimal.TryParse(str, out d))
            {
                return new ValidationResult("実数の形になっていません");
            }

            if (this.Maximum.HasValue && this.Maximum.Value < d)
            {
                if (string.IsNullOrEmpty(this.OverMaximumErrorMessage))
                {
                    return new ValidationResult(String.Format("{0} 以下でなければなりません", this.Maximum));
                }
                else
                {
                    return new ValidationResult(this.OverMaximumErrorMessage);
                }
            }


            if (this.Minimum.HasValue && d < this.Minimum.Value)
            {
                if (string.IsNullOrEmpty(this.UnderMinimumErrorMessage))
                {
                    return new ValidationResult(String.Format("{0} 以上でなければなりません", this.Minimum));
                }
                else
                {
                    return new ValidationResult(this.UnderMinimumErrorMessage);
                }
            }

            return base.IsValid(value, validationContext);
        }
    }
}
