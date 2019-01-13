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
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (var conn = new SQLite.SQLiteConnection(App._RUTABD))
            {
                try
                {
                    
                    conn.CreateTable<Post>();

                    List<Post> listaEnvio = new List<Post>();

                    listaEnvio = conn.Table<Post>().ToList();

                    ListViewPost.ItemsSource = listaEnvio;


                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message.ToString(), "Aceptar");
                }
            }
        }
    }
}