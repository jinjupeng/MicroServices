using LintCoder.Shared.MongoDB;

namespace LintCoder.Shared.Auditing.WriteToMonogDB
{
    public class AuditLogMongoDBHandler : IAuditingProvider<AuditLogMongoDBInfo>
    {
        private readonly IMongoRepository<AuditLogMongoDBInfo> mongoRepository;

        public AuditLogMongoDBHandler(IMongoRepository<AuditLogMongoDBInfo> mongoRepository)
        {
            this.mongoRepository = mongoRepository;
        }

        public void AddAuditLog(AuditLogMongoDBInfo auditLog)
        {
            mongoRepository.InsertOne(auditLog);
        }

        public async Task AddAuditLogAsync(AuditLogMongoDBInfo auditLog)
        {
            await mongoRepository.InsertOneAsync(auditLog);
        }
    }
}
