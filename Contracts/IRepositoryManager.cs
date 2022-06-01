using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IEventRepository Events { get; }
        void Save();
        Task SaveAsync();

    }
}
