using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

using System.Data;

namespace Experience_Based_Cyber_SA
{
    public class Util_DBA
    {


        public static DataTable dbFilterTable(string sql)
        {
            string connString = Util_ConfigData.getDBString();

            DataTable results = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(results);
            }
            return results;
        }

    }
}
