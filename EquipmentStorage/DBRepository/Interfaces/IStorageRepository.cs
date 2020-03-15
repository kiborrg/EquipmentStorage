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
        Task<List<LocationEquipCnt>> GetLocationsAsync();
        /// <summary>
        /// Получить оборудование по родительскому местоположению асинхронно
        /// </summary>
        /// <param name="parentId">ИД родителя</param>
        /// <returns>Оборудование</returns>
        Task<List<EquipWithLocation>> GetEquipmentAsync(int? parentId);
        /// <summary>
        /// Получить список типов оборудования асинхронно
        /// </summary>
        /// <returns></returns>
        Task<List<EquipmentType>> GetEquipTypes();
        /// <summary>
        /// Добавить новое оборудование в комнату
        /// </summary>
        /// <param name="equipment">Оборудование для добавления</param>
        void InsertEquip(Equipment equipment);
        /// <summary>
        /// Редактировать оборудование
        /// </summary>
        /// <param name="equipment">Оборудование для редактирования</param>
        void UpdateEquip(Equipment equipment);
        /// <summary>
        /// Удалить оборудование
        /// </summary>
        /// <param name="equipId">ИД оборудования для удаления</param>
        void DeleteEquip(int equipId);
    }
}
