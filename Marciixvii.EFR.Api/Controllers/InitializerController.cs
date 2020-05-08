using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Marciixvii.EFR.App.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class InitializerController : ControllerBase {
        private string Copyright { get; }

        private readonly ILogger<InitializerController> _logger;

        public InitializerController(ILogger<InitializerController> logger) {
            _logger = logger;
            Copyright = "Copyright © 2020 Kelasys esr";
        }

        [HttpGet]
        public string Get() => Copyright;
    }
}
