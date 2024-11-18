using AutoMapper;
using CRMVM_Back_Presentation.Models.Requests;
using CRMVM_Back_Presentation.Models.Responses;
using CRMVM_BLL.DTO;
using CRMVM_BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMVM_Back_Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class DealController : ControllerBase
    {
        private readonly IDealService _dealService;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public DealController(IDealService dealService, IClientService clientService, IMapper mapper)
        {
            _dealService = dealService;
            _clientService = clientService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all deals
        /// </summary>
        [HttpGet("/getAllDeals")]
        public async Task<IActionResult> GetAllDeals()
        {
            try
            {
                var dealsDTO = await _dealService.GetAllDeals();
                var dealsResponse = _mapper.Map<List<DealResponse>>(dealsDTO);
                return Ok(dealsResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get deal by ID
        /// </summary>
        [HttpGet("/getDealById")]
        public async Task<IActionResult> GetDealById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var guidId))
                    return BadRequest("Invalid ID format");

                var dealDTO = await _dealService.GetDealById(guidId);

                if (dealDTO == null)
                    return NotFound($"Deal with ID {id} not found");

                var dealResponse = _mapper.Map<DealResponse>(dealDTO);
                return Ok(dealResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create new deal
        /// </summary>
        [HttpPost("/createDeal")]
        public async Task<IActionResult> CreateDeal([FromBody] CreateDealRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (request.ClientId.HasValue)
                {
                    var client = await _clientService.GetClientById(request.ClientId.Value);
                    if (client == null)
                    {
                        var newClientDTO = new ClientDTO
                        {
                            Id = request.ClientId.Value,
                            Name = "Client",
                            Email = "",
                            Login = "newclient" + Guid.NewGuid().ToString()
                        };
                        await _clientService.CreateClient(newClientDTO);
                    }
                }

                var dealDTO = _mapper.Map<DealDTO>(request);
                var createdDeal = await _dealService.CreateDeal(dealDTO);
                var response = _mapper.Map<DealResponse>(createdDeal);

                return CreatedAtAction(nameof(GetDealById), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update deal
        /// </summary>
        [HttpPut("/updateDeal/{id}")]
        public async Task<IActionResult> UpdateDeal(string id, [FromBody] UpdateDealRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var guidId))
                    return BadRequest("Invalid ID format");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingDeal = await _dealService.GetDealById(guidId);
                if (existingDeal == null)
                    return NotFound($"Deal with ID {id} not found");

                var dealDTO = _mapper.Map<DealDTO>(request);
                dealDTO.Id = guidId;
                dealDTO.ClientId = existingDeal.ClientId;

                var updatedDeal = await _dealService.UpdateDeal(dealDTO);
                var response = _mapper.Map<DealResponse>(updatedDeal);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update deal status
        /// </summary>
        [HttpPut("/updateDealStatus")]
        public async Task<IActionResult> UpdateDealStatus([FromBody] UpdateDealStatusReq request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updatedDeal = await _dealService.UpdateDealStatus(request.Id, request.Status);
                var response = _mapper.Map<DealResponse>(updatedDeal);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete deal
        /// </summary>
        [HttpDelete("/deleteDeal/{id}")]
        public async Task<IActionResult> DeleteDeal(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var guidId))
                    return BadRequest("Invalid ID format");

                await _dealService.DeleteDeal(guidId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}