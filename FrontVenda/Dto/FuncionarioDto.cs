namespace VendaFront.Dto
{
    public class FuncionarioDto
    {
        public FuncionarioDto(int id, string nome, string telefone, string endereco, string cPF, int cargo)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            Endereco = endereco;
            CPF = cPF;
            Cargo = cargo;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string CPF { get; set; }
        public int Cargo { get; set; }
    }
    public class ExibeFuncionario 
    {
        public ExibeFuncionario(int id, string nome, string telefone, string endereco, string cPF, string cargo)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            Endereco = endereco;
            CPF = cPF;
            Cargo = cargo;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string CPF { get; set; }
        public string Cargo { get; set; }
    }

}
