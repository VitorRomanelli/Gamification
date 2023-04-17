using Gamification.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.Infra.Context
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().Property(e => e.Id).ValueGeneratedOnAdd();
        }

        public virtual DbSet<StandardUser> StandardUsers => Set<StandardUser>();
        public virtual DbSet<SupervisorUser> SupervisorUsers => Set<SupervisorUser>();
        public virtual DbSet<AdministratorUser> AdministratorUsers => Set<AdministratorUser>();
        public virtual DbSet<UserConquest> UserConquests => Set<UserConquest>();
        public virtual DbSet<SectorConquest> SectorConquests => Set<SectorConquest>();
        public virtual DbSet<Conquest> Conquests => Set<Conquest>();
        public virtual DbSet<Sector> Sectors => Set<Sector>();
        public virtual DbSet<Order> Orders => Set<Order>();
    }
}
