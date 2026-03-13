using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Collections.Generic;
using System.Data.SqlClient;

namespace UnitPro_DBDataDriven
{
    public class DBDataSource
    {
        public static List<(string Username, String Password)> GetDBData()
        {
            List<(string, string)> _list = new List<(string, string)>();
            string connstring= "Data Source=LAKSHMINARAYANA;Initial Catalog=DataDrivenSets;Integrated Security=True";
            using(SqlConnection con=new SqlConnection(connstring))
            {
                con.Open();
                string query= "SELECT Username,Password FROM dbo.Login_details";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    _list.Add((((reader["Username"].ToString())),
                              (reader["Password"].ToString())));
                }
            }
            return _list;
        }
    }
}
