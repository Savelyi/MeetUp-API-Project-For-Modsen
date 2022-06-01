using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Events = new List<Event>();
        }
        public IEnumerable<Event> Events { get; set; }
    }
}
