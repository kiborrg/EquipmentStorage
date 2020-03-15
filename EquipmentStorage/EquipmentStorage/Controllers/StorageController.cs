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

        [HttpGet("[action]")]
        public Task<List<EquipmentType>> GetEquipTypes() => StorageRepository.GetEquipTypes();

        [HttpPost("[action]")]
        public void UpdateEquip([FromBody]Equipment equipment) => StorageRepository.UpdateEquip(equipment);

        [HttpPut("[action]")]
        public void InsertEquip([FromBody]Equipment equipment) => StorageRepository.InsertEquip(equipment);

        [HttpDelete("[action]")]
        public void DeleteEquip(int equipId) => StorageRepository.DeleteEquip(equipId);
    }
}