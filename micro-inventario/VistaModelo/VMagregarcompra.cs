using micro_inventario.Datos;
using micro_inventario.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.SharedTransitions;
namespace micro_inventario.VistaModelo
{
    public class VMagregarcompra : BaseViewModel
    {
        #region VARIABLES
        int _Cantidad;
        string _Preciotexto;
        string _Descripcion_prd;
        public Producto parametrosrecibe { get; set; }
        List<Compras> _compraspro;

        #endregion
        #region CONSTRUCTOR
        public VMagregarcompra(INavigation navigation, Producto parametrosTrae)
        {
            Navigation = navigation;
            parametrosrecibe = parametrosTrae;
            Preciotexto = "$" + parametrosrecibe.precio;
            Descripcion_prd = parametrosrecibe.Descripcion;
        }
        #endregion
        #region OBJETOS
        public string Preciotexto
        {
            get { return _Preciotexto; }
            set { SetValue(ref _Preciotexto, value); }
        }
        public string Descripcion_prd
        {
            get { return _Descripcion_prd; }
            set { SetValue(ref _Descripcion_prd, value); }
        }
        public List<Modelo.Compras> Compraspro
        {
            get { return _compraspro; }
            set { SetValue(ref _compraspro, value); }
        }
        public int Cantidad
        {
            get { return _Cantidad; }
            set { SetValue(ref _Cantidad, value); }
        }
        #endregion
        #region PROCESOS

        public async Task ProcesoAsyncrono()
        {

        }
        public void ProcesoSimple()
        {

        }
        #endregion
        #region COMANDOS

        public async Task InsertarDc()
        {
            if (Cantidad == 0)
            {
                Cantidad = 1;
            }


            var parametros = new Compras();
            var relacioncomrpasproducto = new ProductoCompra();
            parametros.Cantidad = Cantidad.ToString();
            parametros.idProducto = parametrosrecibe.IdProducto;
            relacioncomrpasproducto.IdProducto = parametrosrecibe.IdProducto;

            //relacioncomrpasproducto. = parametrosrecibe.precio;

            double total = 0;
            double preciocompra = Convert.ToDouble(parametrosrecibe.precio);
            double cantidad = Convert.ToDouble(Cantidad);
            total = cantidad * preciocompra;
            parametros.Total = total.ToString();
            await App.SQLiteDB.Comprar(parametros);
            await Volver();
        }

        public async Task Volver()
        {
         await Navigation.PopAsync();
        }
        public void Aumentar()
        {
            Cantidad += 1;
        }
        public void Disminuir()
        {
            if (Cantidad > 0)
            {
                Cantidad -= 1;
            }
        }
        public ICommand Volvercommand => new Command(async () => await Volver());
        public ICommand Aumentarcommand => new Command(Aumentar);
        public ICommand Disminuircommand => new Command(Disminuir);
        public ICommand Insertarcommand => new Command(async () => await InsertarDc());
        #endregion
    }
}
