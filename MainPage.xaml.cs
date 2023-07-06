using MauiApiRestDemo.ViewModels;

namespace MauiApiRestDemo;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainViewModel();
	}

	
}

