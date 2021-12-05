using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contexts
{
    public class Context
    {
        const string _connectionString = @"Server=localhost,1433;Database=Blog;User ID = sa; Password=1q2w3e4r@#$";
        public Context()
        {
            ConnectionString = _connectionString;
        }
        
        public string ConnectionString { get; set; }
        
    }
}
