using Microsoft.AspNetCore.Mvc;
using log4net;
namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CandidateController : ControllerBase
{
    private readonly ILog _log;

    public CandidateController()
    {
        _log = LogManager.GetLogger(typeof(CandidateController));
    }
}
