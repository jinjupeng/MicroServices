using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;

namespace LintCoder.Shared.Auditing.WriteToElastic
{
    public class AuditLogElasticHandler<T> : IAuditingProvider<T> where T : class
    {
        private const string _alias = "auditlog";
        private string _indexName = $"{_alias}-{DateTime.UtcNow.ToString("yyyy-MM-dd")}";
        private static Field TimestampField = new Field("timestamp");
        private readonly IOptions<AuditLogElasticOptions> _options;

        private ElasticClient _elasticClient { get; }

        public AuditLogElasticHandler(
           IOptions<AuditLogElasticOptions> auditLogElasticOptions)
        {
            _options = auditLogElasticOptions ?? throw new ArgumentNullException(nameof(auditLogElasticOptions));

            if (_options.Value.IndexPerMonth)
            {
                _indexName = $"{_alias}-{DateTime.UtcNow.ToString("yyyy-MM")}";
            }

            var pool = new StaticConnectionPool(auditLogElasticOptions.Value.Urls);

            var connectionSettings = new ConnectionSettings(pool)
                    .DefaultMappingFor<T>(m => m
                    .IndexName(_indexName));


            //new HttpConnection(),
            //new SerializerFactory((jsonSettings, nestSettings) => jsonSettings.Converters.Add(new StringEnumConverter())))
            //.DisableDirectStreaming();

            _elasticClient = new ElasticClient(connectionSettings);
        }

        public virtual void AddAuditLog(T auditLog)
        {
            var indexRequest = new IndexRequest<T>(auditLog);

            var response = _elasticClient.Index(indexRequest);
            if (!response.IsValid)
            {
                throw new ElasticsearchClientException("Add auditlog disaster!");
            }
        }

        public virtual async Task AddAuditLogAsync(T auditLog)
        {
            var indexRequest = new IndexRequest<T>(auditLog);

            var response = await _elasticClient.IndexAsync(indexRequest);
            if (!response.IsValid)
            {
                throw new ElasticsearchClientException("Add auditlog disaster!");
            }
        }

    }
}
