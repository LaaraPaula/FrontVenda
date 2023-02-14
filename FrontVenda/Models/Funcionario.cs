namespace FrontVenda.Models
{
    public class Funcionario
    {
        public Funcionario(int id, string nome, string telefone, string endereco, string cPF, string cargo)
        {
            this.id = id;
            this.nome = nome;
            this.telefone = telefone;
            this.endereco = endereco;
            cpf = cPF;
            this.cargo = cargo;
        }

        public int id { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public string endereco { get; set; }
        public string cpf { get; set; }
        public string cargo { get; set; }
    }
    

}
