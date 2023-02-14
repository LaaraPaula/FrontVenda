namespace FrontVenda.Models
{
    public class Cliente
    {
        public Cliente(int id, string nome, string telefone, string endereco, string cPF)
        {
            this.id = id;
            this.nome = nome;
            this.telefone = telefone;
            this.endereco = endereco;
            cpf = cPF;
        }

        public int id { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public string endereco { get; set; }
        public string cpf { get; set; }
    }
}
