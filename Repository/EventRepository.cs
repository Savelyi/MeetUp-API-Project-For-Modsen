using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateEvent(Event eventToCreate)
        {
            Create(eventToCreate);
        }

        public void DeleteEvent(Event eventToDelete)
        {
            Delete(eventToDelete);
        }

        public async Task<PagedList<Event>> GetAllEventsAsync(EventParameters eventParameters ,bool trackChanges)
        {
            var _events=await FindAll(trackChanges)
                .OrderBy(e=>e.Name)
                .Skip((eventParameters.PageNumber-1)*eventParameters.PageSize)
                .Take(eventParameters.PageSize)
                .ToListAsync();
            var count = await FindAll(trackChanges).CountAsync();
            return new PagedList<Event>(_events, eventParameters.PageNumber, eventParameters.PageSize,count);
        }

        public async Task<Event> GetEventByIdAsync(Guid Id, bool trackChanges)
        {
            return await FindByCondition(e => e.Id.Equals(Id), trackChanges).SingleOrDefaultAsync();
        }

        public void UpdateEvent(Event eventToUpdate)
        {
            Update(eventToUpdate);
        }
    }
}
