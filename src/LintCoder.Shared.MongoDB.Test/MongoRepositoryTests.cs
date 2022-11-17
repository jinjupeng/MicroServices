using LintCoder.Shared.MongoDB.Test.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;

namespace LintCoder.Shared.MongoDB.Test
{
    public class MongoRepositoryTests
    {
        private Mock<MongoContext> _mockContext;
        private Mock<IMongoCollection<User>> _mockCollection;
        private Mock<IMongoRepository<User>> _mockUserRepo;
        private User _user;

        public MongoRepositoryTests()
        {
            var id = Guid.NewGuid().ToString("N");
            var objectId = new ObjectId(id);
            _user = new User
            {
                Id = objectId,
                Name = "test",
                Email = "test@test.com",
                FirstName = "test1",
                LastName = "test2 "
            };
            _mockContext = new Mock<MongoContext>();
            _mockCollection = new Mock<IMongoCollection<User>>();
            _mockUserRepo = new Mock<IMongoRepository<User>>();
        }


    }
}
