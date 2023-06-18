using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Entities
{
    public class BaseUser
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Mobile { get; set; }
        [Required]
        public bool? AccountActive { get; set; }
        [Required]
        public string Role { get; set; }

    }
}
