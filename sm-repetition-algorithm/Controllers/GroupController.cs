using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using sm_repetition_algorithm.BLL.Interfeces;
using sm_repetition_algorithm.DTOs;
using sm_repetition_algorithm.Interfeces;
using System.Text.RegularExpressions;

namespace sm_repetition_algorithm.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/group")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupSevice _groupSevice;
        public GroupController(IGroupSevice groupSevice) 
        {
            _groupSevice = groupSevice;
        }

        [HttpPost("v1")]
        public async Task<ActionResult> CreateAsync(NoIdGroupDTO groupDTO)
        {
            try
            {
                await _groupSevice.CreateAsync(groupDTO);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet("v1")]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                var groups = await _groupSevice.GetAsync();
                return Ok(groups);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet("v1/{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            try
            {
                var group = await _groupSevice.GetAsync(id);
                return Ok(group);
            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("v1")]
        public async Task<ActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<GroupDTO> patchDoc)
        {
            try
            {
                await _groupSevice.PatchAsync(id, patchDoc);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("v1")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _groupSevice.DeleteAsync(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
