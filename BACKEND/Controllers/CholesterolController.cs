using BACKEND.DTOs.Calculators.Cholesterol;
using BACKEND.Extensions;
using BACKEND.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND.Controllers
{
    public class CholesterolController : BaseApiController
    {
        private readonly IUnitOfWork _uow;

        public CholesterolController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost("calculateCol")]
        public async Task<ActionResult<CholesterolDto>> CalculateCholesterolAsync([FromBody] CholesterolRequest request)
        {
            var userId = User.GetUserId();

            int hdl = request.Hdl;
            int ldl = request.Ldl;
            int triglycerides = request.Triglycerides;

            try
            {
                var colDto = await _uow.CholesterolRepository.CalculateCholesterolAsync(hdl, ldl, triglycerides, userId);

                return Ok(colDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getUserCholesterolResults")]
        public async Task<ActionResult<List<CholesterolDto>>> GetUserCholesterolResults()
        {
            var userId = User.GetUserId();

            try
            {
                var cholesterolResults = await _uow.CholesterolRepository.GetCholesterolResultsForUserAsync(userId);

                return Ok(cholesterolResults);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("cholesterolCategoryStats")]
        public async Task<ActionResult<CholesterolCategoryStatsDto>> GetCholesterolCategoryStatsAsync()
        {
            try
            {
                var categoryStats = await _uow.CholesterolRepository.GetCholesterolCategoryStatsAsync();
                return Ok(categoryStats);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}