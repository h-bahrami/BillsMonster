using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BillsMonster.WebApi2.Controllers
{
    public class BaseController : Controller
    {
        private IMediator mediatR;
        protected IMediator Mediator => mediatR ?? (mediatR = HttpContext.RequestServices.GetService<IMediator>());
    }
}
