using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Pims.Api.Models
{
    public class ParcelModel : BaseModel, IEquatable<ParcelModel>
    {
        #region Properties
        public int Id { get; set; }

        public string PID { get; set; }

        public int StatusId { get; set; }

        public string Status { get; set; }

        public int ClassificationId { get; set; }

        public string Classification { get; set; }

        public int AgencyId { get; set; }

        public string SubAgency { get; set; }

        public string Agency { get; set; }

        public AddressModel Address { get; set; }

        public int FiscalYear { get; set; }

        public float EstimatedValue { get; set; }

        public float AssessedValue { get; set; }

        public float NetBookValue { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public float LandArea { get; set; }

        public string Description { get; set; }

        public string LandLegalDescription { get; set; }

        public IEnumerable<Parts.ParcelBuildingModel> Buildings { get; set; } = new List<Parts.ParcelBuildingModel>();

        public override bool Equals(object obj)
        {
            return Equals(obj as ParcelModel);
        }

        public bool Equals([AllowNull] ParcelModel other)
        {
            return other != null &&
                base.Equals(other) &&
                Id == other.Id &&
                PID == other.PID &&
                StatusId == other.StatusId &&
                Status == other.Status &&
                ClassificationId == other.ClassificationId &&
                Classification == other.Classification &&
                AgencyId == other.AgencyId &&
                SubAgency == other.SubAgency &&
                Agency == other.Agency &&
                EqualityComparer<AddressModel>.Default.Equals(Address, other.Address) &&
                FiscalYear == other.FiscalYear &&
                EstimatedValue == other.EstimatedValue &&
                AssessedValue == other.AssessedValue &&
                NetBookValue == other.NetBookValue &&
                Latitude == other.Latitude &&
                Longitude == other.Longitude &&
                LandArea == other.LandArea &&
                Description == other.Description &&
                LandLegalDescription == other.LandLegalDescription &&
                Enumerable.SequenceEqual(Buildings, other.Buildings);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(base.GetHashCode());
            hash.Add(Id);
            hash.Add(PID);
            hash.Add(StatusId);
            hash.Add(Status);
            hash.Add(ClassificationId);
            hash.Add(Classification);
            hash.Add(AgencyId);
            hash.Add(SubAgency);
            hash.Add(Agency);
            hash.Add(Address);
            hash.Add(FiscalYear);
            hash.Add(EstimatedValue);
            hash.Add(AssessedValue);
            hash.Add(NetBookValue);
            hash.Add(Latitude);
            hash.Add(Longitude);
            hash.Add(LandArea);
            hash.Add(Description);
            hash.Add(LandLegalDescription);
            hash.Add(Buildings);
            return hash.ToHashCode();
        }

        #endregion
    }
}
