namespace LintCoder.Identity.API.Application.Models.Request
{
    public class UpdateUserRequest
    {
        public string UserName { get; set; }

        public string NickName { get; set; }

        public string Portrait { get; set; }

        public long OrgId { get; set; }

        public bool Enabled { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int Sex { get; set; }
    }
}
