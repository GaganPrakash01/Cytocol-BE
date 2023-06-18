using Cytocol.Domain.DTO;
using Cytocol.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Managers
{
    public interface ILawyerManager
    {
        Task<Lawyer> CreateLawyer(LawyerCreatedDto lawyer);
        Task<Lawyer> MarkLawyerAsDeleted(int lawyerId);
        Task<List<Lawyer>> GetAllLawyers();
        Task<Lawyer> GetLawyerByUserName(string username);
        Task<Lawyer> GetLawyerById(int lawyerId);
        Task<Lawyer> UpdateLawyer(int userId,CreateUserDto lawyer);

    }
}
