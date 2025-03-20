using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
   public interface IServicesRepositoryLog<T> where T : class
    {
        List<T> GetAll();
        T GetById(Guid id);
        bool Save(Guid id, Guid userid);
        bool DeleteLog(Guid id);
        bool Delete(Guid id, Guid userid);
        bool Update(Guid id, Guid userid);
    }
}
