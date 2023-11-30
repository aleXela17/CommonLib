// -------------------------------------------------------------------------------------------------------------------------------------
// <copyright company "HOME" file="IntToVisibilityConverter.cs">
//  OrderOV
// </copyright>
// 
// <summary>
// create 07.02.2017  Ghost
// </summary>
//  -------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CommonLib.Converters;

[ValueConversion(typeof(int), typeof(bool))]
public class IntToVisibilityConverter : IValueConverter
{
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value == null || (int)value == 0)
		{
			return Visibility.Hidden;
		}
		return Visibility.Visible;
	}

	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}