using CommonLib.ViewModels.Base;

namespace CommonLib.ViewModels;

public class TitledViewModel : ViewModel
{
	/// <summary>Заголовок</summary>
	private string? _title;

	/// <summary>Заголовок</summary>
	public string? Title { get => _title; set => SetField(ref _title, value); }
}