using Cytocol.Domain.DTO;
using Cytocol.Domain.Entities;
using Cytocol.Domain.Exceptions;
using Cytocol.Domain.Managers;
using Cytocol.Domain.Mappers;
using Cytocol.Domain.Repositories;
using Cytocol.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Core.Managers
{
    public class LawyerManager : ILawyerManager
    {
        private readonly ILawyerRepository _lawyerRepository;
        private readonly IPasswordManager _passwordManager;
        private readonly IObjectMapper _mapper;

        public LawyerManager(ILawyerRepository repository, IPasswordManager passwordManager, IObjectMapper mapper)
        {
            _lawyerRepository = repository;
            _passwordManager = passwordManager;
            _mapper = mapper;
        }

        public async Task<Lawyer> CreateLawyer(LawyerCreatedDto createdLawyer)
        {
            if (await _lawyerRepository.FindFirst(x => x.Email.Equals(createdLawyer.Email)) != null)
            {
                throw new EmailAddressAlreadyExistsException("Email address already exists, Please try again!");
            }
            var lawyer = _mapper.ConvertToTarget<LawyerCreatedDto, Lawyer>(createdLawyer);
            var salt = _passwordManager.GenerateSalt();
            lawyer.Password = "lawyer123";
            var hashedPassword = _passwordManager.HashPassword(lawyer.Password, salt);

            //lawyer.Password = hashedPassword;
            
            lawyer.UserName = lawyer.Email;
            lawyer.AccountActive = true;
            return await _lawyerRepository.Save(lawyer);
        }

        public Task<List<Lawyer>> GetAllLawyers() => _lawyerRepository.FindAll();
     

        public async Task<Lawyer> GetLawyerByUserName(string username)
        {
            var lawyer = await _lawyerRepository.FindFirst(x => x.Email.Equals(username));
            if (lawyer == null)
            {
                throw new LawyerNotFoundException("The given Email ID is not registered");
            }
            return lawyer;
        }

        public async Task<Lawyer> MarkLawyerAsDeleted(int lawyerId)
        {
            var lawyer = await _lawyerRepository.FindById(lawyerId);
            if (lawyer == null)
            {
                throw new LawyerNotFoundException("The given adminId does not exist");
            }
            
            await _lawyerRepository.DeleteLawyer(lawyer); 
            return lawyer;
        }

        public async Task<Lawyer> UpdateLawyer(int userId,CreateUserDto lawyer)
        {
            var existingLawyer = await _lawyerRepository.FindFirst(c => c.Id == userId);
            if (lawyer == null)
            {
                throw new LawyerNotFoundException("The given Lawyer id does not exist");
            }
            _mapper.MapToTarget<CreateUserDto, Lawyer>(lawyer, existingLawyer);
            return await _lawyerRepository.Update(existingLawyer);

        }

        public async Task<Lawyer> GetLawyerById(int lawyerId)
        {
            try
            {
                return await _lawyerRepository.FindById(lawyerId);
            }
            catch(Exception ex)
            {
                throw new LawyerNotFoundException(ex.Message);
            }
        }


    }

  
}
