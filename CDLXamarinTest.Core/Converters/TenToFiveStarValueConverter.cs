using System;
using MvvmCross.Platform.Converters;
using System.Globalization;

namespace CDLXamarinTest.Core
{
	public class TenToFiveStarValueConverter : MvxValueConverter<float, float>
	{
		protected override float Convert (float value, Type targetType, object parameter, CultureInfo culture)
		{
			return (float)Math.Round((value * 5) / 10, 1);
		}
	}
}

