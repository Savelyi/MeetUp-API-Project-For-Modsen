using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.EventDto
{
    public class EventToCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required(ErrorMessage ="Time is required")]
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
        [Required(ErrorMessage ="Place is required")]
        public string Place { get; set; }
    }
}
