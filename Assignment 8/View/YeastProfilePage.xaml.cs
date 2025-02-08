namespace Assignment_8.View;
using Assignment_8.ViewModel;

public partial class YeastProfilePage : ContentPage
{
	public YeastProfilePage()
	{
		InitializeComponent();
		BindingContext = new YeastsViewModel();
	}
}