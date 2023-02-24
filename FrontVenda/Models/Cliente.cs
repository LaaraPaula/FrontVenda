namespace FrontVenda.Models
{
    public class Cliente
    {
        public Cliente()
        {
            id = 0;
            nome = string.Empty;
            telefone = string.Empty;
            endereco = string.Empty;
            cpf = string.Empty;
        }
        public Cliente(int id, string nome, string telefone, string endereco, string cpf)
        {
            this.id = id;
            this.nome = nome;
            this.telefone = telefone;
            this.endereco = endereco;
            this.cpf = cpf;
        }

        public int id { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public string endereco { get; set; }
        public string cpf { get; set; }
    }
}
