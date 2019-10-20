using Archiving.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Archiving.DAL.EF
{
    public class EFContext : DbContext
    {
        private string _connectionString;

        public DbSet<Account> Accounts { get; set; }

        public EFContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
