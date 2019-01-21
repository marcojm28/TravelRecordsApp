using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using TravelRecordsApp.Model;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

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
            using (var conn = new SQLite.SQLiteConnection(App._RUTABD))
            {
                try
                {
                    bool userEmpty, passwordEmpty;

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
                        if (!(entryUser.Text.Contains("@") && entryUser.Text.Contains(".com")))
                        {
                            DisplayAlert("", "Ingrese una dirección de correo valida", "OK");
                        }
                        else
                        {
                            conn.CreateTable<User>();

                            User userLogin = new User()
                            {
                                EmailUser = entryUser.Text.Trim(),
                                PasswordUser = entryPassword.Text.Trim()
                            };

                            IEnumerable<User> query = conn.Query<User>("select * from User where EmailUser = ? and PasswordUser = ?", userLogin.EmailUser.Trim(),userLogin.PasswordUser.Trim());
                            List<User> listUser = query.ToList<User>();

                            if (listUser.Count > 0)
                            {
                                DisplayAlert("", "Bienvenido " + listUser[0].NameUser.ToString().Trim(), "Aceptar");
                                Navigation.PushAsync(new HomePage());
                            }
                            else
                            {
                                DisplayAlert("", "Usuario o contraseña incorrectos.", "Aceptar");
                            }

                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message.ToString(), "Aceptar");
                }
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
