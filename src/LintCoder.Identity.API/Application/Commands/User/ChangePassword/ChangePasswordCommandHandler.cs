using Common.Utility.Utils;
using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.Infrastructure;
using LintCoder.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LintCoder.Identity.API.Application.Commands.User.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, MsgModel>
    {
        private readonly IdentityDbContext dbContext;
        private readonly UserContext userContext;

        public ChangePasswordCommandHandler(IdentityDbContext dbContext, UserContext userContext)
        {
            this.dbContext = dbContext;
            this.userContext = userContext;
        }

        public async Task<MsgModel> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await dbContext.SysUser.FirstOrDefaultAsync(x => x.Id == userContext.Id);
            if (currentUser == null)
            {
                return MsgModel.Fail("用户不存在！");
            }
            var encodeOldPassword = PasswordEncoder.Encode(request.OldPassword);
            var encodeNewPassword = PasswordEncoder.Encode(request.NewPassword);
            if(currentUser.Password != encodeOldPassword)
            {
                return MsgModel.Fail("密码不正确！");
            }
            if(encodeOldPassword == encodeNewPassword)
            {
                return MsgModel.Fail("新密码和旧密码不能相同！");
            }
            currentUser.Password = encodeNewPassword;
            dbContext.Update(currentUser);
            var result = await dbContext.SaveChangesAsync();
            if(result > 0)
            {
                return MsgModel.Success("密码修改成功！");
            }
            else
            {
                return MsgModel.Fail("密码修改失败！");
            }
        }
    }
}
