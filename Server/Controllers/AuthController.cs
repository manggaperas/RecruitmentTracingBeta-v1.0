using Microsoft.AspNetCore.Mvc;
using log4net;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILog _log;

    public AuthController()
    {
        _log = LogManager.GetLogger(typeof(AuthController));
    }

    // public async Task Login()
    // {

    // }
}
