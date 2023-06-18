
using AutoMapper;
using Castle.Core.Resource;
using Cytocol.Domain.DTO;
using Cytocol.Domain.Entities;
using Cytocol.Domain.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IObjectMapper = Cytocol.Domain.Mappers.IObjectMapper;
namespace Cytocol.Api.Mapper
{
    public class CytocolMapper : IObjectMapper
    {
        private readonly IMapper _mapper;

        public CytocolMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<User, CreateUserDto>();
                cfg.CreateMap<CreateUserDto, User>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Lawyer, LawyerCreatedDto>();
                cfg.CreateMap<LawyerCreatedDto, Lawyer>();
                cfg.CreateMap<CreateUserDto, Lawyer>();
                cfg.CreateMap<TicketDto, Ticket>();
            });
            _mapper = configuration.CreateMapper();
        }

        /// <summary>
        /// Method which creates a new target object from the source object
        /// </summary>
        /// <param name="source">Source object</param>
        /// <typeparam name="TSource">Source object type</typeparam>
        /// <typeparam name="TTarget">Target object type</typeparam>
        /// <returns>New target object</returns>
        public TTarget ConvertToTarget<TSource, TTarget>(TSource source) where TSource : class where TTarget : class =>
            _mapper
                .Map<TSource, TTarget>(source);

        /// <summary>
        /// Method which converts source object to the target one without creating a new instance of the target object.
        /// This means that both the source and target objects are present and only requires to convert some or all the fields.
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="target">Target object</param>
        /// <typeparam name="TSource">Source object type</typeparam>
        /// <typeparam name="TTarget">Target object type</typeparam>
        public void MapToTarget<TSource, TTarget>(TSource source, TTarget target)
        {
            _mapper.Map(source, target);
        }
    }
}