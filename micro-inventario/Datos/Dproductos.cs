using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using micro_inventario.Modelo;
using micro_inventario.Conexiones;
namespace micro_inventario.Datos
{
    public class Dproductos
    {
        public async Task<List<Mproductos>> MostrarProductos()

        {
          return  (await conexion.firebase
               .Child("Productos")
               .OnceAsync<Mproductos>()).Select(item => new Mproductos
               {
                   Descripcion = item.Object.Descripcion,
                   Icono = item.Object.Icono,
                   Precio = item.Object.Precio,
                   Peso = item.Object.Peso,
                   Idproducto = item.Key
               }).ToList();
        }
           
    }
}
