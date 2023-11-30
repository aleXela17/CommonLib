using System.Globalization;
using System.Windows.Data;

namespace CommonLib.Converters;

/// <summary>
/// This converter is used to bind a value to a group of radio buttons, and can be used with an enum, bool, int, etc.
/// It depends on the converter parameter, which maps a radio button to a specific value.
/// Note: this converter is not designed to work with flag enums (e.g. multiple checkboxes scenario).
/// 
/// Этот конвертер используется для связывания значение группы переключателей , и может быть использован с перечислениями BOOL , INT , и т.д.
/// Это зависит от параметра преобразователя , который отображает радио-кнопку на определенное значение.
/// Примечание : Этот конвертер не предназначен для работы с флагом перечисления ( например несколько checkboxes ) .
/// 
/// Here is an example of the bidning for an enum value:
/// <RadioButton Content="Option 1" IsChecked="{Binding EnumValue, Converter={StaticResource EnumToRadioBoxCheckedConverter}, 
///     ConverterParameter={x:Static local:TestEnum.Option1}}" GroupName="EnumGroup"/>
/// <RadioButton Content="Option 2" IsChecked="{Binding EnumValue, Converter={StaticResource EnumToRadioBoxCheckedConverter}, 
///     ConverterParameter={x:Static local:TestEnum.Option2}}" GroupName="EnumGroup"/>
/// 
///public enum TestEnum
///{
///    Option1,
///    Option2,
///    Option3
///}
/// </summary>
public class RadioButtonCheckedConverter : IValueConverter
{
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		return value != null && value.Equals(parameter);
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		return value != null && value.Equals(true) ? parameter : Binding.DoNothing;
	}
}