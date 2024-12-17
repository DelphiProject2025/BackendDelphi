using delphibackend.IAM.Domain.Model.Aggregates;
using delphibackend.User.Domain.Model.Entities;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

     
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            builder.EnableSensitiveDataLogging();
            builder.AddCreatedUpdatedInterceptor();
            //builder.UseSnakeCaseNamingConvention();

            base.OnConfiguring(builder);
        }
       
        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<Host> Host { get; set; }
        public DbSet<Participant> Participant { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // IAM Bounded Context
            builder.Entity<AuthUser>().HasKey(u => u.Id);
            builder.Entity<AuthUser>().Property(u => u.Id).IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<AuthUser>().Property(u => u.Email).IsRequired();
            builder.Entity<AuthUser>().Property(u => u.PasswordHash).IsRequired();

            // USER Bounded Context
            builder.Entity<Host>().HasKey(h => h.Id);
            builder.Entity<Host>().Property(h => h.Id).IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<Host>()
                .HasOne(h => h.AuthUser)
                .WithOne(a => a.Host)
                .HasForeignKey<Host>(h => h.AuthUserId)
                .HasPrincipalKey<IAM.Domain.Model.Aggregates.AuthUser>(a => a.Id);
            
            builder.Entity<Participant>().HasKey(p => p.Id);
            builder.Entity<Participant>().Property(p => p.Id).IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<Participant>()
                .HasOne(p => p.AuthUser)
                .WithOne(a => a.Participant)
                .HasForeignKey<Participant>(p => p.AuthUserId)
                .HasPrincipalKey<IAM.Domain.Model.Aggregates.AuthUser>(a => a.Id);
            
    }
        
        
    }
}
