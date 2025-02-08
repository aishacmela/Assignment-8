
using System.Collections.ObjectModel;
using System.Windows.Input;
using Assignment_8.Model;
using Assignment_8.Services;

namespace Assignment_8.ViewModel
{
	public class YeastsViewModel : BindableObject
	{
		private readonly YeastService _yeastService;

		private ObservableCollection<Yeasts> _yeastsList;

		public ObservableCollection<Yeasts> YeastsList
		{
			get => _yeastsList;
			set
			{
				_yeastsList = value;
				OnPropertyChanged();
			}
		}

		private Yeasts _currentYeast;

		public Yeasts CurrentYeast
		{
			get => _currentYeast;
			set
			{
				_currentYeast = value;
				OnPropertyChanged();
			}
		}

		public ICommand SaveCommand { get; }
		public ICommand AddNewYeastCommand { get; }

		public YeastsViewModel()
		{
			_yeastService = new YeastService();
			YeastsList = new ObservableCollection<Yeasts>(); // Initialize as an empty collection
			CurrentYeast = new Yeasts(); // Initialize with a blank yeast profile

			SaveCommand = new Command(async () => await SaveYeastAsync());
			AddNewYeastCommand = new Command(AddNewYeast);

			LoadYeastsAsync(); // Load saved yeasts on startup
		}

		// Load the list of yeasts from the service
		private async Task LoadYeastsAsync()
		{
			var loadedYeasts = await _yeastService.LoadYeastsAsync();
			YeastsList.Clear();
			foreach (var yeast in loadedYeasts)
			{
				YeastsList.Add(yeast);
			}
		}

		// Save the current yeast to the list and save the list to the file
		private async Task SaveYeastAsync()
		{
			// Add the current yeast to the list
			YeastsList.Add(CurrentYeast);

			// Save the updated list of yeasts
			await _yeastService.SaveYeastsAsync(YeastsList);

			// Show a success message
			await Application.Current.MainPage.DisplayAlert("Success", "Yeast profile saved!", "OK");

			// Clear the current yeast form for the user to add a new one
			CurrentYeast = new Yeasts();
		}

		// Reset the current yeast form for adding a new yeast
		private void AddNewYeast()
		{
			CurrentYeast = new Yeasts(); // Reset to a blank profile
		}
	}
}
