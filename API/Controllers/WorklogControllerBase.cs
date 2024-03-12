using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class WorklogControllerBase : ControllerBase
    {
        public IMediator Mediator { get; private set; }

        public WorklogControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}