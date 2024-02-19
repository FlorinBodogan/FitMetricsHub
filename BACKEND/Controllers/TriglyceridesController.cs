using BACKEND.DTOs.Calculators.Triglycerides;
using BACKEND.Extensions;
using BACKEND.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND.Controllers
{
    public class TriglyceridesController : BaseApiController
    {
        private readonly IUnitOfWork _uow;

        public TriglyceridesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost("calculateTri")]
        public async Task<ActionResult<TriglyceridesDto>> CalculateCholesterolAsync([FromBody] TriglyceridesRequest request)
        {
            var userId = User.GetUserId();

            int hdl = request.Hdl;
            int ldl = request.Ldl;
            int cholesterol = request.Cholesterol;

            try
            {
                var triDto = await _uow.TriglyceridesRepository.CalculateCholesterolAsync(hdl, ldl, cholesterol, userId);

                return Ok(triDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getUserTriglyceridesResults")]
        public async Task<ActionResult<List<TriglyceridesDto>>> GetUserTriglyceridesResults()
        {
            var userId = User.GetUserId();

            try
            {
                var triglyceridesResults = await _uow.TriglyceridesRepository.GetTriglyceridesResultsForUserAsync(userId);

                return Ok(triglyceridesResults);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("triglyceridesCategoryStats")]
        public async Task<ActionResult<TriglyceridesCategoryStatsDto>> GetTriglyceridesCategoryStatsAsync()
        {
            try
            {
                var triglyceridesCategoryStats = await _uow.TriglyceridesRepository.GetTriglyceridesCategoryStatsAsync();
                return Ok(triglyceridesCategoryStats);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("deleteTriglycerides")]
        public async Task<ActionResult> DeleteUserBMIs()
        {
            var userId = User.GetUserId();

            try
            {
                await _uow.TriglyceridesRepository.DeleteUserTriglycerides(userId);

                return Ok("Triglycerides history cleaned.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}