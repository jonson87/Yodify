using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BennysApp.Views;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace BennysApp
{
	public partial class App : Application
	{
		public App ()
		{
		    InitializeComponent();

		    MainPage = new NavigationPage(new HomePage());

        }

        protected override void OnStart ()
		{
		    AppCenter.Start("android=98462014-5db7-4429-b6b5-0814698bd397;",
		        typeof(Analytics), typeof(Crashes));
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
