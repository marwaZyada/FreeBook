using Domain.Entity;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.ServicesRepository
{
    public class ServicesCategory : IservicesRepository<Category>
    {
        private readonly FreeBookDbContext _dbContext;

        public ServicesCategory(FreeBookDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        public bool Delete(Guid id)
        {
            try
            {
                var entity = _dbContext.Categories.FirstOrDefault(c => c.CurrentState > 0 && c.Id == id);
                entity.CurrentState = (int)(Helper.EcurrentState.Delete);
                _dbContext.Categories.Update(entity);
               _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Category> GetAll()
        {
            try
            {
                return _dbContext.Categories.OrderBy(c=>c.Name).Where(c=>c.CurrentState>0).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Category GetBy(Guid id)
        {
            try
            {
                return _dbContext.Categories.FirstOrDefault(c=>c.CurrentState>0 && c.Id==id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Category GetBy(string name)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.Name.Contains(name.Trim())&& c.CurrentState>0);
        }

        public bool Save(Category entity)
        {
            try
            {
                var result= _dbContext.Categories.FirstOrDefault(c => c.CurrentState > 0 && c.Id == entity.Id);
                if (result == null)
                {
                    entity.Id = Guid.NewGuid();
                    entity.CurrentState = (int)Helper.EcurrentState.Active;
                    _dbContext.Categories.Add(entity);
               
                 
                }
                else
                {
                    result.Name = entity.Name;
                    result.Description = entity.Description;
                    result.CurrentState = (int)Helper.EcurrentState.Active;
                    _dbContext.Categories.Update(entity);
                    
                 
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

       
    }


}
