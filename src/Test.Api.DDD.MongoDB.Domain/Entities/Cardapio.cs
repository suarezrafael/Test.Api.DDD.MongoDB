using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Test.Api.DDD.MongoDB.Domain.Entities;

public class Cardapio
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("titulo")]
    public string Titulo { get; set; } = string.Empty;

    [BsonElement("preco")]
    public decimal Preco { get; set; }

    [BsonElement("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [BsonElement("possuiPreparo")]
    public bool PossuiPreparo { get; set; }
}
