﻿using System;
using System.Threading;
using TravelRecordsApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TravelRecordsApp
{
    public partial class App : Application
    {

        public static string _RUTABD;

        public App()
        {
            InitializeComponent();

            MainPage = new SplashPage();
            //MainPage = new NavigationPage(new MainPage());
            //Timer tm = new Timer(new TimerCallback((state) => {
            //this.InvokeOnMainThread(new Action(() => { }));
            //}), null, 2000, Timeout.Infinite);
            //MainPage = new NavigationPage(new MainPage()) { BarBackgroundColor = Color.Green };
        }

        public App(string rutaBD)
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage()) { BarBackgroundColor = Color.Green };
            //MainPage = new NavigationPage(new MainPage());
            MainPage = new SplashPage();


            _RUTABD = rutaBD;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
