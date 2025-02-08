using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Assignment_8.Model;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Assignment_8.Services
{
	public class YeastService
	{
			// File name for the JSON data storage
			private const string FileName = "yeasts.json";

			// Save the list of yeasts to a local file
			public async Task SaveYeastsAsync(ObservableCollection<Yeasts> yeasts)
			{
				// Get the path for saving in the app's data directory
				var filePath = Path.Combine(FileSystem.AppDataDirectory, FileName);

				// Serialize the list of yeasts to JSON format
				var jsonData = JsonSerializer.Serialize(yeasts);

				// Write the JSON data to the file
				await File.WriteAllTextAsync(filePath, jsonData);
			}

			// Load the list of yeasts from the local file
			public async Task<List<Yeasts>> LoadYeastsAsync()
			{
				// Get the path to the saved file
				var filePath = Path.Combine(FileSystem.AppDataDirectory, FileName);

				// Check if the file exists
				if (File.Exists(filePath))
				{
					// Read the file content as a string
					var jsonData = await File.ReadAllTextAsync(filePath);

					// Deserialize the JSON string back to a list of yeasts
					return JsonSerializer.Deserialize<List<Yeasts>>(jsonData) ?? new List<Yeasts>();
				}
				else
				{
					// If the file doesn't exist, return an empty list
					return new List<Yeasts>();
				}
			}
		}
	}



