using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    internal class CryptoCurrencyContext : DbContext
    {
        public CryptoCurrencyContext(DbContextOptions<CryptoCurrencyContext> options) 
            : base(options)
        {

        }


        public DbSet<CryptoCurrency> CryptoCurrencies { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CryptoCurrency>().HasKey(x => x.Id);
            modelBuilder.Entity<CryptoCurrency>().Ignore(x => x.CurrencyQuotes);


            base.OnModelCreating(modelBuilder);
        }
    }
}
