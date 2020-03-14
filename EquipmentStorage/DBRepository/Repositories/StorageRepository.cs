using DBRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
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

        public async Task<List<Location>> GetLocationsAsync()
        {
            List<Location> result = new List<Location>();

            using (var context = ContextFactory.CreateDBContext(ConnectionString))
            {
                var query = context.Locations.AsQueryable();
                query = query.Include(l => l.LocationType);
                result = await query.ToListAsync();
            }

            return result;
        }
    }
}
