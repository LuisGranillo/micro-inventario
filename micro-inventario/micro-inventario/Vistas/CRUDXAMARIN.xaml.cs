using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using micro_inventario.Datos;
using micro_inventario.Modelo;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace micro_inventario.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CRUDXAMARIN : ContentPage
    {
        public CRUDXAMARIN()
        {
            InitializeComponent();
            
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Producto pro = new Producto
                {
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    precio = double.Parse(txtPrecio.Text),
                    Imagen = txturlimage.Text,
                    UnidadMedida = float.Parse(txtunidadm.Text),
                    Marca = txtmarca.Text,
                    CantidadesExistentes = int.Parse(txtCantidadesExistentes.Text),
                    Caducidad = txtcaducidad.ToString(),
                  

                };
                await App.SQLiteDB.GuardarProducto(pro);
                LimpiarControles();
                await DisplayAlert("Registro", "Se guardo de manera exitosa el Producto", "ok");
                LlenarDatos();
            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los datos", "OK");
            }
           

        }
        public async void LlenarDatos()
        {
            var produList = await App.SQLiteDB.ObtenerProductosAsync();
            if (produList != null)
            {
                lstProductos.ItemsSource = produList;
            }

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
            else if (string.IsNullOrEmpty(txtPrecio.Text))
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
            txtPrecio.Text = "";
            txtidProducto.Text = "";
            txtunidadm.Text = "";
            txtmarca.Text = "";
            txtCantidadesExistentes.Text = "";
       

        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtidProducto.Text))
            {
                Producto prod = new Producto()
                {
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    precio = double.Parse(txtPrecio.Text),
                    Imagen = txturlimage.Text,
                    UnidadMedida = float.Parse(txtunidadm.Text),
                    Marca = txtmarca.Text,
                    CantidadesExistentes = int.Parse(txtCantidadesExistentes.Text),
                    Caducidad = txtcaducidad.ToString(),


                };
                await App.SQLiteDB.GuardarProducto(prod);
                await DisplayAlert("Registro", "Se actualizo de manera exitosa el Producto", "ok");
                LimpiarControles();
                txtidProducto.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                LlenarDatos();
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var produc = await App.SQLiteDB.ObtenerProductoIdAsync(Convert.ToInt32(txtidProducto.Text));
            if (produc != null)
            {
                await App.SQLiteDB.EliminarProductoAsync(produc);
                await DisplayAlert("Producto", "Se elimino de manera exitosa el Producto", "ok");
                LimpiarControles();
                LlenarDatos();
                btnRegistrar.IsVisible = true;
                txtidProducto.IsVisible = false;
                btnActualizar.IsVisible = false;
            }
        }

        private async void lstProductos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Producto)e.SelectedItem;//e : es un argumento
            btnRegistrar.IsVisible = false;
            txtidProducto.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.IdProducto.ToString()))
            {
                var Producto = await App.SQLiteDB.ObtenerProductoIdAsync(obj.IdProducto);
                if (Producto != null)
                {
                    txtidProducto.Text = Producto.IdProducto.ToString();
                    txtNombre.Text = Producto.Nombre;
                    txtDescripcion.Text = Producto.Descripcion;
                    txtPrecio.Text = Producto.precio.ToString();


                }

            }

        }

        private void Subir_imagen_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new subir_imagen());
        }

        private void Subir_imagen_crud_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new subir_imagen());

        }

        private void Catalogo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Compras());

        }
    }
}