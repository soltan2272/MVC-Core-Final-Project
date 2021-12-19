using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposotries
{
    public interface IGenericRepostory<T>
    {
        IEnumerable<T> Get();
        T GetByID(int id);
        int Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
