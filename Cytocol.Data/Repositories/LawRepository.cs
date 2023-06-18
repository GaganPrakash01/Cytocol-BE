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
    public class LawRepository : BaseRepository<Law> , ILawRepository 
    {
        public LawRepository(CytocolDbContext context) : base(context)
        {
        }

    }
}
