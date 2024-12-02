using Microsoft.AspNetCore.Mvc;
using personal_vocab.DTOs.Requests;
using personal_vocab.DTOs.Responses;
using personal_vocab.Interfeces;

namespace personal_vocab.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/deck")]
public class DeckController(IDeckService DeckSevice) : ControllerBase
{
    private readonly IDeckService _deckSevice = DeckSevice;

    [HttpPost]
    public async Task<ActionResult<DeckDto>> CreateAsync(CreateDeckDto model)
    {
        var result = await _deckSevice.CreateAsync(model);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DeckDto>> GetByIdAsync(Guid id)
    {
        var result = await _deckSevice.GetByIdAsync(id);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeckDto>>> GetAllAsync()
    {
        var result = await _deckSevice.GetAllAsync();

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DeckDto>> UpdateAsync(Guid id, CreateDeckDto model)
    {
        var result = await _deckSevice.UpdateAsync(id, model);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeckDto>> DeleteAsync(Guid id)
    {
        var isDeleted = await _deckSevice.DeleteAsync(id);

        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
