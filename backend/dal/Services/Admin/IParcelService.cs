using System.Collections.Generic;
using Pims.Dal.Entities;
using Pims.Dal.Entities.Models;

namespace Pims.Dal.Services.Admin
{
    /// <summary>
    /// IParcelService interface, provides a service layer to administer parcels within the datasource.
    /// </summary>
    public interface IParcelService : IBaseService<Parcel>
    {
        Paged<Parcel> GetNoTracking(int page, int quantity, string sort);
        Parcel GetNoTracking(int id);
        Parcel GetByPidNoTracking(int pid);
        Parcel Get(int id);
        Parcel GetByPid(int pid);
        IEnumerable<Parcel> Add(IEnumerable<Parcel> parcels);
    }
}
