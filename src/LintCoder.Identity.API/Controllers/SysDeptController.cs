using LintCoder.Identity.API.Application.Commands.Dept.CreateDept;
using LintCoder.Identity.API.Application.Commands.Dept.DeleteDept;
using LintCoder.Identity.API.Application.Commands.Dept.UpdateDept;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LintCoder.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysDeptController : ControllerBase
    {
        private readonly ISender _sender;
        public SysDeptController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] string id)
        {
            var result = await _sender.Send(new DeleteDeptCommand { Id = id });
            return Ok(result);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDeptCommand"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] UpdateDeptCommand updateDeptCommand)
        {
            var result = await _sender.Send(updateDeptCommand);
            return Ok(result);
        }

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="updateDeptCommand"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateDeptCommand updateDeptCommand)
        {
            var result = await _sender.Send(updateDeptCommand);
            return Ok(result);
        }
    }
}
