using Microsoft.AspNetCore.Authorization;
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
}
