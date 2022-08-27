using System.Text.Json.Serialization;


namespace PostCodeSearch.Models
{
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public class AustralianPostCodeImport
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("postcode")]
        public string PostCode { get; set; }

        [JsonPropertyName("locality")]
        public string Locality { get; set; }


        [JsonPropertyName("state")]
        public string State { get; set; }


        [JsonPropertyName("long")]
        public double? Long { get; set; }


        [JsonPropertyName("lat")]
        public double? Lat { get; set; }


        [JsonPropertyName("dc")]
        public string DC { get; set; }


        [JsonPropertyName("type")]
        public string Type { get; set; }


        [JsonPropertyName("status")]
        public string Status { get; set; }


        [JsonPropertyName("sa3")]
        public int? SA3 { get; set; }


        [JsonPropertyName("sa3name")]
        public string SA3Name { get; set; }

        [JsonPropertyName("sa4")]
        public int? SA4 { get; set; }


        [JsonPropertyName("sa4name")]
        public string SA4Name { get; set; }


        [JsonPropertyName("region")]
        public string Region { get; set; }


        [JsonPropertyName("Lat_precise")]
        public double? LatPrecise { get; set; }

        [JsonPropertyName("Long_precise")]
        public double? LongPrecise { get; set; }

        [JsonPropertyName("SA1_MAINCODE_2011")]
        public int? SA1MainCode2011 { get; set; }

        [JsonPropertyName("SA1_MAINCODE_2016")]
        public int? SA1MainCode2016 { get; set; }

        [JsonPropertyName("SA2_MAINCODE_2016")]
        public int? SA2MainCode2016 { get; set; }

        [JsonPropertyName("SA2_NAME_2016")]
        public string SA2Name2016 { get; set; }

        [JsonPropertyName("SA3_CODE_2016")]
        public int? SA3Code2016 { get; set; }

        [JsonPropertyName("SA3_NAME_2016")]
        public string SA3Name2016 { get; set; }

        [JsonPropertyName("SA4_CODE_2016")]
        public int? SA4Code2016 { get; set; }

        [JsonPropertyName("SA4_NAME_2016")]
        public string SA4Name2016 { get; set; }

        [JsonPropertyName("RA_2011")]
        public int? RA2011 { get; set; }

        [JsonPropertyName("RA_2016")]
        public int? RA2016 { get; set; }

        [JsonPropertyName("MMM_2015")]
        public int? MMM2015 { get; set; }

        [JsonPropertyName("MMM_2019")]
        public int? MMM2019 { get; set; }

        [JsonPropertyName("ced")]
        public string Ced { get; set; }

        [JsonPropertyName("altitude")]
        public decimal? Altitude { get; set; }

        [JsonPropertyName("chargezone")]
        public string Chargezone { get; set; }

        [JsonPropertyName("phn_code")]
        public string PhnCode { get; set; }

        [JsonPropertyName("phn_name")]
        public string PhnName { get; set; }

        [JsonPropertyName("lgaregion")]
        public string LgaRegion { get; set; }

        [JsonPropertyName("electorate")]
        public string Electorate { get; set; }

        [JsonPropertyName("electoraterating")]
        public string ElectorateRating { get; set; }

    }
}
