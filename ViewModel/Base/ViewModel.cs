using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CommonLib.ViewModel.Base;

public class ViewModel : INotifyPropertyChanged
{
	public static bool IsDesignMode { get; } = System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject());

	#region INotifyPropertyChanged
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}

	#endregion
}