using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Interfaces
{
    interface IBase<T> where T : class
    {
        Task<bool> insert(T model);
        Task<bool> update(int id, T model);
        Task<T> detail(int id);
        Task<int> delete(int id);
    }
}
