using System;
using System.Data;
using System.Data.SqlClient;

namespace Common
{    /// <summary>
     /// The SQL class manages connections and calls to SQL database procedures.   Other than the connection string, no code should be changed in this class. 
     /// </summary>
    class SQL : IDisposable
    {
        // Development
        //TODO:  Set the data string
        const String connString = "server=[SQL Alias];timeout=90;app=[SampleFeederService];database=[Database Name];uid=[SQL Login];pwd=[SQL Password]";

        /// <summary>
        /// The currrent version of the code uses a hard-coded connection string, but we pass the name of the database because it is based on more dynamic code,
        /// and the code may be updated to be more dynamic again.  The dynamic version would allow us to connect to more than one database in the solution.
        /// </summary>
        /// <param name="DatabaseName"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataSet GetSqlDataSet(String DatabaseName, SqlCommand cmd)
        {

            try
            {
                DataSet ds = new DataSet();
                //string parmValues = "";

                using (SqlConnection sqlConn = new SqlConnection(connString))
                {
                    cmd.Connection = sqlConn;

                    try
                    {
                        sqlConn.Open();
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw ex;
                    }

                    cmd = PrepParameters(cmd);

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        ds = new DataSet();
                        dataAdapter.Fill(ds, "results");
                    }

                    try
                    {
                        sqlConn.Close();
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw ex;
                    }

                }

                return ds;

            }

            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                throw;
            }

        }

        /// <summary>
        /// The currrent version of the code uses a hard-coded connection string, but we pass the name of the database because it is based on more dynamic code,
        /// and the code may be updated to be more dynamic again.  The dynamic version would allow us to connect to more than one database in the solution.
        /// </summary>
        /// <param name="DatabaseName"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public Boolean ExecuteSqlProcedure(String DatabaseName, SqlCommand cmd)
        {

            try
            {
                if (cmd.CommandType != CommandType.StoredProcedure)
                {
                    throw new NotSupportedException(@"[SqlCommand.CommandType not supported.]");
                }

                using (SqlConnection sqlConn = new SqlConnection(connString))
                {
                    cmd.Connection = sqlConn;
                    try
                    {
                        sqlConn.Open();
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw ex;
                    }

                    cmd = PrepParameters(cmd);

                    cmd.ExecuteNonQuery();

                    try
                    {
                        sqlConn.Close();
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw ex;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        private SqlCommand PrepParameters(SqlCommand cmd)
        {

            try
            {
                cmd.Prepare();
                for (int x = 0; x < cmd.Parameters.Count; x++) // last parm is a return code
                {
                    if (cmd.Parameters[x].SqlDbType == SqlDbType.VarChar
                    || cmd.Parameters[x].SqlDbType == SqlDbType.Char
                    || cmd.Parameters[x].SqlDbType == SqlDbType.Xml
                    || cmd.Parameters[x].SqlDbType == SqlDbType.NVarChar
                    || cmd.Parameters[x].SqlDbType == SqlDbType.NChar
                    || cmd.Parameters[x].SqlDbType == SqlDbType.Text
                    || cmd.Parameters[x].SqlDbType == SqlDbType.NText
                        )
                    {
                        if (cmd.Parameters[x].Value != null && cmd.Parameters[x].Value != DBNull.Value)
                        {
                            if (cmd.Parameters[x].IsNullable && IsEmptyString(cmd.Parameters[x].ToString().Trim()))
                            {
                                cmd.Parameters[x].Value = null;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return cmd;

        }

        /// <summary>
        /// This is a utility function.  It returns a true if the string is empty.
        /// </summary>
        /// <param name="nullableString">The nullable string.</param>
        /// <returns></returns>
        public static Boolean IsEmptyString(String nullableString)
        {
            if (nullableString.Trim() == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

}
