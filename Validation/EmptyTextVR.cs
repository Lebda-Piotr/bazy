using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace bazy1.Validation {
	internal class EmptyTextVR : ValidationRule {

		public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            Console.WriteLine("kkkkk");
            if (string.IsNullOrEmpty(value as string)) {
				return new ValidationResult(false, "nie działa");
		}
			return new ValidationResult(true,null);
		}
	}
}
