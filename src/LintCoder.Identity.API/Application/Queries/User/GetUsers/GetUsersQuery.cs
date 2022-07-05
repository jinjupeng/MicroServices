﻿using LintCoder.Identity.API.Application.Models.Request;
using LintCoder.Identity.API.Application.Models.Response;
using MediatR;

namespace LintCoder.Identity.API.Application.Queries.User.GetUsers
{
    public class GetUsersQuery : QueryModel, IRequest<MsgModel> 
    {
        public string Phone { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public long? OrgId { get; set; }

        public bool? Enabled { get; set; }

        public DateTime? CreateStartTime { get; set; }

        public DateTime? CreateEndTime { get; set; }
    }
}
