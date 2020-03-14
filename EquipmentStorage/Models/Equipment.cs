namespace Models
{
    public class Equipment
    {
        /// <summary>
        /// ИД оборудования
        /// </summary>
        public int EquipmentId { get; set; }
        /// <summary>
        /// Тип оборудования
        /// </summary>
        public EquipmentType Type { get; set; }
        /// <summary>
        /// Название оборудования
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ИД комнаты, в которой находится
        /// </summary>
        public int RoomId { get; set; }
        /// <summary>
        /// Количество данного оборудования в комнате
        /// </summary>
        public int Count { get; set; }
    }
}
