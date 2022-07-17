

namespace LintCoder.Shared.MongoDB.Test.Models
{
    [BsonCollection("User")]
    public class User : Document
    {
        public string Name { get; set; }

        public string Email { get;set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
