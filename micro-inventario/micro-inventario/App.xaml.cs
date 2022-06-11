using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using micro_inventario.Datos;
using System.IO;
using micro_inventario.Vistas;

namespace micro_inventario
{
    public partial class App : Application
    {
        static SQLiteCon db;//Dentro de la Clase

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage (new CRUDXAMARIN());
        }
        public static SQLiteCon SQLiteDB
        {

            get
            {
                if (db == null)
                {
                    db = new SQLiteCon(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Producto.db3"));
                }
                return db;
            }
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
