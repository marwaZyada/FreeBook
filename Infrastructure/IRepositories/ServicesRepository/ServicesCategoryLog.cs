using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.ServicesRepository
{
    public class ServicesCategoryLog : IServicesRepositoryLog<LogCategory>
    {
        private readonly FreeBookDbContext _dbContext;

        public ServicesCategoryLog(FreeBookDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        public bool Delete(Guid id, string userid)
        {
            try
            {
                var logCategory = new LogCategory()
                {
                    Id = Guid.NewGuid(),
                    Date=DateTime.Now,
                    Action="Delete",
                    CategoryId=id,
                    ApplicationUserId=userid
                };
                _dbContext.LogCategories.Add(logCategory);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteLog(Guid id)
        {
            try
            {
               var logCategory=_dbContext.LogCategories.Find(id);
                if (!logCategory.Equals(null))
                {
                    _dbContext.LogCategories.Remove(logCategory);
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<LogCategory> GetAll()
        {

            try
            {
                return _dbContext.LogCategories.Include(l=>l.Category).OrderBy(l => l.Date).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public LogCategory GetById(Guid id)
        {
            try
            {
                return _dbContext.LogCategories.Include(l=>l.Category).FirstOrDefault(l => l.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Save(Guid id, string userid)
        {
            try
            {
                var logCategory = new LogCategory()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Action = "Save",
                    CategoryId = id,
                    ApplicationUserId = userid
                };
                _dbContext.LogCategories.Add(logCategory);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Guid id, string userid)
        {
            try
            {
                var logCategory = new LogCategory()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Action = "Update",
                    CategoryId = id,
                    ApplicationUserId = userid
                };
                _dbContext.LogCategories.Add(logCategory);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

       
    }
}
