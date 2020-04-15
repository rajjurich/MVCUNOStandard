
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UNO.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        //        private static readonly IDbConnection _sessionFactory;

        private readonly IDbConnection _connection;
        public IDbTransaction _transaction = null;
        public ProviderManager ProviderManager { get; set; }
        public static string ConnectionString { get; set; }

        static UnitOfWork()
        {
            try
            {
                ConnectionString = ConfigurationSettings.GetConnectionString("DefaultConnection");

            }
            catch (Exception)
            {
                throw new Exception("Error occured while creating connection. Please check connection string and provider name.");
            }

        }

        public IDbConnection Connection
        {
            get
            {
                return _connection;
            }


        }

        //public string ProviderName
        //{
        //    get
        //    {
        //        return ProviderManager.ProviderName;
        //    }


        //}

        public UnitOfWork()
        {
            try
            {
                ProviderManager = new ProviderManager(ConfigurationSettings.GetProviderName("DefaultConnection"));

                _connection = ProviderManager.Factory.CreateConnection();
                _connection.ConnectionString = ConnectionString;
                _connection.Open();
            }
            catch (Exception)
            {
                throw new Exception("Error occured while creating connection. Please check connection string and provider name.");
            }

        }

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
            //connection.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            try
            {
                if (_transaction != null)
                    _transaction.Commit();
            }
            catch
            {
                if (_transaction != null)
                    _transaction.Rollback();

                throw;
            }
            finally
            {
                ProviderManager = null;
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();

            }
        }

        public void Rollback()
        {
            try
            {
                if (_transaction != null)
                    _transaction.Rollback();
            }
            finally
            {
                ProviderManager = null;
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();

            }
        }

        public void Dispose()
        {
            ProviderManager = null;
            if (_connection.State == ConnectionState.Open)
                _connection.Close();

        }
    }
}
