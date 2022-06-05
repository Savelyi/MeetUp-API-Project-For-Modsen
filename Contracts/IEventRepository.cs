using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEventRepository
    {
        void CreateEvent(Event eventToCreate);
        void DeleteEvent(Event eventToDelete);
        Task<PagedList<Event>> GetAllEventsAsync(EventParameters parameters,bool trackChanges);
        Task<Event> GetEventByIdAsync(Guid Id, bool trackChanges);

        void UpdateEvent(Event eventToUpdate);
    }
}
