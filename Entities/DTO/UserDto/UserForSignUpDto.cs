using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.UserDto
{
    public class UserForSignUpDto
    {
        [Required(ErrorMessage ="UserName field is required")]
        [MaxLength(15)]
        [MinLength(5)]
        public string UserName { get; set; }
        [MaxLength(20)]
        public string FullName { get; set; }
        [Required(ErrorMessage ="Password is required")]
        [MaxLength(15)]
        [MinLength(5)]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Role { get; set; }

    }
}
