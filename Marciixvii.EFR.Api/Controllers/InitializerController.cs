using Microsoft.AspNetCore.Mvc;

namespace Marciixvii.EFR.App.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class InitializerController : ControllerBase {

        [HttpGet]
        public string Get() => "Copyright © 2020 Kelasys esr";
    }
}
