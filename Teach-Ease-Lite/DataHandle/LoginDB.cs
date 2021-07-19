using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Teach_Ease_Lite.DataHandle
{
    public class LoginDB
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["myconnection"].ToString();
            con = new SqlConnection(constring);
        }

        public int InsertErrorLog(string ErrorMessage, string FunctionName, string ErrorFrom)
        {
            connection();
            SqlCommand cmd = new SqlCommand("InsertErrorLog", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ErrorMessage", ErrorMessage);
            cmd.Parameters.AddWithValue("@FunctionName", FunctionName);
            cmd.Parameters.AddWithValue("@ErrorFrom", ErrorFrom);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
}