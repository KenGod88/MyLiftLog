using MyLiftLog.UI.Models;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace MyLiftLog.UI;

public partial class SetDetailPage : ContentPage
{
    public List<string> SetDetailes { get; set; }

    public SetDetailPage(Workout workout)
    {
        InitializeComponent();
        LoadSetDetails(workout.Id);
    }

    private void OnBackButtonClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async void LoadSetDetails(Guid workoutId)
    {
        // Bypass SSL certificate validation for development purposes
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
        var client = new HttpClient(handler);

        var response = await client.GetAsync($"https://localhost:7093/api/workout/{workoutId}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var workout = JsonConvert.DeserializeObject<Workout>(content);
            SetDetailes = new List<string>();
            int setNumber = 1;

            foreach (var workoutExercise in workout.WorkoutExercises)
            {
                foreach (var set in workoutExercise.Sets)
                {
                    SetDetailes.Add($"Set {setNumber}: {set.Reps} reps at {set.Weight} Kg");
                    setNumber++;
                }
            }

            SetListView.ItemsSource = SetDetailes;
        }
    }
}