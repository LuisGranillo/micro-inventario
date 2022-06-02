using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using micro_inventario.Vistas;
namespace micro_inventario
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage (new Compras());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
