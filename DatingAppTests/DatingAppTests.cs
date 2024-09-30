using NUnit.Framework;
using Szfindel.Models;
using Szfindel.Repo;
using Moq;
using Microsoft.EntityFrameworkCore;



namespace DatingAppTests
{
    [TestFixture]
    public class DatingAppTests
    {
        [Test]
        public void CreateUser_WithValidUser_ShouldAddToDbContext()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Projekt1")
                .Options;
            var mockDbContext = new Mock<DatabaseContext>(options);
            var userRepository = new UserRepo(mockDbContext.Object);

            var user = new User
            {
                UserId = 1,
                Username = "testuser",
                Password = "testpassword",
                AccountUserId = 2
            };
            

            // Act
            userRepository.CreateUser(user);

            // Assert
            mockDbContext.Verify(m => m.Users.Add(user), Times.Once);
            mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
           
        }
    }
}
