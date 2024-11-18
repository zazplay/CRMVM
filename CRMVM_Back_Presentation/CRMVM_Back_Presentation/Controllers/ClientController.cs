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
    public class ClientController : ControllerBase
    {

        private readonly IClientService _categoryService;
        private readonly IMapper _mapper;

        public ClientController(IClientService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all clients
        /// </summary>
        /// <returns></returns>
        [HttpGet("/getAllClients")]
        public async Task <ActionResult<IEnumerable<ClientResponse>>> Get()
        {
            try
            {
                var clientsDTO = await _categoryService.GetAllClients();
                var clientsResponses = _mapper.Map<List<ClientResponse>>(clientsDTO);
                return(Ok(clientsResponses));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Get deal by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/getClientById")]
        public async Task<ActionResult<ClientResponse>> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var GuidId)) {
                    return BadRequest("Incorrect id");
                }

                var clientDTO = await _categoryService.GetClientById(GuidId);
                var clientResponse = _mapper.Map<ClientResponse>(clientDTO);
                return (Ok(clientResponse));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create new client
        /// </summary>
        /// <param name="request">Client data</param>
        [HttpPost("/createClient")]
        public async Task<ActionResult<ClientResponse>> CreateClient([FromBody] CreateClientRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var clientDTO = _mapper.Map<ClientDTO>(request);
                var createdClient = await _categoryService.CreateClient(clientDTO);
                var response = _mapper.Map<ClientResponse>(createdClient);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update existing client
        /// </summary>
        /// <param name="id">Client ID</param>
        /// <param name="request">Updated client data</param>
        [HttpPut("/updateClient/{OldId}")]
        public async Task<ActionResult<ClientResponse>> UpdateClient(string OldId, [FromBody] CreateClientRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(OldId) || !Guid.TryParse(OldId, out var guidId))
                    return BadRequest("Invalid ID format");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var clientDTO = _mapper.Map<ClientDTO>(request);
                clientDTO.Id = guidId;

                var updatedClient = await _categoryService.UpdateClient(clientDTO);
                var response = _mapper.Map<ClientResponse>(updatedClient);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete client by ID
        /// </summary>
        /// <param name="id">Client ID</param>
        [HttpDelete("/deleteClient/{id}")]
        public async Task<IActionResult> DeleteClient(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var guidId))
                    return BadRequest("Invalid ID format");

                var deletedClientDTO = await _categoryService.DeleteClient(guidId);
                var deletedClientResponse = _mapper.Map<ClientResponse>(deletedClientDTO);

                return Ok(deletedClientResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
