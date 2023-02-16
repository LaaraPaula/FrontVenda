namespace FrontVenda.Models
{
    public class Fornecedor
    {
        public Fornecedor(int? id, string nome, string telefone, string endereco, string cnpj)
        {
            this.id = id;
            this.nome = nome;
            this.telefone = telefone;
            this.endereco = endereco;
            this.cnpj = cnpj;
        }

        public int? id { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public string endereco { get; set; }
        public string cnpj { get; set; }
    }
}
