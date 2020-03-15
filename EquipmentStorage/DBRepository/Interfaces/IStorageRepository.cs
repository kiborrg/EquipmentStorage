using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBRepository.Interfaces
{
    public interface IStorageRepository
    {
        /// <summary>
        /// Получить список местоположений асинхронно
        /// </summary>
        /// <returns>Местоположения</returns>
        //Task<List<Location>> GetLocationsAsync();
        Task<List<LocationEquipCnt>> GetLocationsAsync();
        /// <summary>
        /// Получить оборудование по родительскому местоположению асинхронно
        /// </summary>
        /// <param name="parentId">ИД родителя</param>
        /// <returns>Оборудование</returns>
        Task<List<EquipWithLocation>> GetEquipmentAsync(int? parentId);
    }
}
