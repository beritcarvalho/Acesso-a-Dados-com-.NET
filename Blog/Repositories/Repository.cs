using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Blog.Repositories
{
    class Repository<T> where T : class
    {
        private readonly SqlConnection _connection;

        public Repository(SqlConnection connection)
            => _connection = connection;

        public void Create(T model)
            =>_connection.Insert<T>(model);

        public IEnumerable<T> Read()
            => _connection.GetAll<T>();
        public T Read(int id)
            => _connection.Get<T>(id);

        public void Update(T model)
            => _connection.Update<T>(model);

        public void Delete(T model)
            => _connection.Delete<T>(model);
        
        public void Delete(int id)
        {
            if (id == 0)
                return;

            var model = _connection.Get<T>(id);
            _connection.Delete<T>(model);
        }
    }
}
