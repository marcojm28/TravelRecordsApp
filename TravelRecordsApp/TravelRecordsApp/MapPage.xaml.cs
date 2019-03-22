using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using TravelRecordsApp.Model;
using Rg.Plugins.Popup.Services;

namespace TravelRecordsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {

        private bool hasLocationPermission = false;

        public MapPage()
        {
            InitializeComponent();

            GetPermissions();

        }

        private async void GetPermissions()
        {
            var LoadingPage = new CustomGIFLoader();

            try
            {

                
                await PopupNavigation.PushAsync(LoadingPage);

                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);

                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                    {
                        await PopupNavigation.RemovePageAsync(LoadingPage);
                        await DisplayAlert("", "Se necesita acceder a la ubicación del dispositivo", "Aceptar");
                    }

                    var result = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);

                    if (result.ContainsKey(Permission.LocationWhenInUse))
                    {
                        status = result[Permission.LocationWhenInUse];
                    }
                }

                if (status == PermissionStatus.Granted)
                {
                    hasLocationPermission = true;
                    mapLocation.IsShowingUser = true;

                    GetLocation();
                }
                else
                {
                    await PopupNavigation.RemovePageAsync(LoadingPage);
                    await DisplayAlert("Localización denegada", "no se tiene acceso a la ubicación del dispositivo", "Aceptar");
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;

                locator.PositionChanged += LocatorPositionChanged;
                await locator.StartListeningAsync(TimeSpan.Zero, 50);
            }

            GetLocation();

            using (var conn = new SQLite.SQLiteConnection(App._RUTABD))
            {
                conn.CreateTable<Post>();

                List<Post> listaEnvio = new List<Post>();

                listaEnvio = conn.Table<Post>().ToList();

                DisplayInMap(listaEnvio);


            }
        }

        private void DisplayInMap(List<Post> listPost)
        {
            foreach (var post in listPost)
            {
                try
                {
                    var Position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);

                    var Pin = new Xamarin.Forms.Maps.Pin()
                    {
                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                        Position = Position,
                        Label = post.VenueName,
                        Address = post.Address
                    };

                    mapLocation.Pins.Add(Pin);

                }
                catch (NullReferenceException nre) { }
                catch (Exception ex) { }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= LocatorPositionChanged;

        }

        public void LocatorPositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            MoveMap(e.Position);
        }

        private void MoveMap(Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 0.01, 0.01);

            mapLocation.MoveToRegion(span);
        }

        private async void GetLocation()
        {
            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();

                MoveMap(position);

                
            }
        }
    }
}