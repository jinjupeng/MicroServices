using LintCoder.Shared.MongoDB.Test.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace LintCoder.Shared.MongoDB.Test
{
    public class MongoDBContextTests
    {
        private Mock<IOptions<MongoOptions>> _mockOptions;
        private Mock<IMongoDatabase> _mockDB;
        private Mock<IMongoClient> _mockClient;

        public MongoDBContextTests()
        {
            _mockOptions = new Mock<IOptions<MongoOptions>>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
        }

        [Fact]
        public void MongoBookDBContext_Constructor_Success()
        {
            var settings = new MongoOptions()
            {
                ConnectionString = "mongodb://tes123 ",
                DatabaseName = "TestDB"
            };

            _mockOptions.Setup(s => s.Value).Returns(settings);
            _mockClient.Setup(c => c
            .GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
                .Returns(_mockDB.Object);

            //Act 
            var context = new MongoContext(_mockOptions.Object);

            //Assert 
            Assert.NotNull(context);
        }

        [Fact]
        public void MongoBookDBContext_GetCollection_NameEmpty_Failure()
        {
            //Arrange
            var settings = new MongoOptions()
            {
                ConnectionString = "mongodb://tes123",
                DatabaseName = "TestDB"
            };

            _mockOptions.Setup(s => s.Value).Returns(settings);
            _mockClient.Setup(c => c
            .GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
                .Returns(_mockDB.Object);

            //Act 
            var context = new MongoContext(_mockOptions.Object);
            var myCollection = context.GetCollection<User>("");

            //Assert 
            Assert.Null(myCollection);

        }

        [Fact]
        public void MongoBookDBContext_GetCollection_ValidName_Success()
        {
            //Arrange
            var settings = new MongoOptions()
            {
                ConnectionString = "mongodb://tes123 ",
                DatabaseName = "TestDB"
            };

            _mockOptions.Setup(s => s.Value).Returns(settings);
            _mockClient.Setup(c => c.GetDatabase(_mockOptions.Object.Value.DatabaseName, null)).Returns(_mockDB.Object);

            //Act 
            var context = new MongoContext(_mockOptions.Object);
            var myCollection = context.GetCollection<User>("User");

            //Assert 
            Assert.NotNull(myCollection);
        }
    }
}
