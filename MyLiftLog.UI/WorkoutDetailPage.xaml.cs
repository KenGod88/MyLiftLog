using MyLiftLog.UI.Models;
using Newtonsoft.Json;

namespace MyLiftLog.UI;

public partial class WorkoutDetailPage : ContentPage
{

    public List<Exercise> Exercises { get; set; }

    public WorkoutDetailPage(Workout workout)
    {
        InitializeComponent();
        // Fetch exercises for the workout when the page is initialized
        LoadWorkoutDetails(workout.Id);
    }

    private async void LoadWorkoutDetails(Guid workoutId)
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

            // Extract exercises from workout data
            Exercises = workout.WorkoutExercises.Select(we => we.Exercise).ToList();

            // Bind the data to the CollectionView
            ExerciseListView.ItemsSource = Exercises;
        }
    }

    private void OnBackButtonClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }


}
