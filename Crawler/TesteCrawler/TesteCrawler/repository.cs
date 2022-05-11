using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteCrawler
{
    public class repository
    {

        public static SqlConnection AbrirConn()
        {
            string connetionString = "Data Source=MATNOT;Initial Catalog=MaggaUtil;User ID=pad;Password=TestandoCrawler";
            SqlConnection cnn;
            return cnn = new SqlConnection(connetionString);
        }

        public static void FecharConn(SqlConnection cnn)
        {
            cnn.Close();
        }
    }
}
