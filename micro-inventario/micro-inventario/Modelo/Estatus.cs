using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;
namespace micro_inventario.Modelo
{
     class Estatus
    {
        [PrimaryKey, AutoIncrement]
        public int IdEstatus { get; set; }

        [MaxLength(60)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string Fecha_Registro { get; set; } = DateTime.MinValue.ToString();

        [OneToMany]
        public List<Usuario> Usuarios { get; set; }
    }
}
