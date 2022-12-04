using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using TurboRentingv2.Api.Interfaces.Services;
using TurboRentingv2.Api.Models.EntitiesDtos;
using TurboRentingv2.Requests.Clients;

namespace TurboRentingv2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;
        private readonly IMapper mapper;

        public ClientController(IClientService clientService, IMapper mapper)
        {
            this.clientService = clientService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ICollection<ClientDto>?> Get()
        {
            var clients = clientService.Get();

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public ActionResult<ClientDto> GetClientById([FromRoute] int id)
        {
            var client = clientService.GetById(id);

            return Ok(client);
        }

        [HttpPost]
        public ActionResult<ClientDto> Post([FromBody] CreateClientRequest request)
        {
            var client = mapper.Map<ClientDto>(request);

            var addedClient = clientService.Add(client);

            return CreatedAtAction(nameof(Get), addedClient);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateClientRequest request)
        {
            var client = mapper.Map<ClientDto>(request);

            client.Id = id;

            clientService.Update(client);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            clientService.Delete(id);

            return NoContent();
        }
    }
}
