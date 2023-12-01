using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xaml;

namespace CommonLib.Commands.Base;

public abstract class Command : MarkupExtension, ICommand, INotifyPropertyChanged, IDisposable
{
	private bool _isCanExecute = true;

	// <summary>Признак возможности исполнения</summary>
	public bool IsCanExecute
	{
		get => _isCanExecute;
		set
		{
			if (_isCanExecute == value) return;
			_isCanExecute = value;
			OnPropertyChanged(nameof(IsCanExecute));
			CommandManager.InvalidateRequerySuggested();
		}
	}

	#region INotifyPropertyChanged

	private event PropertyChangedEventHandler? PropertyChangedHandlers;

	event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
	{
		add => PropertyChangedHandlers += value;
		remove => PropertyChangedHandlers -= value;
	}
	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
		PropertyChangedHandlers?.Invoke(this, new PropertyChangedEventArgs(propertyName));

	protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
	{
		if (Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}

	#endregion

	#region ICommand

	private event EventHandler? CanExecuteChangedHandlers;

	protected virtual void OnCanExecuteChanged(EventArgs? e = null) => CanExecuteChangedHandlers?.Invoke(this, e ?? EventArgs.Empty);

	/// <summary>Событие возникает при изменении возможности исполнения команды</summary>
	public event EventHandler? CanExecuteChanged
	{
		add
		{
			CommandManager.RequerySuggested += value;
			CanExecuteChangedHandlers += value;
		}
		remove
		{
			CommandManager.RequerySuggested -= value;
			CanExecuteChangedHandlers -= value;
		}
	}
	public virtual bool CanExecute(object? parameter) => _isCanExecute || ViewModel.ViewModel.IsDesignMode;

	public abstract void Execute(object? parameter);

	bool ICommand.CanExecute(object? parameter) => CanExecute(parameter);

	void ICommand.Execute(object? parameter)
	{
		if (!CanExecute(parameter)) return;
		Execute(parameter);
	}

	#endregion

	#region MarkupExtension

	private WeakReference? _targetObjectReference;
	private WeakReference? _rootObjectReference;
	private WeakReference? _targetPropertyReference;

	protected object? TargetObject => _targetObjectReference?.Target;
	protected object? RootObject => _rootObjectReference?.Target;
	protected object? TargetProperty => _targetPropertyReference?.Target;

	/// <inheritdoc />
	public override object ProvideValue(IServiceProvider sp)
	{
		var targetValueProvider = (IProvideValueTarget)sp.GetService(typeof(IProvideValueTarget))!;
		if (targetValueProvider != null)
		{
			var target = targetValueProvider.TargetObject;
			_targetObjectReference = target is null ? null : new WeakReference(target);
			var targetProperty = targetValueProvider.TargetProperty;
			_targetPropertyReference = targetProperty is null ? null : new WeakReference(targetProperty);
		}

		var rootObjectProvider = (IRootObjectProvider)sp.GetService(typeof(IRootObjectProvider))!;
		if (rootObjectProvider != null)
		{
			var root = rootObjectProvider.RootObject;
			_rootObjectReference = root is null ? null : new WeakReference(root);
		}
		return this;
	}

	#endregion

	#region IDisposable
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	private bool _disposed;

	protected virtual void Dispose(bool disposing)
	{
		if (!disposing || _disposed) return;
		_disposed = true;
	}

	#endregion
}