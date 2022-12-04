using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TurboRentingv2.Api.Interfaces.Services;
using TurboRentingv2.Api.Models.EntitiesDtos;
using TurboRentingv2.Api.Models.Enums;
using TurboRentingv2.Requests.Garages;
using TurboRentingv2.Requests.Vehicules;

namespace TurboRentingv2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarageController : ControllerBase
    {
        private readonly IGarageService garageService;
        private readonly IMapper mapper;

        public GarageController(IGarageService garageService, IMapper mapper)
        {
            this.garageService = garageService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ICollection<GarageDto>?> Get()
        {
            var garages = garageService.Get();

            return Ok(garages);
        }

        [HttpGet("vehicules")]
        public ActionResult<ICollection<VehiculeDto>?> GetVehicules()
        {
            var vehicules = garageService.GetVehicules();

            return Ok(vehicules);
        }

        [HttpGet("{id}")]
        public ActionResult<GarageDto> GetGarageById([FromRoute] int id)
        {
            var garage = garageService.GetById(id);

            return Ok(garage);
        }

        [HttpGet("vehicules/{id}")]
        public ActionResult<VehiculeDto> GetVehiculeById([FromRoute] int vehiculeId)
        {
            var vehicule = garageService.GetVehiculeById(vehiculeId);
            
            return Ok(vehicule);
        }


        [HttpGet("vehicules/brand/{brand}")]
        public ActionResult<ICollection<VehiculeDto>?> GetByBrand(string brand)
        {
            var vehicules = garageService.GetVehiculesByBrand(brand);

            return Ok(vehicules);
        }

        [HttpGet("vehicules/model/{model}")]
        public ActionResult<ICollection<VehiculeDto>?> GetByModel(string model)
        {
            var vehicules = garageService.GetVehiculesByModel(model);

            return Ok(vehicules);
        }

        [HttpGet("vehicules/doors/{nDoors}")]
        public ActionResult<ICollection<VehiculeDto>?> GetByNDoors(int nDoors)
        {
            var vehicules = garageService.GetVehiculesByNDoors(nDoors);

            return Ok(vehicules);
        }

        [HttpGet("vehicules/cv/{cv}")]
        public ActionResult<ICollection<VehiculeDto>?> GetByCv(int cv)
        {
            var vehicules = garageService.GetVehiculesByCv(cv);

            return Ok(vehicules);
        }

        [HttpGet("vehicules/wheels/{wheelsSize}")]
        public ActionResult<ICollection<VehiculeDto>?> GetByWheelsSize(int wheelsSize)
        {
            var vehicules = garageService.GetVehiculesByWheelsSize(wheelsSize);

            return Ok(vehicules);
        }

        [HttpGet("vehicules/fuel/{fuel}")]
        public ActionResult<ICollection<VehiculeDto>?> GetByFuel(Fuel fuel)
        {
            var vehicules = garageService.GetVehiculesByFuel(fuel);

            return Ok(vehicules);
        }

        [HttpGet("vehicules/{registration}/clients")]
        public ActionResult<ICollection<ClientDto>?> GetVehiculeClients(string registration)
        {
            var vehiculeClients = garageService.GetClientsVehicule(registration);

            if (vehiculeClients == null)
                return NotFound();

            return Ok(vehiculeClients);
        }

        [HttpPost]
        public ActionResult<GarageDto> Post([FromBody] CreateGarageRequest request)
        {
            var garage = mapper.Map<GarageDto>(request);

            var addedGarage = garageService.Add(garage);

            return CreatedAtAction(nameof(Get), addedGarage);
        }

        [HttpPost("{id}/vehicules")]
        public ActionResult<VehiculeDto> Post(int id, [FromBody] CreateVehiculeRequest request)
        {
            var vehicule = mapper.Map<VehiculeDto>(request);

            var addedVehicule = garageService.AddVehicule(id, vehicule);

            return CreatedAtAction(nameof(Get), addedVehicule);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateGarageRequest request)
        {
            var garage = mapper.Map<GarageDto>(request);

            garage.Id = id;

            garageService.Update(garage);

            return NoContent();
        }

        [HttpPut("{id}/vehicules/{vehiculeId}")]
        public IActionResult Put(int id, int vehiculeId, [FromBody] UpdateVehiculeRequest request)
        {
            var vehicule = mapper.Map<VehiculeDto>(request);
            
            vehicule.Id = vehiculeId;

            vehicule.GarageId = id;

            garageService.UpdateVehicule(id, vehicule);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            garageService.Delete(id);

            return NoContent();
        }

        [HttpDelete("{id}/vehicules/{vehiculeId}")]
        public IActionResult Delete([FromRoute] int id, int vehiculeId)
        {
            garageService.DeleteVehicule(id, vehiculeId);

            return NoContent();
        }
    }
}
