using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Entities
{
    public class UserDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"server=(localdb)\mssqllocaldb;
                Database=UserAccountDb;" + "Trusted_Connection=True");
        }
    }
}
