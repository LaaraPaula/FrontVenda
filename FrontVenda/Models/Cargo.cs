using Microsoft.Graph;
using System.Collections.Generic;

namespace FrontVenda.Models
{
    public class Cargo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public List<Cargo> GetCargos()
        {
            List<Cargo> listCargo = new List<Cargo>();

            listCargo.Add(new Cargo() { Id = 1, Descricao = "Chefe" });
            listCargo.Add(new Cargo() { Id = 2, Descricao = "Gerente" });
            listCargo.Add(new Cargo() { Id = 3, Descricao = "Vendedor" });
            listCargo.Add(new Cargo() { Id = 4, Descricao = "Atendente" });
            listCargo.Add(new Cargo() { Id = 5, Descricao = "Telefonista" });
            listCargo.Add(new Cargo() { Id = 6, Descricao = "Gerente Comercial" });

            return listCargo;
        }

    }
}
