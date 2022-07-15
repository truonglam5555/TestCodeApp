using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCode.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestCode.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JokesPage : ContentPage
    {
        VMJokes ViewModel;
        public JokesPage()
        {
            InitializeComponent();
            ViewModel = new VMJokes();
            this.BindingContext = ViewModel;
            ViewModel.CallBackDone += CallBackDone;
        }

        async void CallBackDone()
        {
            await Task.Delay(300);
            await grdControl.FadeTo(0);
            aiControl.IsRunning = false;
            bodyContent.Children.Remove(grdControl);
            ViewModel.NextJoke();
        }
    }
}