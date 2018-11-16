namespace Chushka.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    public abstract class BaseController : Controller
    {
        protected readonly IMapper mapper;

        protected BaseController(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}
