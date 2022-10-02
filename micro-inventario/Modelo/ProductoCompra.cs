using System;
using System.Collections.Generic;
using System.Text;
using micro_inventario.Datos;
using SQLite;
using SQLiteNetExtensions.Attributes;
namespace micro_inventario.Modelo
{
    public class ProductoCompra
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(Producto))]
        public int IdProducto { get; set; }

        [ForeignKey(typeof(Compras))]
        public int IdCompras { get; set; }
    }
}
