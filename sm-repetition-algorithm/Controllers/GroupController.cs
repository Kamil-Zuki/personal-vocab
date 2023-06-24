using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sm_repetition_algorithm.BLL.Interfeces;
using sm_repetition_algorithm.DTOs;
using sm_repetition_algorithm.Interfeces;
using System.Text.RegularExpressions;

namespace sm_repetition_algorithm.Controllers
{
    [Authorize]
    [ApiController]
    [Route("group")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupSevice _groupSevice;
        public GroupController(IGroupSevice groupSevice) 
        {
            _groupSevice = groupSevice;
        }

        [HttpPost("v1/")]
        public async Task<ActionResult> CreateAsync(GroupWithoutIdDTO groupDTO)
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
        public async Task<ActionResult> GetAllAsync()
        {
            try
            {
                var groups = await _groupSevice.GetAllAsync();
                return Ok(groups);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet("v1/{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            try
            {
                var group = await _groupSevice.GetAllAsync();
                return Ok(group);
            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("v1")]
        public async Task<ActionResult> UpdateAsync(GroupDTO group)
        {
            try
            {
                await _groupSevice.UpdateAsync(group);
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
