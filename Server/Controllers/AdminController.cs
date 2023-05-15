using Microsoft.AspNetCore.Mvc;
using log4net;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly ILog _log;

    public AdminController()
    {
        _log = LogManager.GetLogger(typeof(AdminController));
    }
}
