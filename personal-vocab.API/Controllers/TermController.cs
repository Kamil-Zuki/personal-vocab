using Microsoft.AspNetCore.Mvc;
using personal_vocab.DTOs.Requests;
using personal_vocab.DTOs.Responses;
using personal_vocab.Interfeces;

namespace personal_vocab.Controllers;

//[Authorize]
[ApiController]
[Route("api/v1/term")]
public class TermController(ITermService TermSevice) : ControllerBase
{
    private readonly ITermService _TermSevice = TermSevice;

    [HttpPost]
    public async Task<ActionResult<TermDto>> CreateAsync(CreateTermDto model)
    {
        var result = await _TermSevice.CreateAsync(model);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TermDto>> GetByIdAsync(Guid id)
    {
        var result = await _TermSevice.GetByIdAsync(id);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TermDto>>> GetAllAsync()
    {
        var result = await _TermSevice.GetAllAsync();

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TermDto>> UpdateAsync(Guid id, CreateTermDto model)
    {
        var result = await _TermSevice.UpdateAsync(id, model);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<TermDto>> DeleteAsync(Guid id)
    {
        var isDeleted = await _TermSevice.DeleteAsync(id);

        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
