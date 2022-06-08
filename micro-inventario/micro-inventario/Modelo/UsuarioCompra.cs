using System;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions.Attributes;
using SQLite;

namespace micro_inventario.Modelo
{
     class UsuarioCompra
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(Usuario))]
        public int IdUsuario { get; set; }

        [ForeignKey(typeof(Compras))]
        public int IdCompras { get; set; }
    }
}
