using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class BaseDAL
    {
        IConfiguration _configuration;

        public BaseDAL(IConfiguration config)
        {
            this._configuration = config;
        }

        private string ConnectionString
        {
            get
            {
                return Convert.ToString(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        private SqlConnection mConnection;

        private SqlConnection Connection
        {
            get
            {
                if (mConnection == null)
                    mConnection = GetConnection(ConnectionString);
                return mConnection;
            }

        }

        private SqlConnection GetConnection(string connStr)
        {
            return new SqlConnection(connStr);
        }

        private void CloseConnection(SqlConnection conn)
        {
            if (conn.State != System.Data.ConnectionState.Closed)
                conn.Close();
        }

        private void OpenConnection(SqlConnection conn)
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
        }

        public int ExecuteNonQuery(SqlCommand cmd)
        {
            return Execute<int>(cmd, delegate ()
            {
                return cmd.ExecuteNonQuery();
            });
        }

        public object ExecuteScalar(SqlCommand cmd)
        {
            return Execute<object>(cmd, delegate ()
            {
                return cmd.ExecuteScalar();
            });
        }

        public void CopyToServer(DataTable table)
        {
            SqlBulkCopy copy = new SqlBulkCopy(this.Connection);
            copy.DestinationTableName = table.TableName;

            try
            {
                OpenConnection(this.Connection);
                using (copy)
                {
                    copy.WriteToServer(table);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConnection(this.Connection);
            }
        }

        public void ExecuteDataReader(SqlCommand cmd, Action<SqlDataReader> action)
        {
            if (cmd.Connection != null)
                throw new ArgumentException("Command must have null Connection property");

            cmd.Connection = GetConnection(this.ConnectionString);
            try
            {
                OpenConnection(cmd.Connection);
                using (cmd)
                {
                    action(cmd.ExecuteReader(CommandBehavior.CloseConnection));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConnection(cmd.Connection);
                cmd.Connection.Dispose();
                cmd.Dispose();
            }
        }

        private T Execute<T>(SqlCommand cmd, Func<T> execute)
        {
            try
            {
                OpenConnection(Connection);
                using (cmd)
                {
                    return execute();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConnection(Connection);
            }
        }

        public SqlCommand NewCommand(string sqlText, CommandType type)
        {
            SqlCommand cmd = new SqlCommand(sqlText, Connection);
            cmd.CommandType = type;
            return cmd;
        }

        public SqlParameter GetParam(string name, SqlDbType type, object value)
        {
            SqlParameter param = new SqlParameter(name, value);
            param.SqlDbType = type;

            return param;
        }

        public SqlParameter GetParamOut(string name, SqlDbType type)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Direction = ParameterDirection.Output;

            return param;
        }

        public T GetCastValue<T>(object obj)
        {
            if (obj is DBNull)
                return default(T);

            return (T)obj;
        }

        public Guid? GetNulableGuid(object obj)
        {
            if (obj is DBNull)
                return null;

            return (Guid)obj;
        }

        public string GetString(object obj)
        {
            if (obj is DBNull)
                return string.Empty;

            return Convert.ToString(obj);
        }

        public DataRow GetExistingRowOrNew(DataTable table, Guid id)
        {
            DataRow[] rows = table.Select(string.Format("Id = '{0}'", id));
            if (rows.Length > 0) return rows[0];

            DataRow row = table.NewRow();
            table.Rows.Add(row);
            return row;
        }
    }
}
