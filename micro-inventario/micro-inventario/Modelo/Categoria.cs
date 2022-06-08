using System;
using System.Collections.Generic;
using System.Text;
using micro_inventario.Datos;
using SQLite;

using SQLiteNetExtensions.Attributes;

namespace micro_inventario.Modelo
{
     class Categoria
    {

        [PrimaryKey, AutoIncrement]
        public int IdCategoria { get; set; }

        [MaxLength(60)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string Descripcion { get; set; }

        [MaxLength(50)]

        public string FechaRegistro { get; set; } = DateTime.MinValue.ToString();




        [ManyToMany(typeof(ProductoCategoria))]
        public List<Producto> Productos { get; set; }



    }
}
