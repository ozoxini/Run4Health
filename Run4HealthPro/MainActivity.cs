using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Run4HealthPro
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextView temperatureTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Button detailsButton = FindViewById<Button>(Resource.Id.detailsButton);

            detailsButton.Click += DetailsButton_Click;

            PobierzAktualnaTemperature().ContinueWith((task) =>
            {
                temperatureTextView.Text = task.Result;
            });
        }

        private async Task<string> PobierzAktualnaTemperature()
        {
            string apiKey = "253b69f48d254853f3ebd6329b7f12d3";
            string miasto = "Warszawa";
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={miasto}&appid={apiKey}&units=metric";
            string temperatura = "";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetStringAsync(apiUrl);
                    var weatherData = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherData>(response);

                    temperatura = $"Aktualna temperatura: {weatherData.Main.Temperature}°C";
                }
                catch (Exception ex)
                {
                    temperatura = "Błąd pobierania temperatury: " + ex.Message;
                }
            }

            return temperatura;
        }

        private void DetailsButton_Click(object sender, EventArgs e)
        {
            StartActivity(new Android.Content.Intent(this, typeof(WeatherDetailsActivity)));
        }
    }

    public class WeatherData
    {
        public MainData Main { get; set; }
    }

    public class MainData
    {
        public double Temperature { get; set; }
    }
}
