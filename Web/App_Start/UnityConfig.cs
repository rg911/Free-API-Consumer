using System;
using Common.Infrastructure.Api;
using Common.Infrastructure.Cache;
using Common.Repository.Implementations;
using Common.Repository.Interfaces;
using Common.ViewModel;
using Microsoft.Practices.Unity;
using UnityLog4NetExtension.Log4Net;

namespace Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main Container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity Container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity Container.</summary>
        /// <param name="container">The unity Container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // Container.LoadConfiguration();

            // Register services
            container.AddNewExtension<Log4NetExtension>();
            container.RegisterType(typeof (IApi<>), typeof (Api<>))
                .RegisterType<ICache, WebCache>()
                .RegisterType<IRatingKeyRepository,RatingKeyRepository>()
                .RegisterType<IEstablishmentRepository, EstablishmentRepository>()
                .RegisterType<IAuthorityRepository, AuthorityRepository>();
            
        }
    }
}
