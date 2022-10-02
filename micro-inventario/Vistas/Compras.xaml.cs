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
    public partial class Compras : ContentPage
    {
        VMcompras vm;
        public Compras()
        {
            InitializeComponent();
            vm = new VMcompras(Navigation, Carrilderecha, carrilizquierda);
            BindingContext = vm;
            this.Appearing += Compras_Appearing;
        }

        private async void Compras_Appearing(object sender, EventArgs e)
        {
            await vm.MostrarPreviaCompra(mycollectionview, productos, imagen_produ, cantidadpro, totalpr, precio_or, pagar);
        }
        private async void Buscar_Clicked(object sender, EventArgs e)
        {
            VMcompras obj = new VMcompras(Navigation, Carrilderecha, carrilizquierda);

            await obj.Mostrarproductos(Carrilderecha, carrilizquierda, Buscar.Text);
            // compras.DibujarProductos();
        }

        private async void DeslizarPanelContador(object sender, SwipedEventArgs e)
        {
            await vm.MostrarPanelDC(GridProductos, paneldecompra, PanelContador);
        }

        private async void Deslizar(object sender, SwipedEventArgs e)
        {
            await vm.MostrarIdProductos(GridProductos, paneldecompra, PanelContador);
        }
    }
}