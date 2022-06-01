using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Event
    {
        public  Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string Place { get; set; }
        public string OrganizerId { get; set; }
        public User Organizer{ get; set; }

    }
}
