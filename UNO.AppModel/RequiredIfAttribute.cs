using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    class RequiredIfAttribute : ValidationAttribute
    {
        private String PropertyName { get; set; }
        //private String ErrorMessage { get; set; }
        private Object DesiredValue { get; set; }

        public RequiredIfAttribute(String propertyName, Object desiredvalue, String errormessage)
        {
            this.PropertyName = propertyName;
            this.DesiredValue = desiredvalue;
            this.ErrorMessage = errormessage;
        }
       
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();
            Object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (proprtyvalue != null)
            {
                if (PropertyName == "EOD_TYPE")
                {
                    if (proprtyvalue.ToString() == DesiredValue.ToString() && value == null)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
                if (PropertyName == "EOD_LEFT_REASON_ID")
                {
                    if (proprtyvalue != null && value == null)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
                if (PropertyName == "EXTRA_CHECK")
                {
                    if (proprtyvalue != null && value == null && proprtyvalue.ToString().ToLower() == "true")
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
