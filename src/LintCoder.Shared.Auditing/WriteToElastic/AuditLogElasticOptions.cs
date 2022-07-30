

namespace LintCoder.Shared.Auditing.WriteToElastic
{
    public class AuditLogElasticOptions
    {
        public bool IndexPerMonth { get; set; }

        public List<Uri> Urls { get; set; }
    }
}
