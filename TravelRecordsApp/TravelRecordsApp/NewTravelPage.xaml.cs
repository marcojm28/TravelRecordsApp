using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TravelRecordsApp.Model;
using Plugin.Geolocator;
using TravelRecordsApp.Logic;

namespace TravelRecordsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;

            var position = await locator.GetPositionAsync();

            var venue = await VenueLogic.GetVenues(position.Latitude,position.Longitude);

            ListViewVenue.ItemsSource = venue;
        }

        private void ToolBarGuardar_Clicked(object sender, EventArgs e)
        {
            using (var conn = new SQLite.SQLiteConnection(App._RUTABD))
            {
                try
                {
                    Post post = new Post()
                    {
                        Experience = entryExperience.Text
                    };

                    conn.CreateTable<Post>();
                    int row = conn.Insert(post);

                    if (row >= 1)
                    {
                        DisplayAlert("Éxito", "La experiencia fue ingresada correctamente", "Aceptar");
                        entryExperience.Text = "";
                    }
                    else
                    {
                        DisplayAlert("Fracaso", "No se insertó la experiencia", "Aceptar");
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