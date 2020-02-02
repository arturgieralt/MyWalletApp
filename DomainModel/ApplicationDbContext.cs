using MyWalletApp.DomainModel.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWalletApp.DomainModel
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {

        public DbSet<Account> Accounts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Currency> Currencies { get; set;}


        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(entity => entity.Property(m => m.Id).HasMaxLength(255));

            var transactionTypeConventer = CreateValueConventer<TransactionType>();

            builder.Entity<Transaction>()
                .Property(t => t.TransactionType)
                .HasConversion(transactionTypeConventer);

            builder.Entity<Category>()
                .HasMany(c => c.Transactions)
                .WithOne(t => t.Category)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private ValueConverter CreateValueConventer<T> () {
            return new ValueConverter<T, string>(
                v => v.ToString(),
                v => (T)Enum.Parse(typeof(T), v)
            );
        }
    }
}
