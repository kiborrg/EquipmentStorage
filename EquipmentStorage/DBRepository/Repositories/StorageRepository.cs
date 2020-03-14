using DBRepository.Interfaces;
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
        { }

        public List<Equipment> GetEquipment(int? parentId)
        {
            List<Equipment> result = new List<Equipment>();

            using (var context = ContextFactory.CreateDBContext(ConnectionString))
            {
                var query = context.Equipment.AsQueryable();
                if(parentId != null)
                {
                    query = query.Where(s => s.RoomId == parentId);
                }
                result = query.ToList();
            }

            return result;
        }

        public async Task<List<Equipment>> GetEquipmentAsync(int? parentId)
        {
            List<Equipment> result = new List<Equipment>();

            using (var context = ContextFactory.CreateDBContext(ConnectionString))
            {
                var query = context.Equipment.AsQueryable();
                if (parentId != null)
                {
                    query = query.Where(s => s.RoomId == parentId);
                }
                result = await query.ToListAsync();
            }

            return result;
        }

        public List<Location> GetLocations()
        {
            List<Location> result = new List<Location>();

            using (var context = ContextFactory.CreateDBContext(ConnectionString))
            {
                var query = context.Locations.AsQueryable();
                query = query.Include(l => l.LocationType);
                result = query.ToList();
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
                foreach(Location l in locations)
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
    }
}
