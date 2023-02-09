namespace VendaFront.Dto
{
    public class FornecedorDto
    {
        public FornecedorDto(int id, string nome, string telefone, string endereco, string cNPJ)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            Endereco = endereco;
            CNPJ = cNPJ;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string CNPJ { get; set; }
    }
}
