using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Plugin.TextToSpeech;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BennysApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Yodify : ContentPage
    {
        readonly Entry _phrase;
        readonly Button _yodify;
        readonly ActivityIndicator _activity;

        public Yodify()
        {
            InitializeComponent();

            this.Padding = new Thickness(20, 20, 20, 20);

            var panel = new StackLayout
            {
                Spacing = 15
            };

            panel.Children.Add(_activity = new ActivityIndicator
            {
                IsRunning = false,
            });

            panel.Children.Add(new Label
            {
                Text = "Enter a phrase:",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(_phrase = new Entry
            {
                Placeholder = "Phrase",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            });

            panel.Children.Add(_yodify = new Button
            {
                Text = "Yodify",
                BackgroundColor = Color.Green,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(new Image
            {
                Source = ImageSource.FromUri(new Uri("https://i.ebayimg.com/images/g/NvgAAOSwHPlWgxnU/s-l1600.jpg")),
                Margin = new Thickness(10,10,10,10)
                
            });

            _yodify.Clicked += Yodify_Clicked;

            this.Content = panel;
        }

        private async void Yodify_Clicked(object sender, EventArgs e)
        {
            _activity.IsRunning = true;
            var url = new Uri($"https://yoda.p.mashape.com/yoda?sentence={_phrase.Text}");
            var yodaResult = "";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Mashape-Key", "7Oz7m29qHXmshPYFl3OCqvRnZAzVp1HnHVRjsniZtXkGB7DXMq");
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = await client.GetAsync(url);
                yodaResult = await response.Content.ReadAsStringAsync();
            }
            _activity.IsRunning = false;
            var answer = await DisplayAlert("Yoda says:", yodaResult, "Play", "Cancel");
            
            if (answer)
            {
                await CrossTextToSpeech.Current.Speak(yodaResult);
            }
            //DependencyService.Get<ITextToSpeech>().Speak(yodaResult);
        }
    }
}