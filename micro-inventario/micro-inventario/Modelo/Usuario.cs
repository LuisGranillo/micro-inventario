using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace micro_inventario.Modelo
{
     class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int IdUsuario { get; set; }



        [MaxLength(50)]
        public string Nombre { get; set; }


        [MaxLength(30)]
        public string Apellidos { get; set; }


        [MaxLength(100)]
        public string Email { set; get; }


        [MaxLength(100)]
        public string password { set; get; }





        /*Movi de auqi para abajo*/
        [ForeignKey(typeof(Rol))]
        public int id_rol { set; get; }
        [OneToOne]
        public Rol Rol { get; set; }



        [ForeignKey(typeof(Estatus))]
        public int id_est { get; set; }
        [OneToOne]
        //public int 
        public Estatus Estatus { set; get; }


        [ManyToMany(typeof(Compras))]
        public List<Compras> compras { get; set; }


    }
}
