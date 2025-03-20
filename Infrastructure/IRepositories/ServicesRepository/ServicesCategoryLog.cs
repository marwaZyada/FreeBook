using Domain.Entity;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.ServicesRepository
{
    public class ServicesCategoryLog : IServicesRepositoryLog<Category>
    {
        private readonly FreeBookDbContext _dbContext;

        public ServicesCategoryLog(FreeBookDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        public bool Delete(Guid id, Guid userid)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLog(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public Category GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save(Guid id, Guid userid)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid id, Guid userid)
        {
            throw new NotImplementedException();
        }
    }
}
