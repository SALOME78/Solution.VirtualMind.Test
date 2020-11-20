using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Test.Model.ViewModels;

namespace VirtualMind.Test.Interface
{
    public interface IConcreteRepository<T> where T : class
    {
        Task<int> AddAsync(T entity);
        Task<IReadOnlyList<T>> GetAllAsync();
    }
}
