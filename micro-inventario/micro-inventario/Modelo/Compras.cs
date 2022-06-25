using System;
using System.Collections.Generic;
using System.Text;
using micro_inventario.Datos;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace micro_inventario.Modelo
{
    public class Compras
    {
        [PrimaryKey, AutoIncrement]
        public int idCompra { get; set; }

        [MaxLength(10)]
        public string FechaCompra { get; set; } = DateTime.MinValue.ToString();




        [ForeignKey(typeof(Producto))]
        public int idProducto { get; set; }


        [ManyToMany(typeof(Producto))]
        public List<Producto> Productos { get; set; }

        [ManyToMany(typeof(Usuario))]
        public List<Usuario> usuarios { get; set; }





    }
}
