using System;
using System.Collections.Generic;
using System.Text;
using SDE.Atributos;
using SDE.Enumerador;

namespace SDE.Entidade
{
    [AtEnt(EnumBanco.bancoCorp, true)]
    public class Balanco
    {
        public int
            id,

            idOperacao, idTransacao;
        public string
            nome,
            dthrInicio, dthrFim;
        public EBalancoSituacao
            situacao;
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public class BalancoItem
    {
        public int
            id,

            idOperacao, idTransacao,

            idBalanco,
            idIEE, idItem;
        public double
            qtdLancada, qtdAnterior,
            custo, compra, venda;
        public string item_nome, estoque_identificador, rfUnica, rfAuxiliar;
    }
}
