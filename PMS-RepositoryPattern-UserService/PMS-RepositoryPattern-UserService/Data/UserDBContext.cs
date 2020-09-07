using Microsoft.EntityFrameworkCore;
using PMS_RepositoryPattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_RepositoryPattern.Data
{
    public class ProductDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }       

        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {
        }
    }
}
