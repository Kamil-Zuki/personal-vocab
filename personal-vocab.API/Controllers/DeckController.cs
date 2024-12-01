using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using personal_vocab.Interfeces;

namespace personal_vocab.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/deck")]
public class DeckController : ControllerBase
{
    private readonly IDeckService _deckService;


}
