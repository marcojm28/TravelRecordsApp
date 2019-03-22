﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Plugin.Permissions;
using FFImageLoading.Forms.Droid;
using Lottie.Forms.Droid;

namespace TravelRecordsApp.Droid
{
    [Activity(Label = "GeolocalitationApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            AnimationViewRenderer.Init();
            //maps
            Xamarin.FormsMaps.Init(this, savedInstanceState);

            //permission
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);

            string nombreArchivo = "bd_travelRecords.sqlite";
            string rutaCarpeta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            LoadApplication(new App(rutaCompleta));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode,permissions,grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}