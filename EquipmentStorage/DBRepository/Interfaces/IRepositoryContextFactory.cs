using System;
using System.Collections.Generic;
using System.Text;

namespace DBRepository.Interfaces
{
    public interface IRepositoryContextFactory
    {
        RepositoryContext CreateDBContext(string connectionString);
    }
}
