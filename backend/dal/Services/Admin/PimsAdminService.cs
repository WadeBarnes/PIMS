using System;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Pims.Dal.Services.Admin
{
    /// <summary>
    /// PimsAdminService class, provides a encapsulated way to references all the independent services.
    /// </summary>
    public class PimsAdminService : IPimsAdminService
    {
        #region Variables
        private readonly IServiceProvider _serviceProvider;
        private readonly PimsContext _dbContext;
        #endregion

        #region Properties
        public ClaimsPrincipal Principal { get; }
        /// <summary>
        /// get - The user service.
        /// </summary>
        /// <value></value>
        public IUserService User { get { return _serviceProvider.GetService<IUserService>(); } }

        /// <summary>
        /// get - The role service.
        /// </summary>
        /// <value></value>
        public IRoleService Role { get { return _serviceProvider.GetService<IRoleService>(); } }

        /// <summary>
        /// get - The parcel service.
        /// </summary>
        /// <value></value>
        public IParcelService Parcel { get { return _serviceProvider.GetService<IParcelService>(); } }

        /// <summary>
        /// get - The building service.
        /// </summary>
        /// <value></value>
        public IBuildingService Building { get { return _serviceProvider.GetService<IBuildingService>(); } }

        /// <summary>
        /// get - The address service.
        /// </summary>
        /// <value></value>
        public IAddressService Address { get { return _serviceProvider.GetService<IAddressService>(); } }

        /// <summary>
        /// get - The province service.
        /// </summary>
        /// <value></value>
        public IProvinceService Province { get { return _serviceProvider.GetService<IProvinceService>(); } }

        /// <summary>
        /// get - The city service.
        /// </summary>
        /// <value></value>
        public ICityService City { get { return _serviceProvider.GetService<ICityService>(); } }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a PimsAdminService class, and initializes it with the specified arguments.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="serviceProvider"></param>
        public PimsAdminService(ClaimsPrincipal user, IServiceProvider serviceProvider)
        {
            this.Principal = user;
            _serviceProvider = serviceProvider;
            _dbContext = serviceProvider.GetService<PimsContext>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Wrap the SaveChanges in a transaction for rollback.
        /// </summary>
        /// <returns></returns>
        public int CommitTransaction()
        {
            return _dbContext.CommitTransaction();
        }
        #endregion
    }
}
