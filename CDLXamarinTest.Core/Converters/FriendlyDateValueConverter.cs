using System;
using MvvmCross.Platform.Converters;
using System.Globalization;

namespace CDLXamarinTest.Core.Converters
{
	public class FriendlyDateValueConverter : MvxValueConverter<DateTime, string>
	{
		protected override string Convert (DateTime value, Type targetType, object parameter, CultureInfo culture)
		{
			var prepend = "{0}";

			try {
				prepend = (string)parameter;
			} catch (Exception e) { }

			return string.Format(prepend, value.ToString("MM/dd/yyyy"));
		}
	}
}

