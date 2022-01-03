using System;
using System.Collections.Generic;
using System.Text;
using SDE.Atributos;

namespace SDE.Entidade
{


    [AtEnt(EnumBanco.bancoCorp, false)]
    public sealed class Incremento
    {
        public int id, valorUltimaID;
        public string entidade;
    }





    [AtEnt(EnumBanco.bancoCorp)]
    public class Corporacao
    {
        public int
            id;
        public string
            nome;
    }
    [AtEnt(EnumBanco.bancoCorp)]
    public class SdeConfig
    {
        public int
            id, prioridade;
        public string
            variavel, classe, tipo, valor;
        public bool
            __forcaAtualizacao;
    }

    /*
    public class Incremento
    {
        public string nome;
        public int valor;
    }
    */
    [AtEnt(EnumBanco.bancoCorp,false)]
    public class CorporacaoMax
    {
        public int
            idEmpresa, idCliente,
            idClienteBancario, idClientePropriedade,
            idClienteContato, idClienteEndereco, idClienteFamiliar, idClienteVeiculo,
            idItem, idIEP, idIEE,
            idMov, idMovItem, idMovNfe,
            idMIE, idMovValor,
            idBalanco, idBalancoItem,

            idCadMarca, idCadSecao, idCadGrade,


            idSdeConfig;

        

        public int
            idFinCentroCusto,
            idFinPlanoConta, idFinPlanoContaTipo,
            idFinPortador, idFinPortadorTipo,
            idFinTipoDocumento,
            idFinConta,
            idFinTipoPagamento, idFinTitulo;
    }
    /*
    public class CorporacaoListas
    {
        public List<Marca> marcas;
        public List<Grade> grades;
        public List<Categoria> categorias;
    }

    public class Marca
    {
        public string nome;
        public List<Marca> filhos;
    }
    public class Grade
    {
        public string nome, rf;
        public List<Grade> filhos;
    }
    public class Categoria
    {
        public string nome;
        public List<Categoria> filhos;
    }
     * */
}
