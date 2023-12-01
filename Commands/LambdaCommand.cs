using System;
using System.ComponentModel;
using System.Windows.Markup;
using CommonLib.Commands.Base;
using CommonLib.ViewModels.Base;

namespace CommonLib.Commands;

/// <summary>
/// Лямбда-команда
/// Позволяет быстро указывать методы для выполнения основного тела команды и определения возможности выполнения
/// </summary>
[MarkupExtensionReturnType(typeof(LambdaCommand))]
public class LambdaCommand : Command
{
	public static LambdaCommand OnExecute(Action<object?> ExecuteAction, Func<object?, bool>? CanExecute = null) => new(ExecuteAction, CanExecute);
	public static LambdaCommand OnExecute(Action ExecuteAction, Func<object?, bool>? CanExecute = null) => new(ExecuteAction, CanExecute);

	/// <summary>Делегат основного тела команды</summary>
	// ReSharper disable once FieldCanBeMadeReadOnly.Global
	protected Action<object?>? _ExecuteAction;
	/// <summary>Функция определения возможности исполнения команды</summary>
	protected Func<object?, bool>? _CanExecute;

	/// <summary>Функция определения возможности исполнения команды</summary>
	public Func<object?, bool>? CanExecuteDelegate
	{
		get => _CanExecute;
		set
		{
			if (ReferenceEquals(_CanExecute, value)) return;
			_CanExecute = value ?? (_ => true);
			OnPropertyChanged(nameof(CanExecuteDelegate));
		}
	}

	#region События

	/// <summary>Возникает, когда команда отменена</summary>
	public event EventHandler? Cancelled;
	protected virtual void OnCancelled(EventArgs? args = null) => Cancelled?.Invoke(this, args ?? EventArgs.Empty);
	public event CancelEventHandler? StartExecuting;
	protected virtual void OnStartExecuting(CancelEventArgs args) => StartExecuting?.Invoke(this, args);
	
	#endregion

	#region Конструкторы
	protected LambdaCommand() { }
	public LambdaCommand(Action<object?> ExecuteAction, Func<object?, bool>? CanExecute = null)
		: this()
	{
		_ExecuteAction = ExecuteAction ?? throw new ArgumentNullException(nameof(ExecuteAction));
		_CanExecute = CanExecute;
	}
	public LambdaCommand(Action<object?> ExecuteAction, Func<bool>? CanExecute) : this(ExecuteAction, CanExecute is null ? null : _ => CanExecute!()) { }
	public LambdaCommand(Action ExecuteAction, Func<object?, bool>? CanExecute = null) : this(_ => ExecuteAction(), CanExecute) { }
	public LambdaCommand(Action ExecuteAction, Func<bool>? CanExecute) : this(_ => ExecuteAction(), CanExecute is null ? null : _ => CanExecute!()) { }

	#endregion

	#region Методы

	/// <summary>Выполнение команды</summary>
	/// <param name="parameter">Параметр процесса выполнения команды</param>
	/// <exception cref="InvalidOperationException">Метод выполнения команды не определён</exception>
	public override void Execute(object? parameter)
	{
		if (_ExecuteAction is null) throw new InvalidOperationException("Метод выполнения команды не определён");
		if (!CanExecute(parameter)) return;
		var cancel_args = new CancelEventArgs();
		OnStartExecuting(cancel_args);
		if (cancel_args.Cancel)
		{
			OnCancelled(cancel_args);
			if (cancel_args.Cancel) return;
		}
		_ExecuteAction(parameter);
		
	}

	/// <summary>Проверка возможности выполнения команды</summary>
	/// <param name="parameter">Параметр процесса выполнения команды</param>
	/// <returns>Истина, если команда может быть выполнена</returns>
	public override bool CanExecute(object? parameter) =>
		ViewModel.IsDesignMode
		|| IsCanExecute && (_CanExecute?.Invoke(parameter) ?? true);

	/// <summary>Проверка возможности выполнения команды</summary>
	public void CanExecuteCheck() => OnCanExecuteChanged();

	#endregion
}