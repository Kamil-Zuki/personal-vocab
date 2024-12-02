using Microsoft.AspNetCore.Mvc;
using personal_vocab.DTOs.Requests;
using personal_vocab.DTOs.Responses;
using personal_vocab.Interfeces;

namespace personal_vocab.Controllers;

//[Authorize]
[ApiController]
[Route("api/v1/group")]
public class GroupController(IGroupSevice groupSevice) : ControllerBase
{
    private readonly IGroupSevice _groupSevice = groupSevice;

    [HttpPost]
    public async Task<ActionResult<GroupDto>> CreateAsync(CreateGroupDto model)
    {
        var result = await _groupSevice.CreateAsync(model);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GroupDto>> GetByIdAsync(Guid id)
    {
        var result = await _groupSevice.GetByIdAsync(id);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GroupDto>>> GetAllAsync()
    {
        var result = await _groupSevice.GetAllAsync();

        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<GroupDto>> UpdateAsync(Guid id, CreateGroupDto model)
    {
        var result = await _groupSevice.UpdateAsync(id, model);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var isDeleted = await _groupSevice.DeleteAsync(id);

        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}