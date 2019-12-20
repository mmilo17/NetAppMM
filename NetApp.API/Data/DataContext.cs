using Microsoft.EntityFrameworkCore;
using NetApp.API.Models;

namespace NetApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        public DbSet<Value> Values {get; set;}
    	public DbSet<User> Users {get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Request>().HasKey(k => new {k.SenderId, k.ReceiverId});

            builder.Entity<Request>()
                .HasOne(u => u.Receiver)
                .WithMany(u => u.Senders)
                .HasForeignKey(u => u.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Request>()
                .HasOne(u => u.Sender)
                .WithMany(u => u.Recivers)
                .HasForeignKey(u => u.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}