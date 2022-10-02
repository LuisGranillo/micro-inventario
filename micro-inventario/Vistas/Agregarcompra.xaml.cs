using micro_inventario.Datos;
using micro_inventario.Modelo;
using micro_inventario.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace micro_inventario.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Agregarcompra : ContentPage
    {
        public Agregarcompra(Producto parametros)
        {
            InitializeComponent();
            BindingContext = new VMagregarcompra(Navigation, parametros);
        }
    }
}