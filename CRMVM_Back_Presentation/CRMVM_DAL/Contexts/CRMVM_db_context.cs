using CRMVM_DAL.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ovile_DAL_Layer.Contexts
{
    /// <summary>
    /// Контекст для бд
    /// </summary>
    public class CRMVM_db_context : IdentityDbContext<IdentityUser>
    {
        public DbSet<Deal> Deals { get; set; }

        public DbSet<Client> Clients { get; set; }


        public CRMVM_db_context(DbContextOptions<CRMVM_db_context> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder) => base.OnModelCreating(builder);
    }
}