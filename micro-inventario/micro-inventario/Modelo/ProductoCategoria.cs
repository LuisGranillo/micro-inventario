using System;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions.Attributes;
using SQLite;
using micro_inventario.Datos;

namespace micro_inventario.Modelo
{
     class ProductoCategoria
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(Categoria))]
        public int IdCategoria { get; set; }

        [ForeignKey(typeof(Producto))]
        public int IdProducto { get; set; }
    }
}
