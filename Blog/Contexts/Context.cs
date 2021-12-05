using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Context
{
    class Context
    {
        private string connectionString;

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = @"Server=localhost,1433;Database=Blog;User ID = sa; Password=1q2w3e4r@#$"; }
        }
    }
}
