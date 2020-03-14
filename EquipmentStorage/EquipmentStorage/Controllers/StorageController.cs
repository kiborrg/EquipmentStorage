using System.Collections.Generic;
using System.Threading.Tasks;
using DBRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace EquipmentStorage.Controllers
{
    [Route("api/[controller]")]
    public class StorageController : Controller
    {
        protected readonly IStorageRepository StorageRepository;

        public StorageController(IStorageRepository storageRepository)
        {
            StorageRepository = storageRepository;
        }

        [HttpGet("[action]")]
        public List<Location> GetLocations() => StorageRepository.GetLocations();

        [HttpGet("[action]")]
        //public Task<List<Location>> GetLocationsAsync() => StorageRepository.GetLocationsAsync();
        public Task<List<LocationEquipCnt>> GetLocationsAsync() => StorageRepository.GetLocationsAsync();

        [HttpGet("[action]")]
        public List<Equipment> GetEquipment(int? parentId) => StorageRepository.GetEquipment(parentId);

        [HttpGet("[action]")]
        public Task<List<Equipment>> GetEquipmentAsync(int? parentId) => StorageRepository.GetEquipmentAsync(parentId);
    }
}