using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PaschoalottoBackend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace PaschoalottoBackend.Servicos
{
    public class ServicoUsuario
    {
        private readonly HttpClient _httpClient;
        private readonly ContextoPaschoalotto _contexto;

        public ServicoUsuario(HttpClient httpClient, ContextoPaschoalotto contexto)
        {
            _httpClient = httpClient;
            _contexto = contexto;
        }

        public async Task<Usuario> GerarUsuario()
        {
            try
            {
                // a requisiçao da API q precisamos
                var resposta = await _httpClient.GetAsync("https://randomuser.me/api/");
                resposta.EnsureSuccessStatusCode();

// Aqui estamos transformando a resposta da API, que vem como texto, em um objeto que podemos usar no nosso código.
                var dados = await resposta.Content.ReadFromJsonAsync<RespostaRandomUser>();

                // Vai Verificar se existem resultados
                if (dados?.Resultados == null || dados.Resultados.Length == 0)
                {
                    throw new Exception("Nenhum usuário encontrado na resposta da API.");
                }

                var usuarioApi = dados.Resultados[0];

// junta todos os pedaços do endereço em uma única string para facilitar o uso, >tive dificuldade em entender o que era o postcode, então deixei como objeto< funcionou normalmente.
                var enderecoCompleto = $"{usuarioApi.Location.Street.Number} {usuarioApi.Location.Street.Name}, " +
                       $"{usuarioApi.Location.City}, {usuarioApi.Location.State}, " +
                       $"{usuarioApi.Location.Postcode.ToString()}, {usuarioApi.Location.Country}";

                // vai verificar se os campos necessários estão presentes
                if (usuarioApi.Name == null || usuarioApi.Email == null || usuarioApi.Phone == null || usuarioApi.Dob == null)
                {
                    throw new Exception("Dados do usuário incompletos na resposta da API.");
                }

// Checkar se os campos de nome (primeiro e último) estão preenchidos
                if (usuarioApi.Name.First == null || usuarioApi.Name.Last == null)
                {
                    throw new Exception("Dados do nome do usuário incompletos na resposta da API.");
                }

                // Verifica se a data de nascimento está presente
                if (usuarioApi.Dob.Date == null)
                {
                    throw new Exception("Data de nascimento do usuário ausente na resposta da API.");
                }

                // Mapeia os dados para a classe Usuario
                var usuario = new Usuario
                {
                    Nome = $"{usuarioApi.Name.First} {usuarioApi.Name.Last}",
                    Email = usuarioApi.Email,
                    Telefone = usuarioApi.Phone,
                    Endereco = enderecoCompleto, // Endereço completo
                    DataNascimento = DateTime.Parse(usuarioApi.Dob.Date).ToUniversalTime()
                };

                // Salva o usuário no banco de dados
                _contexto.Usuarios.Add(usuario);
                await _contexto.SaveChangesAsync();

                return usuario;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao fazer a requisição à API Random User Generator.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Erro ao desserializar a resposta da API.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Erro ao salvar os dados no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao gerar usuário.", ex);
            }
        }
    }

    // Classes para desserializar a resposta da API
    public class RespostaRandomUser
    {
        [JsonPropertyName("results")]
        public Resultado[] Resultados { get; set; }
    }

    public class Resultado
    {
        [JsonPropertyName("name")]
        public NomeUsuario Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("dob")]
        public DataNascimentoUsuario Dob { get; set; }
    }

    public class NomeUsuario
    {
        [JsonPropertyName("first")]
        public string First { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }
    }

    public class DataNascimentoUsuario
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("street")]
        public Street Street { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("postcode")]
        public object Postcode { get; set; } // Alterado para object

        [JsonPropertyName("country")]
        public string Country { get; set; }
    }

    public class Street
    {
        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}