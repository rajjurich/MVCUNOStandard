using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    class DateCompareAttribute : ValidationAttribute
    {
        private String PropertyName { get; set; }
        //private String ErrorMessage { get; set; }
        private string DesiredValue { get; set; }

        public DateCompareAttribute(String propertyName, String desiredvalue, String errormessage)
        {
            this.PropertyName = propertyName;
            this.DesiredValue = desiredvalue;
            this.ErrorMessage = errormessage;
        }
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();
            object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (value != null && Convert.ToDateTime(value) != Convert.ToDateTime("1900-01-01"))
            {

                if (Convert.ToDateTime(value) < Convert.ToDateTime(proprtyvalue))
                {
                    return new ValidationResult(ErrorMessage);
                }

            }
            return ValidationResult.Success;
        }
    }
}
