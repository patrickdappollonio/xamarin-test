using System;
using MvvmCross.Platform.Converters;
using System.Globalization;

namespace CDLXamarinTest.Core.Converters
{
	public class FloatFormatValueConverter : MvxValueConverter<float, string>
	{
		protected override string Convert (float value, Type targetType, object parameter, CultureInfo culture)
		{
			var prepend = "{0}";

			try {
				prepend = (string)parameter;
			} catch (Exception e) { }

			// return string.Format (prepend, value);
			return string.Format(prepend, value);
		}
	}
}

