using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_RepositoryPattern.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress{ get; set; }
        public string SalesOfficeName { get; set; }
        public string UserRole { get; set; }
        public bool IsLoggedIn { get; set; }

    }
}
