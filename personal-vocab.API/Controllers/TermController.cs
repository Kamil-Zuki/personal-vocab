using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace personal_vocab.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/term")]
public class TermController : ControllerBase
{


}
