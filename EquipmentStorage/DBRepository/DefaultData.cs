using DBRepository.Interfaces;
using DBRepository.Repositories;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentStorage
{
    public class DefaultData 
    {
        public DefaultData() 
        { }
        
        public void InitLocationTypes(IRepositoryContextFactory ContextFactory, string ConnectionString)
        {
            using (var context = ContextFactory.CreateDBContext(ConnectionString))
            {
                if (!context.LocationTypes.Any())
                {
                    context.LocationTypes.AddRange(
                        new LocationType
                        {
                            Id = 1,
                            Name = "Организация"
                        },
                        new LocationType
                        {
                            Id = 2,
                            Name = "Здания"
                        },
                        new LocationType
                        {
                            Id = 3,
                            Name = "Комната"
                        });
                    try
                    {
                        context.Database.OpenConnection();
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT LocationTypes ON;");
                        context.SaveChanges();
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT LocationTypes OFF;");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        public void InitLocations(IRepositoryContextFactory ContextFactory, string ConnectionString)
        {
            using (var context = ContextFactory.CreateDBContext(ConnectionString))
            {
                if (!context.Locations.Any())
                {
                    context.Locations.AddRange(
                        new Location
                        {
                            LocationId = 1,
                            LocationType = context.LocationTypes.First(l => l.Id == 1),
                            Name = "ООО Паскаль",
                            ParentId = null
                        },
                        new Location
                        {
                            LocationId = 2,
                            LocationType = context.LocationTypes.First(l => l.Id == 2),
                            Name = "Главное здание",
                            ParentId = 1
                        },
                        new Location
                        {
                            LocationId = 3,
                            LocationType = context.LocationTypes.First(l => l.Id == 2),
                            Name = "Склад",
                            ParentId = 1
                        },
                        new Location
                        {
                            LocationId = 4,
                            LocationType = context.LocationTypes.First(l => l.Id == 3),
                            Name = "Каб. 1",
                            ParentId = 2
                        },
                        new Location
                        {
                            LocationId = 5,
                            LocationType = context.LocationTypes.First(l => l.Id == 3),
                            Name = "Каб. 2",
                            ParentId = 2
                        },
                        new Location
                        {
                            LocationId = 6,
                            LocationType = context.LocationTypes.First(l => l.Id == 3),
                            Name = "Каб. С-1",
                            ParentId = 3
                        },
                        new Location
                        {
                            LocationId = 7,
                            LocationType = context.LocationTypes.First(l => l.Id == 3),
                            Name = "Каб. С-2",
                            ParentId = 3
                        });
                    try
                    {
                        context.Database.OpenConnection();
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Locations ON;");
                        context.SaveChanges();
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Locations OFF;");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        public void InitEquipTypes(IRepositoryContextFactory ContextFactory, string ConnectionString)
        {
            using (var context = ContextFactory.CreateDBContext(ConnectionString))
            {
                if (!context.EquipmentTypes.Any())
                {
                    context.EquipmentTypes.AddRange(
                        new EquipmentType
                        {
                            Id = 1,
                            Name = "Монитор"
                        },
                        new EquipmentType
                        {
                            Id = 2,
                            Name = "МФУ"
                        },
                        new EquipmentType
                        {
                            Id = 3,
                            Name = "Системный блок"
                        },
                        new EquipmentType
                        {
                            Id = 4,
                            Name = "Кондиционер"
                        });
                    try
                    {
                        context.Database.OpenConnection();
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT EquipmentTypes ON;");
                        context.SaveChanges();
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT EquipmentTypes OFF;");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}
