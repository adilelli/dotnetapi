using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoExample.Models;



public class Playlist {

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string UserName { get; set; } = null!;

    public string? LoginTime { get; set; }

    public int? TotalScore { get; set; }

    public int? PrevScore { get; set; }

    public int? TokensRequested { get; set; }

    public string? TxnHash { get; set; } 

    public int? TokensClaimed { get; set; }

    public string? lastTokensClaimedUpdated { get; set; }


}
