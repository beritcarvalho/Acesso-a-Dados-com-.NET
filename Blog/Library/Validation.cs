using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Library
{
    class Validation
    {
        public bool Validate(User user, string password)
        {
            if (user.PasswordHash == password)
                return true;
            else
                return false;
        }
    }
}
