using LintCoder.Shared.MongoDB;
using MongoDB.Bson;

namespace LintCoder.Shared.Auditing.WriteToMonogDB
{
    public class AuditLogMongoDBInfo : AuditLogInfo, IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
