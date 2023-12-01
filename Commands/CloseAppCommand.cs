using System.Windows;
using CommonLib.Commands.Base;

namespace CommonLib.Commands;

public class CloseAppCommand :Command
{
	public override void Execute(object? parameter) => Application.Current.Shutdown();
}