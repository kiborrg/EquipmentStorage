using DBRepository.Interfaces;
using EquipmentStorage;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DBRepository.Repositories
{
    public class StorageRepository : BaseRepository, IStorageRepository
    {
        public StorageRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory)
        {
            DefaultData defaultData = new DefaultData();
            using (var context = ContextFactory.CreateDBContext(ConnectionString))
            {
                if (!context.LocationTypes.Any())
                    defaultData.InitLocationTypes(ContextFactory, ConnectionString);

                if (!context.Locations.Any())
                    defaultData.InitLocations(ContextFactory, ConnectionString);

                if (!context.EquipmentTypes.Any())
                    defaultData.InitEquipTypes(ContextFactory, ConnectionString);
            }
        }

        public async Task<List<EquipWithLocation>> GetEquipmentAsync(int? parentId)
        {
            List<EquipWithLocation> result = new List<EquipWithLocation>();

            using (var context = ContextFactory.CreateDBContext(ConnectionString))
            {
                string sql = GetLocationsRecursiveQuery(parentId);
                var rooms = context.Locations.FromSql(sql).ToList();

                var query = context.Equipment.Where(e => rooms.Select(r => r.LocationId).Contains(e.RoomId));

                query = query.Include(e => e.Type);
                var equip = await query.ToListAsync();
                foreach (Equipment e in equip)
                {
                    result.Add(new EquipWithLocation
                    {
                        EquipmentId = e.EquipmentId,
                        Count = e.Count,
                        Name = e.Name,
                        RoomId = e.RoomId,
                        Type = e.Type,
                        Room = context.Locations.Include(l => l.LocationType).First(r => r.LocationId == e.RoomId)
                    });

                }
            }

            return result;
        }

        public async Task<List<LocationEquipCnt>> GetLocationsAsync()
        {
            List<LocationEquipCnt> result = new List<LocationEquipCnt>();

            using (var context = ContextFactory.CreateDBContext(ConnectionString))
            {
                var query = context.Locations.AsQueryable();
                query = query.Include(l => l.LocationType);
                List<Location> locations = await query.ToListAsync();
                foreach (Location l in locations)
                {
                    result.Add(new LocationEquipCnt
                    {
                        LocationId = l.LocationId,
                        LocationType = l.LocationType,
                        ParentId = l.ParentId,
                        Name = l.Name,
                        EquipCnt = GetCntEquip(l.LocationId)
                    });
                }
            }

            return result;
        }

        private int GetCntEquip(int? parentId)
        {
            int cntEquip = 0;

            using (var context = ContextFactory.CreateDBContext(ConnectionString))
            {
                string sql = GetLocationsRecursiveQuery(parentId);

                var rooms = context.Locations.FromSql(sql).Select(r => r.LocationId).ToList();

                cntEquip = context.Equipment.Where(e => rooms.Contains(e.RoomId)).Sum(e => e.Count);
            }

            return cntEquip;
        }

        private string GetLocationsRecursiveQuery(int? parentId)
        {
            return $@"WITH Tree AS (SELECT LocationId, LocationTypeId, Name, ParentId FROM Locations WHERE LocationId {(parentId == null ? "IS NULL" : "= " + parentId.ToString())} 
			                        UNION ALL
  			                      SELECT l.LocationId, l.LocationTypeId, l.Name, l.ParentId FROM Locations l JOIN Tree t ON l.ParentId = t.LocationId)
                      SELECT t.LocationId, t.LocationTypeId, t.Name, t.ParentId
                      FROM Tree t
                      WHERE LocationTypeId = 3";
        }

        public async Task<List<EquipmentType>> GetEquipTypes()
        {
            List<EquipmentType> result = new List<EquipmentType>();

            using (var context = ContextFactory.CreateDBContext(ConnectionString))
            {
                var query = context.EquipmentTypes.AsQueryable();

                result = await query.ToListAsync();
            }

            return result;
        }

        public void InsertEquip(Equipment equipment)
        {
            using (var context = ContextFactory.CreateDBContext(ConnectionString))
            {
                string sql = $@"INSERT INTO Equipment (TypeId, Name, count, RoomId) VALUES(@type, @name, @count, @room)";

                var type = new SqlParameter("@type", equipment.Type.Id);
                var name = new SqlParameter("@name", equipment.Name);
                var count = new SqlParameter("@count", equipment.Count);
                var room = new SqlParameter("@room", equipment.RoomId);

                context.Database.BeginTransaction();
                var numRows = context.Database.ExecuteSqlCommand(sql, new object[] { type, name, count, room });
                context.Database.CommitTransaction();
            }
        }
        public void UpdateEquip(Equipment equipment)
        {
            using (var context = ContextFactory.CreateDBContext(ConnectionString))
            {
                var prs = new List<object>();
                var prsStr = new List<String>();

                if (equipment.Type != null)
                {
                    prs.Add(new SqlParameter("@type", equipment.Type.Id));
                    prsStr.Add("TypeId = @type");
                }

                if (equipment.Name != null)
                {
                    prs.Add(new SqlParameter("@name", equipment.Name));
                    prsStr.Add("name = @name");
                }

                if (equipment.Count != -1)
                {
                    prs.Add(new SqlParameter("@count", equipment.Count));
                    prsStr.Add("Count = @count");
                }

                if(prs.Count > 0)
                {
                    prs.Add(new SqlParameter("@equip", equipment.EquipmentId));
                    string sql = $@"UPDATE Equipment SET {string.Join(", ", prsStr)} WHERE EquipmentId = @equip";
                    context.Database.BeginTransaction();
                    var numRows = context.Database.ExecuteSqlCommand(sql, prs.ToArray());
                    context.Database.CommitTransaction();
                }
            }
        }
        public void DeleteEquip(int equipId)
        {
            using (var context = ContextFactory.CreateDBContext(ConnectionString))
            {
                string sql = $@"DELETE FROM Equipment WHERE EquipmentId = @equip";

                var equip = new SqlParameter("@equip", equipId);

                context.Database.BeginTransaction();
                var numRows = context.Database.ExecuteSqlCommand(sql, equip);
                context.Database.CommitTransaction();
            }
        }
    }
}
