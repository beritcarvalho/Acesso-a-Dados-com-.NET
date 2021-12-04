using Blog.Models;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Blog.Repositories
{
    class RoleRepository
    {
        private SqlConnection _connection;
        public RoleRepository(SqlConnection connection)
            => _connection = connection;

        public void Create(Role role)
        {
            role.Id = 0;
            _connection.Insert<Role>(role);
        }

        public IEnumerable<Role> Read()
            => _connection.GetAll<Role>();

        public Role Read(int id)
            => _connection.Get<Role>(id);

        public void Update(Role role)
        {
            if (role.Id != 0)
                _connection.Update<Role>(role);
        }

        public void Delete(Role role)
        {
            if (role.Id != 0)
                _connection.Delete<Role>(role);
        }

        public void Delete(int id)
        {
            if (id != 0)
                return;

            var role = _connection.Get<Role>(id);
            _connection.Delete<Role>(role);
        }

    }
}
