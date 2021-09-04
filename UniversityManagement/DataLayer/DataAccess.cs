using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversityManagement.DataLayer
{
    public class DataAccess
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hadi\Desktop\UniversityManagement-master\UniversityManagement\UniversityManagement.mdf;Integrated Security=True";                     
       // string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\UniversityManagement.mdf;Integrated Security=True";
        //string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UniversityManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
       // string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UniversityManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public object GetSingleItem(string sql, SqlParameter[] prams = null)
        {
            object obj = null;
            var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(sql, conn);
                if (prams != null) cmd.Parameters.AddRange(prams);
                obj = cmd.ExecuteScalar();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }

            return obj;
        }
        public int InsertDeleteUpdate(string sql, SqlParameter[] prams = null)
        {
            int rows;
            var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(sql, conn);
                if (prams != null) cmd.Parameters.AddRange(prams); 
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }
        public DataTable GetMultipleItem(string sql, SqlParameter[] prams = null)
        {
            DataTable rows = new DataTable();
            var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(sql, conn);
                if (prams != null) cmd.Parameters.AddRange(prams);
                var da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(rows);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }
    }
}
