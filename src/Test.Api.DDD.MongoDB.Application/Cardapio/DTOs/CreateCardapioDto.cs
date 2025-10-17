namespace Test.Api.DDD.MongoDB.Application.Cardapio.DTOs;

public class CreateCardapioDto
{
    public string Titulo { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public bool PossuiPreparo { get; set; }
}
