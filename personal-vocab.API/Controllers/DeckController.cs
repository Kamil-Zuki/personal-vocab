using Microsoft.AspNetCore.Mvc;
using personal_vocab.DTOs.Requests;
using personal_vocab.DTOs.Responses;
using personal_vocab.Interfeces;

namespace personal_vocab.Controllers;

//[Authorize]
[ApiController]
[Route("api/v1/deck")]
public class DeckController(IDeckService DeckSevice) : ControllerBase
{
    private readonly IDeckService _DeckSevice = DeckSevice;

    [HttpPost]
    public async Task<ActionResult<DeckDto>> CreateAsync(CreateDeckDto model)
    {
        var result = await _DeckSevice.CreateAsync(model);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DeckDto>> GetByIdAsync(Guid id)
    {
        var result = await _DeckSevice.GetByIdAsync(id);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeckDto>>> GetAllAsync()
    {
        var result = await _DeckSevice.GetAllAsync();

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DeckDto>> UpdateAsync(Guid id, CreateDeckDto model)
    {
        var result = await _DeckSevice.UpdateAsync(id, model);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeckDto>> DeleteAsync(Guid id)
    {
        var isDeleted = await _DeckSevice.DeleteAsync(id);

        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
