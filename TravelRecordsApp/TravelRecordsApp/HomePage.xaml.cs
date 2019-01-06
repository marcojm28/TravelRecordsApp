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
	public partial class HomePage : TabbedPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		}

        private void ToolbarHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewTravelPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (var conn = new SQLite.SQLiteConnection(App._RUTABD))
            {
                try
                {
                    List<Post> listPost = new List<Post>();

                    conn.CreateTable<Post>();

                    listPost = conn.Table<Post>().ToList();

                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message.ToString(), "Aceptar");
                }

            }

        }
    }
}