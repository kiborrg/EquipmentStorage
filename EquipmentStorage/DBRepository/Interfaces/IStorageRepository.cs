using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBRepository.Interfaces
{
    public interface IStorageRepository
    {
        /// <summary>
        /// Получить список местоположений
        /// </summary>
        /// <returns>Местоположения</returns>
        List<Location> GetLocations();
        /// <summary>
        /// Получить список местоположений асинхронно
        /// </summary>
        /// <returns>Местоположения</returns>
        //Task<List<Location>> GetLocationsAsync();
        Task<List<LocationEquipCnt>> GetLocationsAsync();
        /// <summary>
        /// Получить оборудование по родительскому местоположению
        /// </summary>
        /// <param name="parentId">ИД родителя</param>
        /// <returns>Оборудование</returns>
        List<Equipment> GetEquipment(int? parentId);
        /// <summary>
        /// Получить оборудование по родительскому местоположению асинхронно
        /// </summary>
        /// <param name="parentId">ИД родителя</param>
        /// <returns>Оборудование</returns>
        Task<List<Equipment>> GetEquipmentAsync(int? parentId);
    }
}
