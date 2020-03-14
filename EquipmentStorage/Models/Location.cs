using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Location
    {
        /// <summary>
        /// ИД Местоположения
        /// </summary>
        public int LocationId { get; set; }
        /// <summary>
        /// Тип местоположения
        /// </summary>
        public LocationType LocationType { get; set; }
        /// <summary>
        /// Название местоположения
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ИД родителя в иерархии
        /// </summary>
        public int? ParentId { get; set; }
    }
}
