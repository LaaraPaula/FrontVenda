using Newtonsoft.Json;

namespace FrontVenda.Response
{
    public class ClienteResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("cpf")]
        public string Cpf { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("telefone")]
        public string telefone { get; set; }
        [JsonProperty("endereco")]
        public string Endereco { get; set; }
    }
}
