using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace Sample.Services.Models
{
    public class HotelModel
    {
        [SimpleField(IsKey = true, IsFilterable = true)]
        public string HotelId { get; set; }

        [SearchableField(IsSortable = true)]
        public string HotelName { get; set; }

        [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.EnLucene)]
        public string Description { get; set; }

        [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.FrLucene)]
        [JsonPropertyName("Description_fr")]
        public string DescriptionFr { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string Category { get; set; }

        [SearchableField(IsFilterable = true, IsFacetable = true)]
        public string[] Tags { get; set; }

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public bool? ParkingIncluded { get; set; }

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public DateTimeOffset? LastRenovationDate { get; set; }

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public double? Rating { get; set; }

        [SearchableField]
        public Address Address { get; set; }
    }

    public partial class Address
    {
        [SearchableField(IsFilterable = true)]
        public string StreetAddress { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string City { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string StateProvince { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string PostalCode { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string Country { get; set; }
    }

    public class HotelDocumentManagementModel : HotelModel
    {
        [JsonPropertyName("@search.action")]
        public string Action { get; set; } = DocumentAction.Default.ToString();
    }

    public class DocumentAction
    {
        public const string UploadAction = "upload";
        public const string MergeAction = "merge";
        public const string MergeOrUploadAction = "mergeOrUpload";
        public const string DeleteAction = "delete";

        private DocumentAction()
        {

        }

        private DocumentAction(string action)
        {
            this.action = action;
        }

        public static DocumentAction Default = new DocumentAction(UploadAction);

        public static DocumentAction Upload = new DocumentAction(UploadAction);

        public static DocumentAction Merge = new DocumentAction(MergeAction);

        public static DocumentAction MergeOrUpload = new DocumentAction(MergeOrUploadAction);

        public static DocumentAction Delete = new DocumentAction(DeleteAction);

        public override string ToString()
        {
            return action;
        }

        private readonly string action;
    }

    public class HotelDocumentManagementRequestModel<T>
    {
        [JsonPropertyName("value")]
        public T Value { get; set; }
    }
}
