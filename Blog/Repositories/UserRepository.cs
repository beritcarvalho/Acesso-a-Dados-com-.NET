using Blog.Models;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Blog.Repositories
{
    class UserRepository
    {
        private SqlConnection _connection;
        public UserRepository(SqlConnection connection)
            => _connection = connection;

        public void Create(User user)
        {
            user.Id = 0;
            _connection.Insert<User>(user);
        }

        public IEnumerable<User> Read()
            => _connection.GetAll<User>();

        public User Read(int id)
            => _connection.Get<User>(id);
        
        public void Update(User user)
        {
            if (user.Id != 0)
                _connection.Update<User>(user);
        }

        public void Delete(User user)
        {
            if (user.Id != 0)
                _connection.Delete<User>(user);
        }

        public void Delete(int id)
        {
            if (id == 0)
                return;

            var user = _connection.Get<User>(id);
            _connection.Delete<User>(user);            
        }

    }
}
