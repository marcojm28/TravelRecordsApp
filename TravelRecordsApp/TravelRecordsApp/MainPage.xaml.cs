using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TravelRecordsApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            bool userEmpty,passwordEmpty;

            userEmpty = String.IsNullOrEmpty(entryUser.Text);
            passwordEmpty = String.IsNullOrEmpty(entryPassword.Text);

            if (userEmpty)
            {
                DisplayAlert("", "Debe ingresar un correo", "OK");
            }
            else if (passwordEmpty)
            {
                DisplayAlert("", "Debe ingresar una contraseña", "OK");
            }
            else
            {
                if(!(entryUser.Text.Contains("@") && entryUser.Text.Contains(".com")))
                    DisplayAlert("", "Ingrese una dirección de correo valida", "OK");
                else
                    Navigation.PushAsync(new HomePage());
            }
        }

        private void AddNewUser()
        {
            Navigation.PushAsync(new AddUser());
        }

        private void TapNewUser_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddUser());
        }
    }
}
