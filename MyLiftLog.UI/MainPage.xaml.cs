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
            // Bypass SSL certificate validation for development purposes
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            var client = new HttpClient(handler);

            var response = await client.GetAsync("https://localhost:7093/api/workout");
            Workouts = new List<Workout>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Workouts = JsonConvert.DeserializeObject<List<Workout>>(content);
            }

            return Workouts;
        }

        public async void OnWorkoutSelected(object sender, SelectionChangedEventArgs e)
        {
            var workout = e.CurrentSelection.FirstOrDefault() as Workout;
            if (workout != null)
            {
               await Navigation.PushAsync(new WorkoutDetailPage(workout));
            }

            ((CollectionView)sender).SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Workouts = await GetAllWorkouts();
            WorkoutListView.ItemsSource = Workouts;
        }
    }

}
