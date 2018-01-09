using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Plugin.TextToSpeech;
using Xamarin.Forms;

namespace BennysApp
{
    public partial class MainPage : ContentPage
    {
        readonly Entry phrase;
        Button yodify;
        ActivityIndicator activity;

        public MainPage()
        {
            InitializeComponent();

            this.Padding = new Thickness(20, 20, 20, 20);

            var panel = new StackLayout
            {
                Spacing = 15
            };

            panel.Children.Add(activity = new ActivityIndicator
            {
                IsRunning = false,
            });

            panel.Children.Add(new Label
            {
                Text = "Enter a phrase:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(phrase = new Entry
            {
                Placeholder = "Phrase",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(yodify = new Button
            {
                Text = "Yodify",
                BackgroundColor = Color.Green,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(new Image
            {
                Source = ImageSource.FromUri(new Uri("https://i.ebayimg.com/images/g/NvgAAOSwHPlWgxnU/s-l1600.jpg")),
                Margin = new Thickness(10, 10, 10, 10),

            });

            yodify.Clicked += Yodify_Clicked;

            this.Content = panel;
        }

        private async void Yodify_Clicked(object sender, EventArgs e)
        {
            activity.IsRunning = true;
            var url = new Uri($"https://yoda.p.mashape.com/yoda?sentence={phrase.Text}");
            var yodaResult = "";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Mashape-Key", "rV4HAvtbk7mshDk62x0aJ1frDwIGp13FqhIjsnZ5xnFgEuaWRh");
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = await client.GetAsync(url);
                yodaResult = await response.Content.ReadAsStringAsync();
            }

            activity.IsRunning = false;
            var answer = await DisplayAlert("Yoda says:", yodaResult, "Play", "Cancel");

            if (answer)
            {
                await CrossTextToSpeech.Current.Speak(yodaResult);
            }
            //DependencyService.Get<ITextToSpeech>().Speak(yodaResult);
        }
    }
}
