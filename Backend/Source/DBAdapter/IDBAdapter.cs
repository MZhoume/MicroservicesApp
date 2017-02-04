namespace DBAdapter
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Exposes an DBAdapter, which supports some simple operations for Database
    /// </summary>
    public interface IDBAdapter
    {
        /// <summary>
        /// Insert an entry into Database
        /// </summary>
        /// <typeparam name="T"> Type for the object to insert </typeparam>
        /// <param name="obj"> Object to insert </param>
        void Insert<T>(T obj)
        where T : class;

        /// <summary>
        /// Delete an entry from Database
        /// </summary>
        /// <typeparam name="T"> Type for the object to delete </typeparam>
        /// <param name="obj"> Object to delete </param>
        void Delete<T>(T obj)
        where T : class;

        /// <summary>
        /// Update an entey in Database
        /// </summary>
        /// <typeparam name="T"> Type for the object to update </typeparam>
        /// <param name="obj"> Object to update </param>
        void Update<T>(T obj)
        where T : class;

        /// <summary>
        /// Get an entry from Database
        /// </summary>
        /// <typeparam name="T"> Type for the object to get </typeparam>
        /// <param name="key"> Primary key for table </param>
        /// <returns> Object retrived from Database </returns>
        T Get<T>(int key)
        where T : class;

        /// <summary>
        /// Get a batch of entries from Database
        /// </summary>
        /// <typeparam name="T"> Type for the objects to get </typeparam>
        /// <param name="predicate"> Predicate for query from Database </param>
        /// <returns> Objects retrived from Database </returns>
        IEnumerable<T> GetBatch<T>(Expression<Func<T, bool>> predicate)
        where T : class;
    }
}