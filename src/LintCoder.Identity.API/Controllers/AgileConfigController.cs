using AgileConfig.Client;
using Microsoft.AspNetCore.Mvc;

namespace LintCoder.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgileConfigController : ControllerBase
    {
        private readonly IConfigClient _configClient;

        public AgileConfigController(IConfigClient configClient)
        {
            _configClient = configClient;
        }

        /// <summary>
        /// 使用IConfigClient读取配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ByIConfigClient()
        {
            var userId = _configClient["userId"];
            var dbConn = _configClient["db:connection"];

            foreach (var item in _configClient.Data)
            {
                Console.WriteLine($"{item.Key} = {item.Value}");
            }

            var result = $"userId:{userId},dbConn:{dbConn}";

            return Ok(result);
        }
    }
}
