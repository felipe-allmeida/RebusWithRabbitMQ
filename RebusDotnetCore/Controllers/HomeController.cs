using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orchestrator.Commands;
using Rebus.Bus;

namespace RebusDotnetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IBus _bus;

        public HomeController(IBus bus)
        {
            this._bus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _bus.Send(new StartSagaCommand { AggregateId = Guid.NewGuid() });
            
            return Ok();
        }
    }
}
