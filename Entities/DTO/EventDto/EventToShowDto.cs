using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.EventDto
{
    public class EventToShowDto
    {
        public string Id { get; set; }

        public string OrganizerName { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string Place { get; set; }
    }
}
