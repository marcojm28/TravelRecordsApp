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
                    conn.CreateTable<User>();

                    User usuario = new User()
                    {
                        EmailUser = entryUser.Text,
                        PasswordUser = entryPassword.Text,
                        NameUser = entryNombreApellido.Text
                    };

                    //IEnumerable<User> foo = db.Query<User>("select * from ESItemdb where Group = \"Messier\"");
                    IEnumerable<User> query = conn.Query<User>("select * from User where EmailUser = ?",usuario.EmailUser.ToString().Trim());
                    List<User> listUser = query.ToList<User>();

                    if (listUser.Count > 0)
                    {
                        throw new Exception("La cuenta de correo ya fue registrada anteriormente.");
                    }
                    else
                    {
                        conn.Insert(usuario);
                        DisplayAlert("Éxito", "La cuenta fue registrada con Éxito", "Aceptar");
                    }

                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message.ToString(), "Aceptar");
                }
            }
        }
    }
}