using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using personal_vocab.BLL.Interfeces;

namespace personal_vocab.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/repetition")]
public class RepetitionController : ControllerBase
{
    private readonly IFlashCardService _superMemoAlgorithm;




}
