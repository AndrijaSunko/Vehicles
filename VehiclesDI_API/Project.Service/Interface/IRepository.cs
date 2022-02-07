using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public interface IRepository<T>
    {
        public IEnumerable<T> GetAll();
        public Task<T> Create(T _object);

        public void Update(T _object);
                

        public T GetById(int Id);

        public void Delete(T _object);
    }
}
