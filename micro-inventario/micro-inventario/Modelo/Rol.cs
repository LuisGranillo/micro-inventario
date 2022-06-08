using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace micro_inventario.Modelo
{
     class Rol
    {
        [PrimaryKey, AutoIncrement]
        public int IdRol { get; set; }

        [MaxLength(60)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string Fecha { get; set; } = DateTime.MinValue.ToString();


        [OneToMany]
        public List<Usuario> Usuarios { get; set; }
    }
}
