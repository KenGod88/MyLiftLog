using MyLiftLog.UI.Models;

namespace MyLiftLog.UI;

public partial class SetDetailPage : ContentPage
{
    public List<string> SetDetailes { get; set; }

    public SetDetailPage(Workout workout, Guid workoutExerciseId)
    {
        InitializeComponent();
        LoadSetDetails(workout, workoutExerciseId);
    }

    private void OnBackButtonClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void LoadSetDetails(Workout workout, Guid workoutExerciseId)
    {
        SetDetailes = new List<string>();
        int setNumber = 1;

        // Find the specific WorkoutExercise using the ID
        var workoutExercise = workout.WorkoutExercises.FirstOrDefault(we => we.Id == workoutExerciseId);

        if (workoutExercise != null)
        {
            foreach (var set in workoutExercise.Sets)
            {
                SetDetailes.Add($"Set {setNumber}: {set.Reps} reps at {set.Weight} Kg");
                setNumber++;
            }

            SetListView.ItemsSource = SetDetailes;
        }
    }
}