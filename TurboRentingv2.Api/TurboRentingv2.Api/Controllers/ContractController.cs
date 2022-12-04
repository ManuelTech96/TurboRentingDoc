using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TurboRentingv2.Api.Enums;
using TurboRentingv2.Api.Interfaces.Services;
using TurboRentingv2.Api.Models.EntitiesDtos;
using TurboRentingv2.Requests.Contracts;

namespace TurboRentingv2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService contractService;
        private readonly IMapper mapper;

        public ContractController(IContractService contractService, IMapper mapper)
        {
            this.contractService = contractService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ICollection<ContractDto>?> Get()
        {
            var contracts = contractService.Get();

            return Ok(contracts);
        }

        [HttpGet("{id}")]
        public ActionResult<ContractDto> GetContractById([FromRoute] int id)
        {
            var contract = contractService.GetById(id);

            return Ok(contract);
        }

        [HttpGet("clients/{id}")]
        public ActionResult<ICollection<ClientDto>?> GetContractClients([FromRoute] int id)
        {
            var contractClients = contractService.GetContractClients(id);

            if (contractClients == null)
                return NotFound();

            return Ok(contractClients);
        }

        [HttpGet("type/{typeName}")]
        public ActionResult<ICollection<ContractDto>?> GetByType(TypeNames typeName)
        {
            var contracts = contractService.GetByType(typeName);

            return Ok(contracts);
        }

        [HttpPost]
        public ActionResult<ContractDto> Post([FromBody] CreateContractRequest request)
        {
            var contract = mapper.Map<ContractDto>(request);

            var addedContract = contractService.Add(contract);

            return CreatedAtAction(nameof(Get), addedContract);
        }

        [HttpPut("{id}")] 
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateContractRequest request)
        {
            var contract = mapper.Map<ContractDto>(request);

            contract.Id = id;

            contractService.Update(contract);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            contractService.Delete(id);

            return NoContent();
        }
    }
}
