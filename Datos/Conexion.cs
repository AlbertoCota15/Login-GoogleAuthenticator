using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Conexion
    {
        SqlConnection con1 = new SqlConnection(@"Data Source=DESKTOP-87E399H\SICOES;Initial Catalog=LoginSDLI;Integrated Security=True");

        public Tuple<string, string> ConsultaUsuario(string Usuario, string contrasena)
        {
            con1.Open();
            string resultado11 = "NULL";
            string resultado22 = "NULL";

            string Query = "Select * From [LoginSDLI].[dbo].[AspNetUsers] where UserName = '" + @Usuario + "'" +
                    "and PasswordHash = '" + contrasena + "'";
            SqlCommand cmd = new SqlCommand(Query, con1);
            SqlDataReader registro = cmd.ExecuteReader();

            if (registro.Read())
            {
                resultado11 = registro["UserName"].ToString();

                resultado22 = registro["Rol"].ToString();
            }

            con1.Close(); ;
            return Tuple.Create(resultado11, resultado22);

        }
    }

    
}
