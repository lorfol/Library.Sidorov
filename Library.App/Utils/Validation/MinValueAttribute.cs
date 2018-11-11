using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.App.Utils.Validation
{
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly int _minValue;

        public MinValueAttribute(int minValue, string message)
        {
            _minValue = minValue;
            this.ErrorMessage = message;
            this.FormatErrorMessage(message);
        }

        public override bool IsValid(object value)
        {
            return (int)value <= _minValue;
        }
    }
}