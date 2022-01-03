using System;
using System.Collections.Generic;
using SDE.Enumerador;
using SDE.Atributos;

namespace SDE.Entidade
{
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class Item
    {
        public int
            id,
            idSecao, idMarca;
        public string
            secao, grupo, subgrupo,
            marca, modelo,
            rfUnica, rfAuxiliar,
            nome, nomeEtiqueta, complAplic, obs,
            classificacaoFiscal,

            locacao1, locacao2, locacao3, locacao4, locacao5,
            locacao6, locacao7, locacao8, locacao9, locacao10;

        public bool desuso;

        public double
            rfPeso;

        public EItemUnidMed unidMed;
        public EItemTipo tipo;
        public EItemOrigem origem;
        public ItemEmp __ie;
        public List<ItemEmp> __listaIE;
        public List<Mov> __movimentacoes;
        public List<ItemFornecedor> __fornecedores;
        public List<ItemEmpEstoque> __estoques;

        public EItemTipoIdent tipoIdent;
        //parte item Veiculo
        //public bool ehVeiculo;
        //public ItemVeiculo __veiculo;
    }

    /*
    public sealed class ItemVeiculo
    {
        public int
            codMarcaModelo,
            serie,  
            potencia,
            distanciaEixos,
            anoModelo, anoFabricacao,
            CMKG, //carga maxima de tracao 
            CM3;
        public double
            pesoL, 
            pesoB;
        public string
            //numMotor,       //tamanho 21
            //chassi,         //tamanho 17
            codCor,           //codigo da cor da montadora
            descCor; 
            
            //tipo da operacao//venda Concessionaria - Faturamento direto - venda direta - outros
        public EVeiculoCombustivel tipoCombustivel;
        public EVeiculoTipo tipoVeiculo;
        public EVeiculoEspecie especie;
        public EVeiculoCondicaoVIN condicaoVIN;//condicao - numero de identificacao do veiculo
        public EVeiculoPintura tipoPintura;
    }
    */
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class ItemEmp
    {
        public int
            id;
        public int
            idItem, idEmp;
        public ItemEmpAliquotas
            __aliquotas;
        public ItemEmpPreco __preco;
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class ItemFornecedor
    {
        public int
            id;
        public int
            idItem, idCliente;
        public bool isDeletado;
        public string nome;
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class ItemEmpAliquotas
    {
        public int
            id;
        public int
            idItem, idEmp, ipiQtdSelo;
        public double
            icmsAliq_ED, icmsAliqPadrao_ED,
            icmsAliq_EF, icmsAliqPadrao_EF,
            icmsAliq_SD, icmsAliqPadrao_SD,
            icmsAliq_SF, icmsAliqPadrao_SF,
            ipiAliq, ipiAliqPadrao,
            pisAliq, pisAliqPadrao,
            cofinsAliq, cofinsAliqPadrao,
            issqn, inss;

        public string
            icmsCST_ED,
            icmsCST_EF,
            icmsCST_SD,
            icmsCST_SF,
            ipiCST,
            pisCST,
            cofinsCST,
            //
            icmsObs_ED,
            icmsObs_EF,
            icmsObs_SD,
            icmsObs_SF,
            ipiObs,
            ipiCNPJ,
            ipiCodSelo, ipiCodEnquad, ipiClasseEnquad;
        public ECalculoIpiTipo
            ipiTipoCalculo;
 
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class ItemEmpPreco
    {
        public int
            id, idEmp, idItem;
        public double
           compra, custo, venda, margemLucro,//vendaOriginal,
           pctComissao, descontoMaximo, atacado/*, promocao*/;
        //public string dtFimPromocao;
        //public long dtFimPromocao_ticks;
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class ItemEmpEstoque
    {
        public int
            id, idEmp, idItem;
        public string
            identificador, codBarras, codBarrasGrade, lote, dtFab, dtVal;
        public double
            qtd, qtdReserva,
            qtdMin, qtdMax,
            custo;
    }



}
