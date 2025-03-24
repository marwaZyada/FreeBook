using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IservicesRepository<T> where T : class
    {
        List<T> GetAll();
        T GetBy(Guid id);
        T GetBy(string name);
        T Save(T entity);
        bool Delete(Guid id);
      
    }
}
