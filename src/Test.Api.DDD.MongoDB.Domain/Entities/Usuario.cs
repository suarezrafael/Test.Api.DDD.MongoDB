using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Test.Api.DDD.MongoDB.Domain.Entities;

public class Usuario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("senha")]
    public string Senha { get; set; } = string.Empty;
}
