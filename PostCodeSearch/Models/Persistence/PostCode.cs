using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace PostCodeSearch.Models.Persistence
{
    [TableName(TableName)]
    [PrimaryKey("Id", AutoIncrement = false)]
    public class PostCodes
    {
        public const string TableName = nameof(PostCodes);

        [Column("Id")]
        public int Id { get; set; }

        [Column("Country")]
        public string Country { get; set; }

        [Column("PostCode")]
        public string PostCode { get; set; }

        [Column("Locality")]
        public string Locality { get; set; }

        [Column("Region")]
        public string Region { get; set; }

        [Column("Long")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public double? Long { get; set; }

        [Column("Lat")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public double? Lat { get; set; }

        [Column("Electorate")]
        public string Electorate { get; set; }

        [Column("ElectorateRating")]
        public string ElectorateRating { get; set; }
    }
}
