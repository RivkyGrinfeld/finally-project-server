using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface ICrud<T>
    {
        public Task<bool> Create(T t);
        public Task<bool> Update(T t);
        public Task<bool> Delete(T t);
        public Task<List<T>> GetAll();
        public Task<T> GetById(int t);
        
    }
}
