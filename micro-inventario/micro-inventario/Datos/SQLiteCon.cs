using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using micro_inventario.Modelo;
using System.Threading.Tasks;
using micro_inventario.Datos;

namespace Sprint1.Data
{
    class SQLiteCon
    {
        SQLiteAsyncConnection db;

        public SQLiteCon(string dbPath)//Crear la base de datos
        {
            db = new SQLiteAsyncConnection(dbPath); //conexion
            db.CreateTableAsync<Usuario>().Wait();
            db.CreateTableAsync<Rol>().Wait();
            db.CreateTableAsync<Estatus>().Wait();
            db.CreateTableAsync<Categoria>().Wait();
            db.CreateTableAsync<Producto>().Wait();
            db.CreateTableAsync<Compras>().Wait();
        }


        public Task<int> GuardarProducto(Producto produ)
        {
            if (produ.IdProducto != 0)//SE CAMBIO POR DIFERENTE DE CERO
                return db.UpdateAsync(produ);

            else
            {
                return db.InsertAsync(produ);
            }

        }

        public Task<int> EliminarProductoAsync(Producto pro)
        {

            return db.DeleteAsync(pro);
        }

        public Task<List<Producto>> ObtenerProductosAsync()//Recupera todos los Productos
        {
            return db.Table<Producto>().ToListAsync();
        }

        public Task<Producto> ObtenerProductoIdAsync(int idProducto)//Recupera X id
        {
            return db.Table<Producto>().Where(i => i.IdProducto == idProducto).FirstOrDefaultAsync();
        }



        ///////////USUARIO 



        public Task<int> CrearUsuario(Usuario usu)
        {
            if (usu.IdUsuario != 0)//SE CAMBIO POR DIFERENTE DE CERO
                return db.UpdateAsync(usu);

            else
            {
                return db.InsertAsync(usu);
            }

        }

        public Task<int> EliminarUsuarioAsync(Usuario usu)
        {

            return db.DeleteAsync(usu);
        }

        public Task<List<Usuario>> TodosUsuariosAsync()//Recupera LOS USUARIOS
        {
            return db.Table<Usuario>().ToListAsync();
        }

        public Task<Usuario> UsuarioIdAsync(int Id)//Recupera X id
        {
            return db.Table<Usuario>().Where(i => i.IdUsuario == Id).FirstOrDefaultAsync();
        }

        //////////////////////////////////CATEGORIA

        public Task<int> GuardarCategoria(Categoria cate)
        {
            if (cate.IdCategoria != 0)//SE CAMBIO POR DIFERENTE DE CERO
                return db.UpdateAsync(cate);

            else
            {
                return db.InsertAsync(cate);
            }

        }

        public Task<int> EliminarCategoriaAsync(Categoria cate)
        {

            return db.DeleteAsync(cate);
        }

        public Task<List<Categoria>> VerCategoriasAsync()
        {
            return db.Table<Categoria>().ToListAsync();
        }

        public Task<Categoria> VerCategoriaIdAsync(int idCate)//Recupera X id
        {
            return db.Table<Categoria>().Where(i => i.IdCategoria == idCate).FirstOrDefaultAsync();
        }

        //ESTATUS 
        public Task<int> CrearEstatus(Estatus est)
        {
            if (est.IdEstatus != 0)//SE CAMBIO POR DIFERENTE DE CERO
                return db.UpdateAsync(est);

            else
            {
                return db.InsertAsync(est);
            }

        }

        public Task<int> EliminarEstatusAsync(Estatus est)
        {

            return db.DeleteAsync(est);
        }

        public Task<List<Estatus>> VerEstatusTodosAsync()
        {
            return db.Table<Estatus>().ToListAsync();
        }

        public Task<Estatus> VerEstatusAsync(int idest)//Recupera X id
        {
            return db.Table<Estatus>().Where(i => i.IdEstatus == idest).FirstOrDefaultAsync();
        }


        /////////////////////////////
        ///ROL
        ///

        public Task<int> CrearRol(Rol r)
        {
            if (r.IdRol != 0)//SE CAMBIO POR DIFERENTE DE CERO
                return db.UpdateAsync(r);

            else
            {
                return db.InsertAsync(r);
            }

        }

        public Task<int> BorrarRolAsync(Rol r)
        {

            return db.DeleteAsync(r);
        }

        public Task<List<Rol>> VerTodosRolesAsync()
        {
            return db.Table<Rol>().ToListAsync();
        }

        public Task<Rol> VerRolAsync(int idr)//Recupera X id
        {
            return db.Table<Rol>().Where(i => i.IdRol == idr).FirstOrDefaultAsync();
        }


        public Task<int> Comprar(Compras c)
        {
            if (c.idCompra != 0)//SE CAMBIO POR DIFERENTE DE CERO
                return db.UpdateAsync(c);

            else
            {
                return db.InsertAsync(c);
            }

        }

        public Task<int> EliminarCompra(Compras c)
        {

            return db.DeleteAsync(c);
        }

        public Task<List<Compras>> VerTodasComprasAsync()
        {
            return db.Table<Compras>().ToListAsync();
        }

        public Task<Compras> VerCompraAsync(int c)//Recupera X id
        {
            return db.Table<Compras>().Where(i => i.idCompra == c).FirstOrDefaultAsync();
        }


    }
}
