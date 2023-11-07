using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;

namespace Run4HealthPro
{
    [Activity(Label = "WeatherDetailsActivity")]
    public class WeatherDetailsActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.weather_details);
        }
    }
}
