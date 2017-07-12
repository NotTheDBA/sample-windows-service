using System;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace SampleService
{
    public class DataFactory
    {
        //TODO:  Write wrapper methods for any stored procedure calls
        public void UpdateEvent(int Parameter1, String Parameter2)
        {
            SqlCommand cmd = new SqlCommand("Stored_Procedure_Name");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Parameter1", Parameter1);
            cmd.Parameters.AddWithValue("@Parameter2", Parameter2);

            using (Common.SQL sql = new Common.SQL())
            {
                sql.ExecuteSqlProcedure("Database_Name", cmd);
            }

        }

        /// <summary>
        /// Example method for getting 1 or more events to process (production only)
        /// </summary>
        /// <returns></returns>
        public DataSet GetEvents()
        {
            SqlCommand cmd = new SqlCommand("Stored_Procedure_Name");
            cmd.CommandType = CommandType.StoredProcedure;
            using (Common.SQL sql = new Common.SQL())
            {
                DataSet ds = sql.GetSqlDataSet("Database_Name", cmd);

                return ds;
            }

        }

        /// <summary>
        /// Example method for getting 1 or more events to test
        /// </summary>
        /// <returns></returns>
        public DataSet GetTestEvents()
        {
            SqlCommand cmd = new SqlCommand("Stored_Procedure_Name");
            cmd.CommandType = CommandType.StoredProcedure;
            using (Common.SQL sql = new Common.SQL())
            {
                DataSet ds = sql.GetSqlDataSet("Database_Name", cmd);

                return ds;
            }

        }

        /// <summary>
        /// Example method for retrieving specific data
        /// </summary>
        /// <param name="Parameter1"></param>
        /// <returns></returns>
        public DataSet GetCustomerData(int Parameter1)
        {
            SqlCommand cmd = new SqlCommand("Stored_Procedure_Name");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Parameter1", Parameter1);
            using (Common.SQL sql = new Common.SQL())
            {
                DataSet ds = sql.GetSqlDataSet("Database_Name", cmd);
                return ds;
            }
        }

    }
}
