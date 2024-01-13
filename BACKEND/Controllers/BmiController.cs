using System.Security.Claims;
using BACKEND.DTOs.Calculators;
using BACKEND.DTOs.Calculators.Bmi;
using BACKEND.Extensions;
using BACKEND.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND.Controllers
{
    public class BmiController : BaseApiController
    {
        private readonly IUnitOfWork _uow;

        public BmiController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost("calculateBmi")]
        public async Task<ActionResult<BmiDto>> CalculateBmiAsync([FromBody] BmiCalculationRequest request)
        {
            var userId = User.GetUserId();

            int height = request.Height;
            int weight = request.Weight;

            try
            {
                var bmiDto = await _uow.BmiRepository.CalculateBmiAsync(height, weight, userId);

                return Ok(bmiDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getUserBmiResults")]
        public async Task<ActionResult<List<BmiDto>>> GetUserBmiResults()
        {
            var userId = User.GetUserId();

            try
            {
                var bmiResults = await _uow.BmiRepository.GetBmiResultsForUserAsync(userId);

                return Ok(bmiResults);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("bmiCategoryStats")]
        public async Task<ActionResult<BmiCategoryStatsDto>> GetBmiCategoryStatsAsync()
        {
            try
            {
                var categoryStats = await _uow.BmiRepository.GetBmiCategoryStatsAsync();
                return Ok(categoryStats);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}