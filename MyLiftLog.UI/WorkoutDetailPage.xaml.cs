using MyLiftLog.UI.Models;
using Newtonsoft.Json;

namespace MyLiftLog.UI;

public partial class WorkoutDetailPage : ContentPage
{
    public List<Exercise> Exercises { get; set; }
    public Workout CurrentWorkout { get; set; }
   


    public WorkoutDetailPage(Workout workout)
    {
        InitializeComponent();
        BindingContext = this;
        CurrentWorkout = workout;
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
            Exercises = workout.WorkoutExercises.Select(we => new Exercise
            {
                Id = we.Id,
                Name = $"{we.Exercise.Name} - {we.Sets.Count} sets"
            }).ToList();

            // Bind the data to the CollectionView
            ExerciseListView.ItemsSource = Exercises;
        }
    }

    private void OnBackButtonClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async void OnExerciseSelected(object sender, SelectionChangedEventArgs e)
    {
        var exercise = e.CurrentSelection.FirstOrDefault() as Exercise;
        if (exercise != null)
        {
            // Find the selected exercise
            var selectedExercise = CurrentWorkout.WorkoutExercises.FirstOrDefault(we => we.Id == exercise.Id );

            if (selectedExercise != null)
            {
                // Fetch sets for the selected exercise
                await Navigation.PushAsync(new SetDetailPage(CurrentWorkout, selectedExercise.Id));
            }
            
        }

        ((CollectionView)sender).SelectedItem = null;
        

       
    }
}