using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Entities
{
    public class Ticket  
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        [Required]
        public bool IsResolved { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        public int UserId { get; set; }
        public List<string> ImagePaths { get; set; } 

        public virtual Lawyer AssignedLawyer { get; set; }

        public int? LawyerId { get; set; }
    }
}
