using Cytocol.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Managers
{
    public interface ILawManager
    {
        Task<Law> AddLaw(Law law);
        Task<Law> UpdateLaw(Law law);
        Task<List<Law>> GetAllLaws();

    }
}
