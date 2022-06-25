using micro_inventario.Datos;
using micro_inventario.Modelo;
using micro_inventario.Vistas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace micro_inventario.VistaModelo
{
    public class VMcompras:BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        int _index;
        List<Producto> _listaproductos;
        #endregion
        #region CONSTRUCTOR
        public VMcompras(INavigation navigation, StackLayout Carrilderecho, StackLayout Carrilizquierdo)
        {
            Navigation = navigation;
            Mostrarproductos(Carrilderecho,Carrilizquierdo);
        }
        #endregion
        #region OBJETOS
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
        public async Task Mostrarproductos(StackLayout Carrilderecho, StackLayout Carrilizquierdo)
        {
            var produList = await App.SQLiteDB.ObtenerProductosAsync();

            Listaproductos =  produList;
            var box = new BoxView
            {
                HeightRequest=60,
               


            };
            Carrilizquierdo.Children.Clear();
            Carrilderecho.Children.Clear();
            Carrilderecho.Children.Add(box);
            foreach(var item in Listaproductos)
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
        #endregion
        #region COMANDOS
        public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}

