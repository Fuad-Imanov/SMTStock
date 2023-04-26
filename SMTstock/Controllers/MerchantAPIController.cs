using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMTstock.Entities.DTO.MerchantDto;
using SMTstock.Services.Interfaces;

namespace SMTstock.Controllers
{
    [Route("api/MerchantAPI")]
    [ApiController]
    public class MerchantAPIController : ControllerBase
    {
        private readonly IMerchantService _MerchantService;

        public MerchantAPIController(IMerchantService MerchantService)
        {
            _MerchantService = MerchantService;
        }


        [HttpGet("GetAllMerchantsWithPaginations/{page:int}")]
        public async Task<IActionResult> GetWithPaginationsAsync(int page)
        {
            var result = await _MerchantService.GetAllMerchantsWithPaginationsAsync(page);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _MerchantService.GetAllMerchantsAsync();
            return Ok(result);
        }

        [HttpGet("GetMerchantById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var Merchant = await _MerchantService.GetMerchantByIdAsync(id);
            if (Merchant == null)
            {
                return NotFound();
            }

            return Ok(Merchant);
        }


        [HttpPost("AddMerchant")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddMerchantAsync(AddMerchantDTO addMerchantDTO)
        {


            if (ModelState.IsValid)
            {

                var result = await _MerchantService.AddMerchantAsync(addMerchantDTO);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Data.Id }, result);
            }

            return BadRequest(ModelState);


        }

        [HttpPut("UpdateMerchant/{id:int}")]
        public async Task<IActionResult> UpdateMerchantAsync(int id, [FromBody] MerchantDTO MerchantDTO)
        {
            if (MerchantDTO == null || id != MerchantDTO.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _MerchantService.UpdateMerchant(id, MerchantDTO);
                    return Ok(result);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_MerchantService.GetMerchantByIdAsync(id) == null)
                    {
                        return NotFound();
                    }

                    throw;
                }

                return NoContent();
            }

            return BadRequest(ModelState);
        }



        [HttpDelete("DeleteMerchant/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _MerchantService.RemoveMerchant(id);

            return Ok(result);
        }

    }
}
