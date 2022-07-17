using MongoDB.Bson;

namespace LintCoder.Shared.MongoDB
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
