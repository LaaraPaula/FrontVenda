namespace VendaFront.NewFolder
{
    public class ClienteDto
    {
        public ClienteDto(int id, string nome, string telefone, string endereco, string cPF)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            Endereco = endereco;
            CPF = cPF;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string CPF { get; set; }
    }
}
