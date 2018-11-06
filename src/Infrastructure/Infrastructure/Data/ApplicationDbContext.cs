using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace WebMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        protected readonly ApplicationUser userService;
        protected readonly DbContextOptions options;
        private readonly IHttpContextAccessor _contextAccessor;
        //protected readonly ITimeService timeService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            _contextAccessor = contextAccessor;
        }

        public DbSet<Customer> Customers { get; set; }
        //public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasPostgresExtension("uuid-ossp");
            
            ConvertToSnakeCase.FixSnakeCaseNames(builder);
        }


        public override int SaveChanges()
        {
            AddAuditInfo();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AddAuditInfo();
            return await base.SaveChangesAsync(true, cancellationToken);
        }

        private void AddAuditInfo()
        {
            // get entries that are being Added or Updated
            var modifiedEntries = ChangeTracker.Entries()
                    .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

            // Get the authenticated user name 
            string userName = string.Empty;

            var user = ClaimsPrincipal.Current;
            if (user != null)
            {
                var identity = user.Identity;
                if (identity != null)
                {
                    userName = identity.Name;
                }
            }
            //var identityName = ApplicationUser.IdentityUser,
            Guid identityName = new Guid(userName); // GetString(*Guid *);

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as BaseEntity;

                if (entry.State == EntityState.Added)
                {
                    entity.created_by = identityName;
                    entity.created_on = DateTime.Now;
                }

                entity.updated_by = identityName;
                entity.updated_on = DateTime.Now;
            }
        }
    }
}
