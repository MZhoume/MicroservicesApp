namespace DBAdapter
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Dapper;
    using Dommel;
    using MySql.Data.MySqlClient;

    /// <summary>
    /// IDBAdapter's implementation for MySql Database
    /// </summary>
    public class MySqlAdapter : IDBAdapter
    {
        private MySqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlAdapter"/> class.
        /// </summary>
        /// <param name="connString"> Database Connection String </param>
        public MySqlAdapter(string connString)
        {
            this.connection = new MySqlConnection(connString);
        }

        private MySqlAdapter()
        {
        }

        /// <summary>
        /// Delete an entry from Database
        /// </summary>
        /// <typeparam name="T"> Type for the object to delete </typeparam>
        /// <param name="obj"> Object to delete </param>
        public void Delete<T>(T obj)
        where T : class
        {
            this.connection.Open();
            this.connection.Delete<T>(obj);
            this.connection.Close();
        }

        /// <summary>
        /// Get an entry from Database
        /// </summary>
        /// <typeparam name="T"> Type for the object to get </typeparam>
        /// <param name="key"> Primary key for table </param>
        /// <returns> Object retrived from Database </returns>
        public T Get<T>(int key)
        where T : class
        {
            this.connection.Open();
            var res = this.connection.Get<T>(key);
            this.connection.Close();
            return res;
        }

        /// <summary>
        /// Get a batch of entries from Database
        /// </summary>
        /// <typeparam name="T"> Type for the objects to get </typeparam>
        /// <param name="predicate"> Predicate for query from Database </param>
        /// <returns> Objects retrived from Database </returns>
        public IEnumerable<T> GetBatch<T>(Expression<Func<T, bool>> predicate)
        where T : class
        {
            this.connection.Open();
            var res = this.connection.Select<T>(predicate);
            this.connection.Close();
            return res;
        }

        /// <summary>
        /// Insert an entry into Database
        /// </summary>
        /// <typeparam name="T"> Type for the object to insert </typeparam>
        /// <param name="obj"> Object to insert </param>
        public void Insert<T>(T obj)
        where T : class
        {
            this.connection.Open();
            this.connection.Insert<T>(obj);
            this.connection.Close();
        }

        /// <summary>
        /// Update an entey in Database
        /// </summary>
        /// <typeparam name="T"> Type for the object to update </typeparam>
        /// <param name="obj"> Object to update </param>
        public void Update<T>(T obj)
        where T : class
        {
            this.connection.Open();
            this.connection.Update<T>(obj);
            this.connection.Close();
        }
    }
}