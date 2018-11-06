using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Module.Stooges.ModelValidation
{
    public class AmountAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var amount = (double)value;
            return amount >= 0;
        }
    }
}
