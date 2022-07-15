using System;
using System.Threading.Tasks;
using TestCode.Common;
using TestCode.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestCode
{
    public partial class App : Application
    {
        public static Request request { get; private set; }
        public App()
        {
            InitializeComponent();
            if (request == null)
                request = new Request();
            MainPage = new NavigationPage(new JokesPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
