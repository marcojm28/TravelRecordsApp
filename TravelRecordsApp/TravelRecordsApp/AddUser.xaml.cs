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
        public AddUser()
        {
            InitializeComponent();
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

                    
                    
                    if (String.IsNullOrEmpty(entryPassword.Text) || String.IsNullOrEmpty(entryConfirmPassword.Text) || String.IsNullOrEmpty(entryUser.Text) || String.IsNullOrEmpty(entryNombreApellido.Text))
                    {
                        if (String.IsNullOrEmpty(entryPassword.Text))
                        {
                            entryPassword.PlaceholderColor = Color.Red;
                            entryPassword.Placeholder = "Ingrese una contraseña";
                        }
                        

                        if (String.IsNullOrEmpty(entryConfirmPassword.Text))
                        {
                            entryConfirmPassword.PlaceholderColor = Color.Red;
                            entryConfirmPassword.Placeholder = "Confirme la contraseña";
                        }

                        if (String.IsNullOrEmpty(entryUser.Text))
                        {
                            entryUser.PlaceholderColor = Color.Red;
                            entryUser.Placeholder = "Ingrese el correo";
                        }

                        if (String.IsNullOrEmpty(entryNombreApellido.Text))
                        {
                            entryNombreApellido.PlaceholderColor = Color.Red;
                            entryNombreApellido.Placeholder = "Ingrese los nombres y apellidos";
                        }

                        return;
                    }

                    IEnumerable<User> query = conn.Query<User>("select * from User where EmailUser = ?", usuario.EmailUser.ToString().Trim());
                    List<User> listUser = query.ToList<User>();

                    //validación contraseña
                    if (entryPassword.Text != entryConfirmPassword.Text)
                    {
                        throw new Exception("La confirmación de la contraseña de coincide.");
                    }

                    //validación correo
                    if (listUser.Count > 0)
                    {
                        throw new Exception("La cuenta de correo ya fue registrada anteriormente.");
                    }
                    else
                    {
                        conn.Insert(usuario);
                        DisplayAlert("", "La cuenta fue registrada con Éxito", "Aceptar");
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