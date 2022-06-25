using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using micro_inventario.Datos;
using micro_inventario.Modelo;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Timers;

namespace micro_inventario.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Categorias : ContentPage
    {
        ObservableCollection<FileImageSource> imageSources = new ObservableCollection<FileImageSource>();
        public Categorias()
        {
            InitializeComponent();
            this.BindingContext = this;
            imageSources.Add("lacteos.jpg");
            imageSources.Add("bebidas.jpg");
            imageSources.Add("legumbres.jpg");
            imageSources.Add("alimentosenlatados.jpg");
            imageSources.Add("dulces.jpg");
            imageSources.Add("electronica.jpg");
            imageSources.Add("frituras.jpg");
            imageSources.Add("frutasverduras.jpg");
            imageSources.Add("limpieza.jpg");
            imageSources.Add("mascotas.jpg");

            imgSlider.Images = imageSources;
        }
        private Timer timer;
        public ObservableCollection<Walkthrough> WalkthroughItems { get => Load(); } // toma los elementos y se cargan a la aplicación

        protected override void OnAppearing() //método propio de carrusel
        {
            timer = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds) { AutoReset = true, Enabled = true };
            timer.Elapsed += Timer_Elapsed;
            base.OnAppearing();
        }

        protected override void OnDisappearing() //método propio del sistema
        {
            timer?.Dispose();
            base.OnDisappearing();  
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if(cvWalkthrough.Position == 9)
                {
                    cvWalkthrough.Position = 0;
                    return;
                }

                cvWalkthrough.Position += 1;
            });
        }

        private ObservableCollection<Walkthrough> Load()
        { //Retornar objetos del tipo Walkthrough 
            return new ObservableCollection<Walkthrough>(new[]
            {
                new Walkthrough
                {
                    Heading = "Lácteos y Derivados",
                    Caption = "Explore the world from your own point.",
                    Image = "lacteos.jpg"
                },
                new Walkthrough
                {
                    Heading = "Legumbres a granel",
                    Caption = "Experience the blue and grey sights",
                    Image = "legumbres.jpg"
                },
                 new Walkthrough
                {
                    Heading = "Bebidas",
                    Caption = "Experience the blue and grey sights",
                    Image = "bebidas.jpg"
                },
                  new Walkthrough
                {
                    Heading = "Dulcería",
                    Caption = "Experience the blue and grey sights",
                    Image = "dulces.jpg"
                },
                   new Walkthrough
                {
                    Heading = "Productos de limpieza",
                    Caption = "Experience the blue and grey sights",
                    Image = "limpieza.jpg"
                },
                    new Walkthrough
                {
                    Heading = "Mascotas",
                    Caption = "Experience the blue and grey sights",
                    Image = "mascotas.jpg"
                },
                     new Walkthrough
                {
                    Heading = "Frutas y Verduras",
                    Caption = "Experience the blue and grey sights",
                    Image = "frutasverduras.jpg"
                },
                      new Walkthrough
                {
                    Heading = "Frituras",
                    Caption = "Experience the blue and grey sights",
                    Image = "frituras.jpg"
                },
                       new Walkthrough
                {
                    Heading = "Enlatados",
                    Caption = "Experience the blue and grey sights",
                    Image = "alimentosenlatados.jpg"
                },
                        new Walkthrough
                {
                    Heading = "Electrónica",
                    Caption = "Experience the blue and grey sights",
                    Image = "electronica.jpg"
                },

            });
        }
        public class Walkthrough
        {//clase del objeto Walkthrough, encabezado, una descripción y la imagen o recurso de imagen
            public string Heading { get; set; }
            public string Caption { get; set; }
            public string Image { get; set; }
        }
        private void consultar_Clicked(object sender, EventArgs e)
        {//Rellena el ListViewControl
            LlenarDatos();
            
        }

        private async void lstCategorias_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Categoria)e.SelectedItem;//e : es un argumento
            guardarCategoria.IsVisible = false;
            txtidCategoria.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.IdCategoria.ToString()))
            {
                var Categoria = await App.SQLiteDB.VerCategoriaIdAsync(obj.IdCategoria);
                if (Categoria != null)
                {
                    txtidCategoria.Text = Categoria.IdCategoria.ToString();
                    txtNombre.Text = Categoria.Nombre;
                    txtDescripcion.Text = Categoria.Descripcion;
                    txtFechaRegistro.Text = Categoria.FechaRegistro.ToString();

                }

            }
        }

        private void mostrarFecha_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("DATE", datepicker.Date.ToShortDateString(), "Ok");  
        }

        private async void guardarCategoria_Clicked(object sender, EventArgs e)
        {

            if (ValidarDatos())
            {
                Categoria cate = new Categoria()
                {
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    FechaRegistro = txtFechaRegistro.Text

                };
                await App.SQLiteDB.GuardarCategoria(cate);
                LimpiarControles();
                await DisplayAlert("Registro", "Se guardo de manera exitosa la Categoría", "ok");
                LlenarDatos();
            }
            else 
            {
                await DisplayAlert("Advertencia", "Ingrese todos los datos", "OK");
            }
        }

        private void datepicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            DateTime date = datepicker.Date;
            txtFechaRegistro.Text = $"{date.Date}";
        }

        public bool ValidarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty(txtFechaRegistro.Text))
            {
                respuesta = false;

            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        public void LimpiarControles()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtFechaRegistro.Text = "";
        }

        public async void LlenarDatos()
        {
            var catelist = await App.SQLiteDB.VerCategoriasAsync();
            if (catelist != null)
            {
                lstCategorias.ItemsSource = catelist;
            }

        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtidCategoria.Text))
            {
                Categoria cate = new Categoria()
                {
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    FechaRegistro = txtFechaRegistro.Text

                };
                await App.SQLiteDB.GuardarCategoria(cate);
                LimpiarControles();
                await DisplayAlert("Registro", "Se actualizó de manera exitosa la Categoría", "ok");
                LlenarDatos();
                txtidCategoria.IsVisible = false;
                btnActualizar.IsVisible = false;
                guardarCategoria.IsVisible = true;
                LlenarDatos();
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var categ = await App.SQLiteDB.VerCategoriaIdAsync(Convert.ToInt32(txtidCategoria.Text));
            if (categ != null)
            {
                await App.SQLiteDB.EliminarCategoriaAsync(categ);
                await DisplayAlert("Categoria", "Se eliminó de manera exitosa la Categoría", "ok");
                LimpiarControles();
                LlenarDatos();
                guardarCategoria.IsVisible = true;
                txtidCategoria.IsVisible = false;
                btnActualizar.IsVisible = false;
            }
        }
    }
}