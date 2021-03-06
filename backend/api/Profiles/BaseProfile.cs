using System;
using AutoMapper;
using Pims.Api.Models;
using Entity = Pims.Dal.Entities;

namespace Pims.Api.Helpers.Profiles
{
    public class BaseProfile : Profile
    {
        #region Constructors
        public BaseProfile()
        {
            CreateMap<Entity.BaseEntity, BaseModel>()
                .IncludeAllDerived()
                .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => Convert.ToBase64String(src.RowVersion)));

            CreateMap<BaseModel, Entity.BaseEntity>()
                .IncludeAllDerived()
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore())
                .AfterMap((source, dest) =>
                {
                    if (!String.IsNullOrWhiteSpace(source.RowVersion))
                    {
                        var rowversion = Convert.FromBase64String(source.RowVersion);
                        if (dest.RowVersion == null)
                        {
                            dest.RowVersion = new byte[rowversion.Length];
                        }
                        Buffer.BlockCopy(rowversion, 0, dest.RowVersion, 0, rowversion.Length);
                    }
                });
        }
        #endregion
    }
}
