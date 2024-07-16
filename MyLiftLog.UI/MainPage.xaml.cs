using MyLiftLog.UI.Models;
using Newtonsoft.Json;

namespace MyLiftLog.UI
{
    public partial class MainPage : ContentPage
    {
        public List<Workout> Workouts { get; set; }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;  
        }

        public async Task<List<Workout>> GetAllWorkouts()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7093/api/workout");
            Workouts = new List<Workout>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Workouts = JsonConvert.DeserializeObject<List<Workout>>(content);
            }

            return Workouts;
        }

        public void OnWorkoutSelected(object sender, SelectionChangedEventArgs e)
        {
            var workout = e.CurrentSelection.FirstOrDefault() as Workout;
            if (workout != null)
            {
                Navigation.PushAsync(new WorkoutDetailPage(workout));
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Workouts = await GetAllWorkouts();
            WorkoutListView.ItemsSource = Workouts;
        }
    }

}
