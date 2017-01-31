namespace Miharu.Wpf.Validations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ChoicesAttribute : ValidationAttribute
    {
        public object[] Items { get; set; }

        public ChoicesAttribute(params object[] items)
        {
            this.Items = items;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (this.IsValid(value))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(this.ErrorMessage);
            }
        }

        public override bool IsValid(object value)
        {
            foreach (var item in this.Items)
            {
                if (item.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
