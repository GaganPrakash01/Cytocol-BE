using Cytocol.Data.Context;
using Cytocol.Domain.Entities;
using Cytocol.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Data.Repositories
{

    public class LawyerRepository : BaseRepository<Lawyer>,ILawyerRepository
    {
        public LawyerRepository(CytocolDbContext context) : base(context) { }

        public async Task<Lawyer> DeleteLawyer(Lawyer lawyer)
        {
            throw new NotImplementedException();
        }
    }
}
