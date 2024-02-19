using BACKEND.DTOs.Calculators.Rmb;
using BACKEND.Extensions;
using BACKEND.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND.Controllers
{
    public class RmbController : BaseApiController
    {
        private readonly IUnitOfWork _uow;

        public RmbController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpDelete("calculateRmb")]
        public async Task<ActionResult<RmbDto>> CalculateRmbAsync([FromBody] RmbCalculationRequest request)
        {
            var userId = User.GetUserId();

            if (request.Height <= 0 || request.Weight <= 0 || request.Age <= 0)
            {
                return BadRequest("Height and weight and age must be greater than zero.");
            }

            int height = request.Height;
            int weight = request.Weight;
            int age = request.Age;
            string activityLevel = request.ActivityLevel;
            string gender = request.Gender;

            try
            {
                var rmbDto = await _uow.RmbRepository.CalculateRmbAsync(gender, height, weight, age, activityLevel, userId);

                return Ok(rmbDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getUserRmbResults")]
        public async Task<ActionResult<List<RmbDto>>> GetUserRmbResults()
        {
            var userId = User.GetUserId();

            try
            {
                var rmbResults = await _uow.RmbRepository.GetRmbResultsForUserAsync(userId);

                return Ok(rmbResults);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("deleteRMBs")]
        public async Task<ActionResult> DeleteUserBMIs()
        {
            var userId = User.GetUserId();

            try
            {
                await _uow.RmbRepository.DeleteUserRmbs(userId);

                return Ok("RMB history cleaned.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}