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
        public Task<List<LocationEquipCnt>> GetLocationsAsync() => StorageRepository.GetLocationsAsync();

        [HttpGet("[action]")]
        public Task<List<EquipWithLocation>> GetEquipmentAsync(int? parentId) => StorageRepository.GetEquipmentAsync(parentId);
    }
}