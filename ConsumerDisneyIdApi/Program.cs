using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

Console.OutputEncoding = Encoding.UTF8;

const string endpoint = "https://api.disneyapi.dev/character/423";

try
{
    using var client = new HttpClient();

    // Consome o endpoint e converte o JSON para objetos C#
    var resposta = await client.GetFromJsonAsync<DisneyResponse>(endpoint);

    if (resposta?.Data is null)
    {
        Console.WriteLine("A API não retornou dados.");
        return;
    }

    Console.WriteLine("Nome:");
    Console.WriteLine(resposta.Data.Name);
    Console.WriteLine();
    Console.WriteLine("Imagem:");
    Console.WriteLine(resposta.Data.ImageUrl);
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Erro ao acessar a API: {ex.Message}");
}

// O JSON da Disney API vem dentro de um objeto "data"
public record DisneyResponse(
    [property: JsonPropertyName("data")] Character? Data
);

public record Character(
    [property: JsonPropertyName("_id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("imageUrl")] string ImageUrl
);
