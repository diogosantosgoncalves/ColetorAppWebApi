using ColetorAPP.Services;
using ColetorAPP.Views;
using System;
using Xamarin.Forms;


namespace ColetorAPP
{
    public partial class App : Application
    {
        public static string DbName;
        public static string DbPath;
        public static ServicesDBProduto BancoDeDados { get; private set; }


        public App()
        {
            InitializeComponent();
            MainPage = new PagePrincipal();
        }
        public App(String dbPath, string dbName)
        {
            InitializeComponent();

            App.DbName = dbName;
            App.DbPath = dbPath;
            //BancoDeDados = new ServicesDBNotas(dbPath);

            //MainPage = new PagePrincipal();
            //MainPage = new PageLogin();
            MainPage = new NavigationPage(new PageLogin());
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
