using Cytocol.Domain.Entities;
using Cytocol.Domain.Managers;
using Cytocol.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Core.Managers
{
    public class LawManager : ILawManager
    {
        private readonly ILawRepository _repository;
        public LawManager(ILawRepository repository)
        {
            _repository = repository;
        }
        public async Task<Law> AddLaw(Law law)
        {
            return await _repository.Save(law);
        }

        public async Task<List<Law>> GetAllLaws()
        {
           var laws = await _repository.FindAll();
            return (laws);
        }

        public async Task<Law> UpdateLaw(Law law)
        {
            return await (_repository.Update(law));
        }
    }
}
