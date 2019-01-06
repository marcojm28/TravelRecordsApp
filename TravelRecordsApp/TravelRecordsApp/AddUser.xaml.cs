using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TravelRecordsApp.Model;

namespace TravelRecordsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddUser : ContentPage
	{
		public AddUser ()
		{
			InitializeComponent ();
		}

        private void ButtonAddNewUser_Clicked(object sender, EventArgs e)
        {
            using (var conn = new SQLite.SQLiteConnection(App._RUTABD))
            {
                try
                {
                    User usuario = new User()
                    {
                        EmailUser = entryUser.Text,
                        PasswordUser = entryPassword.Text,
                        NameUser = entryNombreApellido.Text
                    };
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message.ToString(), "Aceptar");
                }
            }
        }
    }
}