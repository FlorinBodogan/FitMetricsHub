using BACKEND.DTOs.Calculators.ArterialTension;
using BACKEND.Extensions;
using BACKEND.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND.Controllers
{
    public class ArterialTensionController : BaseApiController
    {
        private readonly IUnitOfWork _uow;

        public ArterialTensionController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost("calculateAT")]
        public async Task<ActionResult<ArterialTensionDto>> CalculateArterialTensionAsync([FromBody] ArterialTensionRequest request)
        {
            var userId = User.GetUserId();

            int sbp = request.Sbp;
            int dbp = request.Dbp;

            try
            {
                var atDto = await _uow.ArterialTensionRepository.CalculateArterialTension(sbp, dbp, userId);

                return Ok(atDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getUserArterialTensionResults")]
        public async Task<ActionResult<List<ArterialTensionDto>>> GetUserArterialTensionResults()
        {
            var userId = User.GetUserId();

            try
            {
                var arterialTensionResults = await _uow.ArterialTensionRepository.GetArterialTensionResultsForUserAsync(userId);

                return Ok(arterialTensionResults);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("arterialTensionCategoryStats")]
        public async Task<ActionResult<ArterialTensionCategoryStatsDto>> GetArterialTensionCategoryStatsAsync()
        {
            try
            {
                var latestArterialTension = await _uow.ArterialTensionRepository.GetArterialTensionCategoryStatsAsync();

                return Ok(latestArterialTension);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}