using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TurboRentingv2.Api.Exceptions;
using TurboRentingv2.Api.Interfaces.Repositories;
using TurboRentingv2.Api.Models.Entities;
using TurboRentingv2.Api.Models.Enums;

namespace TurboRentingv2.Api.Models.Repositories
{
    public class GarageRepository : IGarageRepository
    {
        private readonly IRepository<int, Garage> repository;
        private readonly IRepository<int, Client> repositoryClient;

        public GarageRepository(IRepository<int, Garage> repository, IRepository<int, Client> repositoryClient)
        {
            this.repository = repository;
            this.repositoryClient = repositoryClient;
        }

        public void Add(Garage garage) => repository.Add(garage);

        public void Delete(int id) => repository.Delete(id);

        public ICollection<Garage>? Get()
        {
            var entities = repository.Where(g => g.Activo)
                                     .Include(g => g.Vehicules)
                                     .ToList();

            return entities;
        }

        public Garage? Get(int id)
        {
            var entity = repository.Where(g => g.Id == id && g.Activo)
                                   .SingleOrDefault();

            return entity;
        }

        public void Update(Garage garage) => repository.Update(garage);

        /*Metodos De Vehiculo*/

        public void AddVehicule(int garageId, Vehicule vehicule)
        {
            var garage = repository
                .Where(e => e.Id == garageId && e.Activo)
                .Include(e => e.Vehicules)
                .SingleOrDefault();

            if (garage == null)
                throw new GarageNotFoundException(garageId);

            garage?.Vehicules?.Add(vehicule);

            repository.Update(garage);
        }

        public void DeleteVehicule(int garageId, int vehiculeId)
        {
            var garage = repository
                .Where(e => e.Id == garageId && e.Activo)
                .Include(e => e.Vehicules)
                .SingleOrDefault();

            var deletedVehicule = garage?.Vehicules?.ToList()?.Find(v => v.Id == vehiculeId && v.Activo);

            deletedVehicule.Activo = false;

            repository.Update(garage);
        }

        public ICollection<Vehicule>? GetVehicules()
        {
            List<Vehicule> vehicules = new List<Vehicule>();

            var garages = repository
                .Where(e => e.Activo)
                .Include(e => e.Vehicules);

            foreach(var garage in garages)
            {
                if(garage.Vehicules != null)
                    foreach(var vehicule in garage.Vehicules)
                        if(vehicule.Activo)
                            vehicules.Add(vehicule);
            }

            return vehicules;
        }

        public ICollection<Vehicule>? GetVehiculesByBrand(string brand)
        {
            List<Vehicule> vehicules = new List<Vehicule>();

            var garages = repository
                .Where(e => e.Activo)
                .Include(e => e.Vehicules);

            foreach (var garage in garages)
            {
                if (garage.Vehicules != null)
                    foreach (var vehicule in garage.Vehicules)
                        if (vehicule.Activo && vehicule.Brand.Equals(brand))
                            vehicules.Add(vehicule);
            }

            return vehicules;
        }

        public ICollection<Vehicule>? GetVehiculesByModel(string model)
        {
            List<Vehicule> vehicules = new List<Vehicule>();

            var garages = repository
                .Where(e => e.Activo)
                .Include(e => e.Vehicules);

            foreach (var garage in garages)
            {
                if (garage.Vehicules != null)
                    foreach (var vehicule in garage.Vehicules)
                        if (vehicule.Activo && vehicule.Model.Equals(model))
                            vehicules.Add(vehicule);
            }

            return vehicules;
        }

        public ICollection<Vehicule>? GetVehiculesByNDoors(int nDoors)
        {
            List<Vehicule> vehicules = new List<Vehicule>();

            var garages = repository
                .Where(e => e.Activo)
                .Include(e => e.Vehicules);

            foreach (var garage in garages)
            {
                if (garage.Vehicules != null)
                    foreach (var vehicule in garage.Vehicules)
                        if (vehicule.Activo && vehicule.NDoors == nDoors)
                            vehicules.Add(vehicule);
            }

            return vehicules;
        }

        public ICollection<Vehicule>? GetVehiculesByFuel(Fuel fuel)
        {
            List<Vehicule> vehicules = new List<Vehicule>();

            var garages = repository
                .Where(e => e.Activo)
                .Include(e => e.Vehicules);

            foreach (var garage in garages)
            {
                if (garage.Vehicules != null)
                    foreach (var vehicule in garage.Vehicules)
                        if (vehicule.Activo && vehicule.Fuel == fuel)
                            vehicules.Add(vehicule);
            }

            return vehicules;
        }

        public ICollection<Vehicule>? GetVehiculesByCv(int cv)
        {
            List<Vehicule> vehicules = new List<Vehicule>();

            var garages = repository
                .Where(e => e.Activo)
                .Include(e => e.Vehicules);

            foreach (var garage in garages)
            {
                if (garage.Vehicules != null)
                    foreach (var vehicule in garage.Vehicules)
                        if (vehicule.Activo && vehicule.Cv == cv)
                            vehicules.Add(vehicule);
            }

            return vehicules;
        }

        public ICollection<Vehicule>? GetVehiculesByWheelsSize(int wheelsSize)
        {
            List<Vehicule> vehicules = new List<Vehicule>();

            var garages = repository
                .Where(e => e.Activo)
                .Include(e => e.Vehicules);

            foreach (var garage in garages)
            {
                if (garage.Vehicules != null)
                    foreach (var vehicule in garage.Vehicules)
                        if (vehicule.Activo && vehicule.WheelsSize == wheelsSize)
                            vehicules.Add(vehicule);
            }

            return vehicules;
        }

        public ICollection<Client>? GetVehiculeClients(string registration)
        {
            List<Client> clients= new List<Client>();

            var garages = repository
                .Where(e => e.Activo)
                .Include(e => e.Vehicules);

            foreach (var garage in garages)
            {
                if (garage.Vehicules != null)
                    foreach (var vehicule in garage.Vehicules)
                        if (vehicule.Activo && vehicule.Registration.Equals(registration))
                            foreach(var client in vehicule.Clients)
                                if(client.Activo)
                                    clients.Add(client);
            }

            return clients;

        }

        public Vehicule? GetVehicule(int id)
        {
            List<Vehicule> vehicules = new List<Vehicule>();

            var garages = repository
                .Where(e => e.Activo)
                .Include(e => e.Vehicules);

            foreach(var garage in garages)
                if(garage.Vehicules != null)
                    vehicules.AddRange(garage.Vehicules);

            var vehicule = vehicules.Find(e => e.Id == id && e.Activo);

            if (vehicule == null)
                throw new VehiculeNotFoundException(id);

            return vehicule;
        }

        public Garage? GetGarageVehicule(int garageId)
        {
            var garage = repository.Where(g => g.Id == garageId && g.Activo)
                .Include(g => g.Vehicules)
                .SingleOrDefault();

            return garage;
        }

        public Vehicule? GetVehiculeByRegistration(string registration)
        {
            List<Vehicule> vehicules = new List<Vehicule>();

            var garages = repository
                .Where(g => g.Activo)
                .Include(g => g.Vehicules);

            foreach (var garage in garages)
                if (garage.Vehicules != null)
                    vehicules.AddRange(garage.Vehicules);

            var vehicule = vehicules.Find(v => v.Registration.Equals(registration) && v.Activo);

            if (vehicule == null)
                throw new VehiculeNotFoundException(vehicule.Id);

            return vehicule;
        }

        public void UpdateVehicule(int garageId, Vehicule vehicule)
        {
            var garage = repository
                .Where(e => e.Id == garageId)
                .Include(e => e.Vehicules)
                .SingleOrDefault();

            if (garage == null)
                throw new GarageNotFoundException(garageId);

            UpdateGarage(garage, vehicule);

            repository.Update(garage);
            
        }

        private void UpdateGarage(Garage garage, Vehicule vehicule)
        {
            garage.Vehicules.ToList().Find(v => v.Id == vehicule.Id && v.Activo).Registration = vehicule.Registration;
            garage.Vehicules.ToList().Find(v => v.Id == vehicule.Id && v.Activo).Brand = vehicule.Brand;
            garage.Vehicules.ToList().Find(v => v.Id == vehicule.Id && v.Activo).Model = vehicule.Model;
            garage.Vehicules.ToList().Find(v => v.Id == vehicule.Id && v.Activo).Fuel = vehicule.Fuel;
            garage.Vehicules.ToList().Find(v => v.Id == vehicule.Id && v.Activo).Cv = vehicule.Cv;
            garage.Vehicules.ToList().Find(v => v.Id == vehicule.Id && v.Activo).NDoors = vehicule.NDoors;
            garage.Vehicules.ToList().Find(v => v.Id == vehicule.Id && v.Activo).WheelsSize = vehicule.WheelsSize;
            garage.Vehicules.ToList().Find(v => v.Id == vehicule.Id && v.Activo).Image = vehicule.Image;
            garage.Vehicules.ToList().Find(v => v.Id == vehicule.Id && v.Activo).ClientId = vehicule.ClientId;

            var client = garage.Vehicules.ToList().Find(v => v.Id == vehicule.Id && v.Activo).ClientId;

            if (client != 0 && client != null)
            {
                var clienteToAdd = repositoryClient.Get(client).SingleOrDefault();

                garage.Vehicules.ToList().Find(v => v.Id == vehicule.Id && v.Activo).Clients.Add(clienteToAdd);
            }
        }

    }
}
