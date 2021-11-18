using Microsoft.EntityFrameworkCore;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    public partial class ShoelessdevContext : DbContext
    {
        public ShoelessdevContext()
        {
        }

        public ShoelessdevContext(DbContextOptions<ShoelessdevContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<ShoeImage> ShoeImages { get; set; }
        public DbSet<Manufacter> Manufacters { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SecretConfig.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shoe>()
                .HasOne(a => a.ShoeImage)
                .WithOne(b => b.Shoe)
                .HasForeignKey<ShoeImage>(b => b.ShoeId);

            //Friends
            modelBuilder.Entity<Friend>().HasKey(c => new { c.SenderId, c.RecieverId });

            modelBuilder.Entity<Friend>()
                .HasOne(b => b.Reciever)
                .WithMany(c => c.RecieverFriends)
                .HasForeignKey(b => b.RecieverId);

            modelBuilder.Entity<Friend>()
                .HasOne(b => b.Sender)
                .WithMany(c => c.SenderFriends)
                .HasForeignKey(b => b.SenderId);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
