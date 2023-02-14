﻿namespace FrontVenda.Models
{
    public class Produto
    {
        public Produto(int id, string nome, string descricao, decimal precoUnitario, int quantidadeEstoque)
        {
            this.id = id;
            this.nome = nome;
            this.descricao = descricao;
            this.precoUnitario = precoUnitario;
            this.quantidadeEstoque = quantidadeEstoque;
        }

        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public decimal precoUnitario { get; set; }
        public int quantidadeEstoque { get; set; }
    }
}
