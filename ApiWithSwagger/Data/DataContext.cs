using ApiWithSwagger.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithSwagger.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected DataContext()
        {
        }

        public DbSet<User> AppUser { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Deposit> Deposits { get; set; }

    }
}
