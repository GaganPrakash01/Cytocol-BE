using Cytocol.Api.Mapper;
using Cytocol.Core.Managers;
using Cytocol.Data.Context;
using Cytocol.Data.Repositories;
using Cytocol.Domain.Managers;
using Cytocol.Domain.Mappers;
using Cytocol.Domain.Repositories;
using Cytocol.Domain.Shared;
using Cytocol.Shared.Authentication;
using Cytocol.Shared.Password;
using System;

using Unity;

namespace Cytocol.Api
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<CytocolDbContext>();
            container.RegisterType<IPasswordManager, BCryptPasswordManager>();
            container.RegisterType<IAuthManager, AuthenticationManager>();
            container.RegisterType<ITokenService, JWTAuthenticator>();
            container.RegisterType<IUserManager, UserManager>();
            container.RegisterType<IObjectMapper, CytocolMapper>();
            container.RegisterType<ILawyerManager, LawyerManager>();
            container.RegisterType<ITicketManager, TicketManager>();
            container.RegisterType<ILawManager, LawManager>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ILawyerRepository, LawyerRepository>();
            container.RegisterType<ITicketRepository, TicketRepository>();
            container.RegisterType<ILawRepository, LawRepository>();
        }
    }
}