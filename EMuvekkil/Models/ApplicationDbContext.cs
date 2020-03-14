using EMuvekkil.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMuvekkil.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EventUsers>().HasKey(n => new
            {
                n.EventId,
                n.UserId
            });

            builder.Entity<EventUsers>().HasOne<Event>(v => v.Event).WithMany(v => v.EventUsers).HasForeignKey(v => v.EventId);
            builder.Entity<EventUsers>().HasOne<ApplicationUser>(v => v.User).WithMany(v => v.EventUsers).HasForeignKey(v => v.UserId);


        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Masraf> Masrafs { get; set; }
        public DbSet<Dava> Davas { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<DavaState> DavaStates { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventUsers> EventUsers { get; set; }
        public DbSet<Notification> Notifications { get; set; }

    }


}
