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
using Rg.Plugins.Popup.Services;

namespace TravelRecordsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewActivityPage : ContentPage
    {
        public NewActivityPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var LoadingPage = new CustomGIFLoader();
            try
            {
                await PopupNavigation.PushAsync(LoadingPage);

                var locator = CrossGeolocator.Current;

                var position = await locator.GetPositionAsync();

                var venue = await VenueLogic.GetVenues(position.Latitude, position.Longitude);

                ListViewVenue.ItemsSource = venue;

            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message.ToString(), "Aceptar");
            }
            finally
            {
                await PopupNavigation.RemovePageAsync(LoadingPage);
            }
        }

        private async void ToolBarGuardar_Clicked(object sender, EventArgs e)
        {
            var LoadingPage = new CustomGIFLoader();

            await PopupNavigation.PushAsync(LoadingPage);

            using (var conn = new SQLite.SQLiteConnection(App._RUTABD))
            {
                try
                {


                    if (!String.IsNullOrEmpty(entryNewActivity.Text))
                    {
                        var selectedVenue = ListViewVenue.SelectedItem as Venue;
                        var firtsCategory = selectedVenue.categories.FirstOrDefault();

                        Post post = new Post()
                        {
                            Experience = entryNewActivity.Text,
                            CategoryId = firtsCategory.id,
                            CategoryName = firtsCategory.name,
                            Address = selectedVenue.location.address,
                            Distance = selectedVenue.location.distance,
                            Longitude = selectedVenue.location.lng,
                            Latitude = selectedVenue.location.lat,
                            VenueName = selectedVenue.name,
                            DateTimeActivity = DateTime.Now

                        };

                        conn.CreateTable<Post>();
                        int row = conn.Insert(post);

                        if (row >= 1)
                        {
                            await PopupNavigation.RemovePageAsync(LoadingPage);
                            await DisplayAlert("", "La actividad fue ingresada correctamente", "Aceptar");
                            entryNewActivity.Text = string.Empty;
                        }
                        else
                        {
                            await PopupNavigation.RemovePageAsync(LoadingPage);
                            await DisplayAlert("Error", "No se insertó la actividad", "Aceptar");
                        }
                    }
                    else
                    {
                        await DisplayAlert("", "Ingrese la actividad.", "Aceptar");
                    }
                }
                catch (NullReferenceException nre)
                {

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
    }
}