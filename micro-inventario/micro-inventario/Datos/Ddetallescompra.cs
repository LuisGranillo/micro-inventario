using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database.Query;
using System.Linq;
using Firebase.Database;
using System.Threading.Tasks;
using micro_inventario.Modelo;
using micro_inventario.Conexiones;
namespace micro_inventario.Datos
{
    public class Ddetallescompra
    {
        public async Task InsertarDc(Mdetallecompras parametros)
        {
            await conexion.firebase
                .Child("Detallecompra")
                .PostAsync(new Mdetallecompras()
                {
                    Cantidad= parametros.Cantidad,
                    Idproducto= parametros.Idproducto,
                    Preciocompra= parametros.Preciocompra,
                    Total=parametros.Total
                });
        }
    }
}
