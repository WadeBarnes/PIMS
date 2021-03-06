using AutoMapper;
using Pims.Dal.Entities;
using Entity = Pims.Dal.Entities;
using Model = Pims.Api.Areas.Admin.Models;

namespace backend.Helpers.Profiles.Converters
{
    public class ParcelSubAgencyConverter : IValueConverter<Entity.Agency, string>
    {
        public string Convert(Entity.Agency sourceMember, ResolutionContext context)
        {
            if (sourceMember?.ParentId == null) return null;
            return sourceMember.Code;
        }
    }
}
