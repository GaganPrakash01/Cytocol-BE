using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.DTO
{
    public class TicketDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsResolved { get; set; }
        [Required]
        public int UserId { get; set; }
        public List<string> ImagePaths { get; set; }
        public int LawyerId { get; set; }

    }
}
