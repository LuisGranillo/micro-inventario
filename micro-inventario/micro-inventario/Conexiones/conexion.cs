using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
namespace micro_inventario.Conexiones
{
    internal class conexion
{
        public static FirebaseClient firebase = new FirebaseClient("https://appcompras-de920-default-rtdb.firebaseio.com/");
}
}
