using Microsoft.EntityFrameworkCore;

namespace Szfindel.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AccountUser> AccountUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<UserHobby> UserHobby { get; set;}
        public DbSet<Match> Matches { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                        .HasOne(m => m.Sender)
                        .WithMany(u => u.SentMessages)
                        .HasForeignKey(m => m.SenderId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                        .HasOne(m => m.Receiver)
                        .WithMany()  
                        .HasForeignKey(m => m.ReceiverId)
                        .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<AccountUser>()
                .HasMany(m => m.SentMessages)
                .WithOne(u => u.Sender);
            modelBuilder.Entity<AccountUser>()
                .HasMany(m => m.ReceivedMessages)
                .WithOne(u => u.Receiver);


            modelBuilder.Entity<AccountUser>()
                .HasOne(a => a.User)
                .WithOne(u => u.AccountUser)
                .HasForeignKey<User>(u => u.AccountUserId);

            modelBuilder.Entity<UserHobby>()
                .HasKey(a => new { a.UserId, a.HobbyId });

            modelBuilder.Entity<UserHobby>()
                .HasOne(a => a.User)
                .WithMany(b => b.UserHobbies)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<UserHobby>()
                .HasOne(a => a.Hobby)
                .WithMany(b => b.UserHobbies)
                .HasForeignKey(a => a.HobbyId);

            modelBuilder.Entity<AccountUser>()
                .HasMany(m => m.Matches)
                .WithOne(u => u.AccountUser);


            modelBuilder.Entity<Hobby>().HasData(
                new Hobby { HobbyId = 1, HobbyName = "Koszykówka",},
                new Hobby { HobbyId = 2, HobbyName = "Netflix & Hill", },
                new Hobby { HobbyId = 3, HobbyName = "Programming", },
                new Hobby { HobbyId = 4, HobbyName = "ONS", },
                new Hobby { HobbyId = 5, HobbyName = "FWB", },
                new Hobby { HobbyId = 6, HobbyName = "Muzyka", },
                new Hobby { HobbyId = 7, HobbyName = "Gotowanie", },
                new Hobby { HobbyId = 8, HobbyName = "Wędkowanie", }
                );

        }

    }
}
        

        
