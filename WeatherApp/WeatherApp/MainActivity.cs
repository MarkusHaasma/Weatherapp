using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using WeatherApp.Core;
using Android.Graphics;
using System.Net;
using System;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace WeatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView minMaxTempTextView;
        TextView windTextView;
        TextView pressureTextView;
        ImageView weatherImage;
        EditText cityEditText;
        ProgressBar progressBar;
        protected  override  void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            AppCenter.Start("33907d7d-f324-42ce-885e-59511b0c5543", typeof(Analytics), typeof(Crashes));

            var button = FindViewById<Button>(Resource.Id.SearchButton);
            minMaxTempTextView = FindViewById<TextView>(Resource.Id.MinMaxTempTextView);
            windTextView = FindViewById<TextView>(Resource.Id.WindTextView);
            pressureTextView = FindViewById<TextView>(Resource.Id.PressureTextView);
            cityEditText = FindViewById<EditText>(Resource.Id.CityEditText);
            weatherImage = FindViewById<ImageView>(Resource.Id.WeatherImage);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);          
            progressBar.Visibility = Android.Views.ViewStates.Invisible;                    
            button.Click += Button_Click;
        }


        private async void Button_Click(object sender, System.EventArgs e)
        {
            progressBar.Visibility = Android.Views.ViewStates.Visible;

            var weather = await Core.Core.GetWeather(cityEditText.Text);
            if(weather != null)
            {
                minMaxTempTextView.Text = weather.MinTemp + " / " + weather.MaxTemp;
                windTextView.Text = weather.WindSpeed;
                pressureTextView.Text = weather.Pressure;

                Bitmap imageBitmap = null;
                using (var webClient = new WebClient())
                {
                    var imageBytes =
                        webClient.DownloadData(new Uri("http://openweathermap.org/img/w/" + weather.Icon.Trim() + ".png"));
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
                weatherImage.SetImageBitmap(imageBitmap);

            }
            else
            {
                minMaxTempTextView.Text = "No City Found";
            }

            progressBar.Visibility = Android.Views.ViewStates.Invisible;


        }
    }
}