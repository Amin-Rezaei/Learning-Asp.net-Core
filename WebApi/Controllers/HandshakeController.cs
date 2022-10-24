using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    public class HandshakeController : ControllerBase
    {
        [Route("api/Handshake/hi")]
        public string hello()
        {
            return "salam";
        }

        [Route("api/Handshake/bye")]
        public string goodby()
        {
            return "khodafez";
        }
    }
}
