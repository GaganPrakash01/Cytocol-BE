using Cytocol.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Repositories
{
    public interface ILawyerRepository : IBaseRepository<Lawyer>
    {
        Task<Lawyer> DeleteLawyer(Lawyer lawyer);
    }
}
