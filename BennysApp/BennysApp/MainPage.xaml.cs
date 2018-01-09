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
        Entry phrase;
        Button yodify;

        public MainPage()
        {

            //this.Padding = new Thickness(20, 20, 20, 20);
            var panel = new StackLayout
            {
                Spacing = 15,
                BackgroundColor = Color.Black
            };

            panel.Children.Add(new Image
            {
                
            });

            panel.Children.Add(new Label
            {
                Text = "Enter a shit:",
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(phrase = new Entry
            {
                Placeholder = "Phrase",
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(yodify = new Button
            {
                Text = "Yodify",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            yodify.Clicked += Yodify_Clicked;

            this.Content = panel;
            
        }

        private async void Yodify_Clicked(object sender, EventArgs e)
        {
            var url = new Uri($"https://yoda.p.mashape.com/yoda?sentence={phrase.Text}");
            var yodaResult = "";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Mashape-Key", "7Oz7m29qHXmshPYFl3OCqvRnZAzVp1HnHVRjsniZtXkGB7DXMq");
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = await client.GetAsync(url);
                yodaResult = await response.Content.ReadAsStringAsync();
            }

            await CrossTextToSpeech.Current.Speak(yodaResult);

            await DisplayAlert("Yoda says:", yodaResult, "OK", " ");
            //DependencyService.Get<ITextToSpeech>().Speak(yodaResult);
        }
    }
}
