using AutoMapper;
using TurboRentingv2.Api.Exceptions;
using TurboRentingv2.Api.Interfaces.Repositories;
using TurboRentingv2.Api.Interfaces.Services;
using TurboRentingv2.Api.Models.Entities;
using TurboRentingv2.Api.Models.EntitiesDtos;
using TurboRentingv2.Api.Models.Enums;
using TurboRentingv2.Api.Models.Repositories;

namespace TurboRentingv2.Api.Services
{
    public class GarageService : IGarageService
    {
        private readonly IMapper mapper;
        private readonly IGarageRepository garageRepository;

        public GarageService(IMapper mapper, IGarageRepository garageRepository)
        {
            this.mapper = mapper;
            this.garageRepository = garageRepository;
        }

        public GarageDto Add(GarageDto garage)
        {
            var entity = mapper.Map<Garage>(garage);

            garageRepository.Add(entity);

            var result = mapper.Map<GarageDto>(entity);

            return result;
        }

        public void Delete(int id) => garageRepository.Delete(id);

        public ICollection<GarageDto> Get()
        {
            var garages = garageRepository.Get();

            if (garages == default)
                return default;

            var result = garages.Select(mapper.Map<GarageDto>)
                                .ToList();

            return result;
        }

        public GarageDto GetById(int id)
        {
            var entity = garageRepository.Get(id);

            if (entity == default)
                throw new GarageNotFoundException(id);

            var result = mapper.Map<GarageDto>(entity);

            return result;
        }

        public void Update(GarageDto garage)
        {
            Garage? currentGarage = garageRepository.Get(garage.Id);

            if (currentGarage == default)
                throw new GarageNotFoundException(garage.Id);

            Garage updatedGarage = mapper.Map(garage, currentGarage);

            garageRepository.Update(updatedGarage);
        }



        /*Servicios de vehiculo*/
        public VehiculeDto AddVehicule(int garageId, VehiculeDto vehicule)
        {
            var entity = mapper.Map<Vehicule>(vehicule);

            garageRepository.AddVehicule(garageId, entity);

            var result = mapper.Map<VehiculeDto>(entity);

            return result;
        }

        public void DeleteVehicule(int garageId, int vehiculeId) => garageRepository.DeleteVehicule(garageId, vehiculeId);

        public ICollection<ClientDto> GetClientsVehicule(string registration)
        {
            var vehiculeClients = garageRepository.GetVehiculeClients(registration);

            if (vehiculeClients == default)
                return default;

            var result = vehiculeClients.Select(mapper.Map<ClientDto>).ToList();

            return result;
        }

        public GarageDto GetGarageVehicule(int id)
        {
            var garage = garageRepository.GetGarageVehicule(id);

            if (garage == default)
                throw new GarageNotFoundException(id);

            var result  = mapper.Map<GarageDto>(garage);

            return result;
        }

        public VehiculeDto GetVehiculeById(int id)
        {
            var entity = garageRepository.GetVehicule(id);

            if(entity == default)
                throw new VehiculeNotFoundException(id);

            var result = mapper.Map<VehiculeDto>(entity);

            return result;
        }

        public ICollection<VehiculeDto> GetVehicules()
        {
            var entities = garageRepository.GetVehicules();

            if (entities == default)
                return default;

            var result = entities.Select(mapper.Map<VehiculeDto>).ToList();

            return result;
        }

        public ICollection<VehiculeDto> GetVehiculesByBrand(string brand)
        {
            var entities = garageRepository.GetVehiculesByBrand(brand);

            if (entities == default)
                return default;

            var result = entities.Select(mapper.Map<VehiculeDto>).ToList();

            return result;
        }

        public ICollection<VehiculeDto> GetVehiculesByCv(int cv)
        {
            var entities = garageRepository.GetVehiculesByCv(cv);

            if (entities == default)
                return default;

            var result = entities.Select(mapper.Map<VehiculeDto>).ToList();

            return result;
        }

        public ICollection<VehiculeDto> GetVehiculesByFuel(Fuel fuel)
        {
            var entities = garageRepository.GetVehiculesByFuel(fuel);

            if (entities == default)
                return default;

            var result = entities.Select(mapper.Map<VehiculeDto>).ToList();

            return result;
        }

        public ICollection<VehiculeDto> GetVehiculesByModel(string model)
        {
            var entities = garageRepository.GetVehiculesByModel(model);

            if (entities == default)
                return default;

            var result = entities.Select(mapper.Map<VehiculeDto>).ToList();

            return result;
        }

        public ICollection<VehiculeDto> GetVehiculesByNDoors(int nDoors)
        {
            var entities = garageRepository.GetVehiculesByNDoors(nDoors);

            if (entities == default)
                return default;

            var result = entities.Select(mapper.Map<VehiculeDto>).ToList();

            return result;
        }

        public ICollection<VehiculeDto> GetVehiculesByWheelsSize(int wheelsSize)
        {
            var entities = garageRepository.GetVehiculesByWheelsSize(wheelsSize);

            if (entities == default)
                return default;

            var result = entities.Select(mapper.Map<VehiculeDto>).ToList();

            return result;
        }

        public VehiculeDto GeVehiculetByRegistration(string registrarion)
        {
            var entity = garageRepository.GetVehiculeByRegistration(registrarion);

            if (entity == default)
                throw new VehiculeNotFoundException(entity.Id);

            var result = mapper.Map<VehiculeDto>(entity);

            return result;
        }

        public void UpdateVehicule(int garageId, VehiculeDto vehicule)
        {
            Vehicule? currentVehicule = garageRepository.GetVehicule(vehicule.Id);

            if (currentVehicule == default)
                throw new VehiculeNotFoundException(vehicule.Id);

            Vehicule vehiculeToUpdate = mapper.Map(vehicule, currentVehicule);

            garageRepository.UpdateVehicule(garageId, vehiculeToUpdate);
        }

    }
}
