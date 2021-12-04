using Blog.Models;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repositories
{
    class UserRepository
    {
        private SqlConnection _connection;
        public UserRepository(SqlConnection connection)
            => _connection = connection;   
        
        public IEnumerable<User> Get()
            => _connection.GetAll<User>();                 
    }
}
