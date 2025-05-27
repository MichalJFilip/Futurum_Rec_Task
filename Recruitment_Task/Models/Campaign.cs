using System.Text.Json.Serialization;

namespace Recruitment_Task
{
    public enum Keyword
    {
        sale,
        auction,
        perfume,
        clothes,
        technologies,
        games,
    }
    public enum Town
    {
        Cracow,
        Warsaw,
        Łódź,
        Poznań,
        Wrocław,
    }
    public enum Status
    {
        On,
        Off,   
    }
    public class Campaign
    {
        public required int Id { get; set; }
        public required string Name { get; set; }

        [JsonIgnore]
        public List<Keyword> Keywords { get; set; } = new List<Keyword>();
        public string KeywordsString
        {
            get => string.Join(',', Keywords);
            set => Keywords = value.Split(',', StringSplitOptions.RemoveEmptyEntries)
                              .Select(k => Enum.Parse<Keyword>(k))
                              .ToList();
        }

        public required decimal BidAmount { get; set; }
        public required decimal CampaignFund { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required Status Status { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required Town Town { get; set; }
        
        public required decimal Radius { get; set; }
    }
}