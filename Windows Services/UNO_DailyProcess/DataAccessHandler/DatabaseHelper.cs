using System;
using System.Data;
using System.Data.Common;

namespace DataAccessHandler
{
    public class DatabaseHelper
    {
        #region Properties

        public ProviderManager ProviderManager { get; set; }

        public string ConnectionString { get; set; }

        #endregion

        #region Constructors

        public DatabaseHelper()
        {
            ConnectionString = ConfigurationSettings.ConnectionString;
            ProviderManager = new ProviderManager();
        }

        public DatabaseHelper(string connectionName)
        {
            ConnectionString = ConfigurationSettings.GetConnectionString(connectionName);
            ProviderManager = new ProviderManager(ConfigurationSettings.GetProviderName(connectionName));
        }

        public DatabaseHelper(string connectionString, string providerName)
        {
            ConnectionString = connectionString;
            ProviderManager = new ProviderManager(providerName);
        }

        #endregion

        #region Database objects

        /// <summary>
        /// Creates connection to database.
        /// </summary>
        /// <returns>Connection object.</returns>
        public IDbConnection GetConnection()
        {
            try
            {
                var connection = ProviderManager.Factory.CreateConnection();
                connection.ConnectionString = ConnectionString;
                connection.Open();
                
                return connection;
            }
            catch (Exception)
            {
                throw new Exception("Error occured while creating connection. Please check connection string and provider name.");
            }
        }

        public void CloseConnection(IDbConnection connection)
        {
            connection.Close();
        }

        /// <summary>
        /// Creates command object.
        /// </summary>
        /// <param name="commandText">Command text.</param>
        /// <param name="connection">Connection.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns></returns>
        public IDbCommand GetCommand(string commandText, IDbConnection connection, CommandType commandType)
        {
            try
            {
                IDbCommand command = ProviderManager.Factory.CreateCommand();
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
        public DbDataAdapter GetDataAdapter(IDbCommand command)
        {
            DbDataAdapter adapter = ProviderManager.Factory.CreateDataAdapter();
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
        public DbParameter GetParameter(string name, object value, DbType dbType)
        {
            try
            {
                DbParameter dbParam = ProviderManager.Factory.CreateParameter();
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
        public DbParameter GetParameter(string name, object value, DbType dbType, ParameterDirection parameterDirection)
        {
            try
            {
                DbParameter dbParam = ProviderManager.Factory.CreateParameter();
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
        public DbParameter GetParameter(string name, object value, DbType dbType, int size, ParameterDirection parameterDirection)
        {
            try
            {
                DbParameter dbParam = ProviderManager.Factory.CreateParameter();
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
