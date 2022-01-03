using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDE.Atributos;

namespace SDE.Entidade
{
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class Cargo
    {
        public int id, idEmp, idClienteFuncionarioLogado;
        public string nomeCargo;

        public bool
            comissionado, acessaSistema;

        public bool
            calculaMontanteTotal, calculaMaoDeObra, calculaMaoDeObraGeral,
            calculaMaoDeObraGarantia, calculaMaoDeObraGeralGarantia,
            calculaProdutosEmGarantia, calculaProdutos;

        public double
            comissaoMontanteTotal, comissaoMaoDeObra, comissaoMaoDeObraGeral,
            comissaoMaoDeObraGarantia, comissaoMaoDeObraGeralGarantia,
            comissaoProdutosEmGarantia, comissaoProdutos;
    }

    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class CargoComissionamento
    {
        public int id, idEmp, idCargo;

        public bool
            calculaMontanteTotal, calculaMaoDeObra, calculaMaoDeObraGeral,
            calculaMaoDeObraGarantia, calculaMaoDeObraGeralGarantia,
            calculaProdutosEmGarantia, calculaProdutos;

        public double
            comissaoMontanteTotal, comissaoMaoDeObra, comissaoMaoDeObraGeral,
            comissaoMaoDeObraGarantia, comissaoMaoDeObraGeralGarantia,
            comissaoProdutosEmGarantia, comissaoProdutos;
    }

    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class CargoPermissao
    {
        public int
            id, prioridade, idEmp, idCargo;
        public string
            variavel, classe, tipo, valor;
        public bool
            __forcaAtualizacao;
    }
}
