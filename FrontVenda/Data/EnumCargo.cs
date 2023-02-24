using System;
using System.Collections.Generic;
using System.Linq;

namespace APIVenda.Data.Enum
{
    public enum EnumCargo
    {
        Chefe = 1,
        Gerente = 2,
        Vendedor = 3,
        Atendente = 4,
        Telefonista = 5,
        GerenteComercial = 6
    }

    public class EnumCargoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public EnumCargoModel GetAtributo(EnumCargo enum1)
        {
            switch (enum1)
            {
                case EnumCargo.Atendente: { Nome = "Atendente"; break; }
                case EnumCargo.Gerente: { Nome = "Gerente"; break; }
                case EnumCargo.Vendedor: { Nome = "Vendedor"; break; }
                case EnumCargo.Chefe: { Nome = "Chefe"; break; }
                case EnumCargo.Telefonista: { Nome = "Telefonista"; break; }
                case EnumCargo.GerenteComercial: { Nome = "Gerente Comercial"; break; }
                default: return null;
            }
            return this;
        }
        public IList<EnumCargoModel> MostraCargos()
        {
            return ((IEnumerable<EnumCargo>)System.Enum.GetValues(typeof(EnumCargo))).Select(c => new EnumCargoModel() { Id = (int)c, Nome = GetAtributo(c).Nome }).ToList();
        }
    }
}