using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Read me Linl// 
/// https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/obtaining-a-dbproviderfactory
/// 
namespace UNO.DAL
{
    public class DatabaseHelper
    {
        private UnitOfWork _unitOfWork;
        public DatabaseHelper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        public IDbDataParameter CreateParameter(string name, object value, DbType dbType)
        {
            return GetParameter(name, value, dbType, ParameterDirection.Input);
        }

        public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType)
        {
            return GetParameter(name, value, dbType, size, ParameterDirection.Input);
        }

        public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection direction)
        {
            return GetParameter(name, value, dbType, size, direction);
        }


        public DataTable GetDataTable(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
        {
            //using (var connection = database.GetConnection())
            //{
            using (var command = GetCommand(commandText, _unitOfWork.Connection, commandType))
            {
                command.Transaction = _unitOfWork._transaction;
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                var dataset = new DataSet();
                var dataAdaper = GetDataAdapter(command);
                dataAdaper.Fill(dataset);

                return dataset.Tables[0];
            }
            ///   }
        }

        public DataSet GetDataSet(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
        {
            //using (var connection = database.GetConnection())
            //{
            using (var command = GetCommand(commandText, _unitOfWork.Connection, commandType))
            {
                command.Transaction = _unitOfWork._transaction;
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                var dataset = new DataSet();
                var dataAdaper = GetDataAdapter(command);
                dataAdaper.Fill(dataset);

                return dataset;
                
            }
            //}
        }

        public IDataReader GetDataReader(string commandText, CommandType commandType, IDbDataParameter[] parameters, out IDbConnection connection)
        {
            IDataReader reader = null;
            connection = null;            //database.GetConnection();

            var command = GetCommand(commandText, _unitOfWork.Connection, commandType);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            reader = command.ExecuteReader();

            return reader;
        }


        //         public object GetScalarValue(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
        public async Task<dynamic> GetScalarValue(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
        {
            //using (var connection = database.GetConnection())
            //{
            using (var command = GetCommand(commandText, _unitOfWork.Connection, commandType))
            {
                command.Transaction = _unitOfWork._transaction;
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                var x = await Task.Run(() => command.ExecuteScalar());
                return x;
            }
            //}
        }

        public int ExecuteScalar(string commandText, CommandType commandType, IDbDataParameter[] parameters, out int lastId)
        {
            lastId = 0;
            //using (var connection = database.GetConnection())
            //{
            using (var command = GetCommand(commandText, _unitOfWork.Connection, commandType))
            {
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                object newId = command.ExecuteScalar();
                lastId = Convert.ToInt32(newId);
            }
            //}

            return lastId;
        }

        public long ExecuteScalar(string commandText, CommandType commandType, IDbDataParameter[] parameters, out long lastId)
        {
            lastId = 0;
            //using (var connection = database.GetConnection())
            //{
            using (var command = GetCommand(commandText, _unitOfWork.Connection, commandType))
            {
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                object newId = command.ExecuteScalar();
                lastId = Convert.ToInt64(newId);
            }
            //}

            return lastId;
        }


        public async Task<int> Insert(string commandText, CommandType commandType,string ipaddress , int activeuser, IDbDataParameter[] parameters = null )
        {
            string commandtexttrim = commandText.Replace("'", string.Empty);
            commandText = commandText + "  ;; insert into UNOMVCLOGSHISTORY values('" + commandtexttrim + "','" + ipaddress + "'," + activeuser + ",'"+DateTime.Now.ToString()+"')  ;;";
            using (var command = GetCommand(commandText, _unitOfWork.Connection, commandType))
            {
                command.Transaction = _unitOfWork._transaction;
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                var id = await Task.Run(() => command.ExecuteScalar());

                return id == null ? 0 : Convert.ToInt32(id);
            }

        }

        #region Database objects
        /// <summary>
        /// Creates command object.
        /// </summary>
        /// <param name="commandText">Command text.</param>
        /// <param name="connection">Connection.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns></returns>
        private IDbCommand GetCommand(string commandText, IDbConnection connection, CommandType commandType)
        {
            try
            {
                IDbCommand command = _unitOfWork.ProviderManager.Factory.CreateCommand();
                command.CommandText = commandText;
                command.Connection = connection;
                command.CommandType = commandType;

                return command;
            }
            catch (Exception)
            {
                throw new Exception("Invalid parameter 'commandText'.");
            }
        }

        /// <summary>
        /// Creates adapter. 
        /// </summary>
        /// <param name="command">Command object.</param>
        /// <returns>Object of DbDataAdapter.</returns>
        private DbDataAdapter GetDataAdapter(IDbCommand command)
        {
            DbDataAdapter adapter = _unitOfWork.ProviderManager.Factory.CreateDataAdapter();
            adapter.SelectCommand = (DbCommand)command;
            adapter.InsertCommand = (DbCommand)command;
            adapter.UpdateCommand = (DbCommand)command;
            adapter.DeleteCommand = (DbCommand)command;
            return adapter;
        }

        /// <summary>
        /// Create input parameter.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">Parameter value.</param>
        /// <param name="dbType">Parameter data type.</param>
        /// <returns>Object of DbParameter.</returns>
        private DbParameter GetParameter(string name, object value, DbType dbType)
        {
            try
            {
                DbParameter dbParam = _unitOfWork.ProviderManager.Factory.CreateParameter();
                dbParam.ParameterName = name;
                dbParam.Value = value;
                dbParam.Direction = ParameterDirection.Input;
                dbParam.DbType = dbType;

                return dbParam;
            }
            catch (Exception)
            {
                throw new Exception("Invalid parameter or type.");
            }
        }

        /// <summary>
        /// Create input parameter.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">Parameter value.</param>
        /// <param name="parameterDirection">Parameter direction.</param>
        /// <param name="dbType">Parameter data type.</param>
        /// <returns>Object of DbParameter.</returns>
        private DbParameter GetParameter(string name, object value, DbType dbType, ParameterDirection parameterDirection)
        {
            try
            {
                DbParameter dbParam = _unitOfWork.ProviderManager.Factory.CreateParameter();
                dbParam.ParameterName = name;
                dbParam.Value = value;
                dbParam.Direction = parameterDirection;
                dbParam.DbType = dbType;

                return dbParam;
            }
            catch (Exception)
            {
                throw new Exception("Invalid parameter or type.");
            }
        }

        /// <summary>
        /// Create input parameter.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">Parameter value.</param>
        /// <param name="parameterDirection">Parameter direction.</param>
        /// <param name="dbType">Parameter data type.</param>
        /// <returns>Object of DbParameter.</returns>
        private DbParameter GetParameter(string name, object value, DbType dbType, int size, ParameterDirection parameterDirection)
        {
            try
            {
                DbParameter dbParam = _unitOfWork.ProviderManager.Factory.CreateParameter();
                dbParam.ParameterName = name;
                dbParam.Value = value;
                dbParam.Size = size;
                dbParam.Direction = parameterDirection;
                dbParam.DbType = dbType;

                return dbParam;
            }
            catch (Exception)
            {
                throw new Exception("Invalid parameter or type.");
            }
        }

        #endregion
    }
}
