namespace VendaFront.Dto
{
    public class ProdutoDto
    {
        public ProdutoDto(int id, string nome, string descricao, decimal precoUnitario, int quantidadeEstoque)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            PrecoUnitario = precoUnitario;
            QuantidadeEstoque = quantidadeEstoque;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int QuantidadeEstoque { get; set; }
    }
}
