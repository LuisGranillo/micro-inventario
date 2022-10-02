using micro_inventario.Datos;
using micro_inventario.Modelo;
using micro_inventario.Vistas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Compras = micro_inventario.Modelo.Compras;
using Plugin.SharedTransitions;

namespace micro_inventario.VistaModelo
{
    public class VMcompras:BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        int _index;
        List<Producto> _listaproductos;
        List<Compras> _listaPreviaCompra;
        bool _isvisiblePanelDetalleCompras;

        #endregion
        #region CONSTRUCTOR
        public VMcompras(INavigation navigation, StackLayout Carrilderecho, StackLayout Carrilizquierdo)
        {
            Navigation = navigation;
            Mostrarproductos(Carrilderecho,Carrilizquierdo);
        }
        #endregion
        #region OBJETOS
        public List<Modelo.Compras> ListaPreviaCompra
        {
            get { return _listaPreviaCompra; }
            set { SetValue(ref _listaPreviaCompra, value); }
        }
        public bool IsvisiblePanelDC
        {
            get { return _isvisiblePanelDetalleCompras; }
            set { SetValue(ref _isvisiblePanelDetalleCompras, value); }
        }
        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }
        public List<Producto> Listaproductos
        {
            get { return _listaproductos; }
            set { SetValue(ref _listaproductos, value); }
        }
        #endregion
        #region PROCESOS
        public async Task Mostrarproductos(StackLayout Carrilderecho, StackLayout Carrilizquierdo,string nombre="")
        {
            var produList = await App.SQLiteDB.ObtenerProductosAsync(); 

            if (nombre != "")
            {

                produList = await App.SQLiteDB.BuscarProducto(nombre);

            }


            // var buscar = await App.SQLiteDB.BuscarProducto();
            Listaproductos = produList;
            if (_listaproductos == null)
            {
                await DisplayAlert("Mensaje", "No se encontraron resultados", "Aceptar");

                await Navigation.PopAsync();
            }


            var box = new BoxView
            {
                HeightRequest = 60,
            };
            Carrilizquierdo.Children.Clear();
            Carrilderecho.Children.Clear();
            Carrilderecho.Children.Add(box);
            foreach (var item in Listaproductos)
            {
                DibujarProductos(item, _index, Carrilderecho, Carrilizquierdo);
                _index++;
            }

        }
        public void DibujarProductos(Producto item,int index,StackLayout Carrilderecha,StackLayout carrilizquiderda)
        {
            var _ubicacion = Convert.ToBoolean(index % 2);
            var carril = _ubicacion ? Carrilderecha : carrilizquiderda;
            var frame = new Frame
            {
                                  HeightRequest = 290,
                                  CornerRadius = 10,
                                   Margin = 8,
                                   HasShadow = false,
                                   BackgroundColor =Color.White,
                                   Padding = 22,
            };
            var stak = new StackLayout
            {

            };
            var img = new Image
            {
                                     Source = item.Imagen,
                                           HeightRequest = 150,
                                           HorizontalOptions = LayoutOptions.Center,
                                           Margin = new Thickness (0,10),
            };
            var labelprecio = new Label
            {
                Text = "$"+item.precio,
                                           FontAttributes = FontAttributes.Bold,
                                           FontSize = 22,
                                           Margin = new Thickness (0,10),
                                           TextColor =Color.FromHex ("#333333"),
            };
            var labeldescripcion = new Label
            {
                                             Text = item.Descripcion,
                                           FontSize = 16,
                                           TextColor = Color.Black,
                                           CharacterSpacing = 1,
            };
            var labelpeso = new Label
            {
                                          Text= item.UnidadMedida.ToString(),
                                           FontSize = 13,
                                           TextColor =Color.FromHex ("#CCCCCC"),
                                           CharacterSpacing = 1
            };
            stak.Children.Add(img);
            stak.Children.Add(labelprecio);
            stak.Children.Add(labeldescripcion);
            stak.Children.Add(labelpeso);
            frame.Content = stak;
            var tap = new TapGestureRecognizer();
            tap.Tapped += async (object sender, EventArgs e) =>
             {
                 var page = (App.Current.MainPage as SharedTransitionNavigationPage).CurrentPage;
                 SharedTransitionNavigationPage.SetBackgroundAnimation(page, BackgroundAnimation.Flip);
                 SharedTransitionNavigationPage.SetTransitionDuration(page, 1000);


                 SharedTransitionNavigationPage.SetTransitionSelectedGroup(page, item.IdProducto.ToString());
                 await Navigation.PushAsync(new Agregarcompra(item));

 
             };
            carril.Children.Add(frame);
            stak.GestureRecognizers.Add(tap);
        }
        public async Task ProcesoAsyncrono()
        {

        }
        public void ProcesoSimple()
        {

        }
        public async Task MostrarPreviaCompra(CollectionView Compras, Label Marca, Image imagen, Label cantidad, Label totalpr, Label list_pr, Label pagar)
        {
            ListaPreviaCompra = await App.SQLiteDB.VerTodasComprasAsync();
            double total = 0;
            Compras.ItemsSource = ListaPreviaCompra;

            foreach (var item in Listaproductos)
            {
                foreach (var item2 in ListaPreviaCompra)
                {

                    if (item.IdProducto == item2.idProducto)
                    {

                        Marca.Text = item.Marca;
                        imagen.Source = item.Imagen;
                        list_pr.Text = item.precio.ToString();
                        cantidad.Text = item2.Cantidad;
                        totalpr.Text = item2.Total;

                        double pagarcompra = Convert.ToDouble(item2.Total);

                        double totalpagar = await App.SQLiteDB.Sumatoria();
                        pagar.Text = totalpagar.ToString();

                        // pagar.Text=total_compra = item2.Total + item2.Total;
                    }
                    _index++;
                }
                _index++;
            }



        }

        public async Task MostrarPanelDC(Grid gridproductos, StackLayout panelDC, StackLayout panelc)
        {
            uint duracion = 700;
            await Task.WhenAll(
                panelc.FadeTo(0, 500),
                gridproductos.TranslateTo(0, -200, duracion + 200, Easing.CubicIn),
                panelDC.TranslateTo(0, -300, duracion, Easing.CubicIn)

                );
            IsvisiblePanelDC = true;
        }
        public async Task MostrarIdProductos(Grid gridproductos, StackLayout panelDC, StackLayout panelc)
        {
            uint duracion = 700;
            await Task.WhenAll(
                panelc.FadeTo(1, 500),
                gridproductos.TranslateTo(0, 0, duracion + 200, Easing.CubicIn),
                panelDC.TranslateTo(0, 1000, duracion, Easing.CubicIn)

                );
            IsvisiblePanelDC = false;
        }
        #endregion
        #region COMANDOS
        public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}

