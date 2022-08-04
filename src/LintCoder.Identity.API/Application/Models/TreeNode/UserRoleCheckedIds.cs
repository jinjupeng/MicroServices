using System.Collections.Generic;

namespace ApiServer.Models.Model.Nodes
{
    public class UserRoleCheckedIds
    {
        public string UserId { get; set; }

        public List<string> CheckedIds { get; set; }
    }
}
