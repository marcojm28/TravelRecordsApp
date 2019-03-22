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
using Rg.Plugins.Popup.Services;

namespace TravelRecordsApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            //imageLogin.Source = ImageSource.FromResource("TravelRecordsApp.Img.AddUserIcon.png", assembly);

            //imageLogin.Source = ImageSource.FromFile("Img/geoImage.png");
        }

        

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            var LoadingPage = new CustomGIFLoader();

            

            await PopupNavigation.PushAsync(LoadingPage);

            using (var conn = new SQLite.SQLiteConnection(App._RUTABD))
            {
                
                try
                {

                    bool userEmpty, passwordEmpty;

                    userEmpty = String.IsNullOrEmpty(entryUser.Text);
                    passwordEmpty = String.IsNullOrEmpty(entryPassword.Text);

                    if (userEmpty)
                    {
                        await PopupNavigation.RemovePageAsync(LoadingPage);
                        await DisplayAlert("", "Debe ingresar un correo", "OK");
                    }
                    else if (passwordEmpty)
                    {
                        await PopupNavigation.RemovePageAsync(LoadingPage);
                        await DisplayAlert("", "Debe ingresar una contraseña", "OK");
                    }
                    else
                    {
                        if (!(entryUser.Text.Contains("@") && entryUser.Text.Contains(".com")))
                        {
                            await PopupNavigation.RemovePageAsync(LoadingPage);
                            await DisplayAlert("", "Ingrese una dirección de correo valida", "OK");
                        }
                        else
                        {
                            conn.CreateTable<User>();

                            User userLogin = new User()
                            {
                                EmailUser = entryUser.Text.Trim(),
                                PasswordUser = entryPassword.Text.Trim()
                            };

                            IEnumerable<User> query = conn.Query<User>("select * from User where EmailUser = ? and PasswordUser = ?", userLogin.EmailUser.Trim(), userLogin.PasswordUser.Trim());
                            List<User> listUser = query.ToList<User>();

                            if (listUser.Count > 0)
                            {
                                await Navigation.PushAsync(new HomePage());
                                await DisplayAlert("", "Bienvenido " + listUser[0].NameUser.ToString().Trim(), "Aceptar");
                            }
                            else
                            {
                                await PopupNavigation.RemovePageAsync(LoadingPage);
                                await DisplayAlert("", "Usuario o contraseña incorrectos.", "Aceptar");
                            }


                        }
                    }

                   
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message.ToString(), "Aceptar");
                }
                finally
                {
                    await PopupNavigation.RemovePageAsync(LoadingPage);
                }
            }
        }

        private async  void ButtonAddNewUser_Clicked(object sender, EventArgs e)
        {
            var LoadingPage = new CustomGIFLoader();
            await PopupNavigation.PushAsync(LoadingPage);
            Navigation.PushAsync(new AddUser());
            await PopupNavigation.RemovePageAsync(LoadingPage);
        }
    }
}
