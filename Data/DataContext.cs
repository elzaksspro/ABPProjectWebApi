using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Models;

namespace ProjectApi.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, 
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext>  options) : base (options) {}


        public DbSet<AccessLevel> AccessLevels { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<IDType> IDTypes { get; set; }
        public DbSet<MaritalStatus> MaritalStatus { get; set; }
        public DbSet<Nationality> Nationality { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Lga> Lgas { get; set; }
        public DbSet<FarmerVarificationStatus> FarmerVarifcationstatus { get; set; }
        public DbSet<FarmOwnershipType> FarmOwnershipTypes { get; set; }
        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<EOPType> EOPTypes { get; set; }
        public DbSet<EOPUnit> EOPUnits { get; set; }
        public DbSet<EOP> EOPs { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Farmer> Farmers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<UserRole>(userRole => 
            {
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                    
            });



          
           

           
            
           

        }


    }

}