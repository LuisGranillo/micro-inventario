using System;
using System.Collections.Generic;
using System.Text;
using micro_inventario.Modelo;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace micro_inventario.Datos
{
  
        class Producto
        {
            [PrimaryKey, AutoIncrement]
            public int IdProducto { get; set; }

            [MaxLength(80)]
            public string Nombre { get; set; }

            [MaxLength(7)]
            public double precio { get; set; }

            [MaxLength(256)]
            public string Imagen { get; set; }

            [MaxLength(8)]
            public float UnidadMedida { get; set; }

            [MaxLength(150)]
            public string Marca { get; set; }

            [MaxLength(8)]
            public int CantidadesExistentes { get; set; }

            [MaxLength(20)]
            public string Caducidad { get; set; } = DateTime.MinValue.ToString();


            [MaxLength(300)]
            public string Descripcion { get; set; }

            /*
             MOVI DE AQUI PARA ABAJO
                        */

            [ForeignKey(typeof(Estatus))]
            public int idEstatus { get; set; }

            [ForeignKey(typeof(Categoria))]
            public int idCategoria { get; set; }


            [OneToOne]
            public Estatus Estatus { get; set; }

            [ManyToMany(typeof(ProductoCategoria))]
            public List<Categoria> Categorias { get; set; }


        }
    }

