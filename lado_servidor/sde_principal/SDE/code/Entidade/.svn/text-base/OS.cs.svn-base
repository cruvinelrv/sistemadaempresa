using SDE.Enumerador;
using System.Collections.Generic;
using SDE.Entidade;
using SDE.Atributos;

namespace SDE.Entidade
{
    [AtEnt(EnumBanco.bancoCorp)]
    public class OrdemServico
    {
        public int
            id,
            
            idOperacao, idTransacao,
            
            idEmp, idCliente,
            idClienteFuncionarioLogado,
            idOrdemServicoTipo,
            
            idMovFinalizacao;
        public string
            numOS, descricao, obs,
            cliente_nome, cliente_cpf, cliente_contato, cliente_endereco_cobranca;
        public string
            veiculo, placa, kilometragem, numMotor,
            maquina, implAgricola,
            equipamento, numSerie,
            servico, defeitoReclamado, defeiroConstatado;
        public bool
            isContratado;

        public double
            vlrItensInicial, vlrItensFinal, vlrAcrescimo, vlrTotal;

        public string
            dthrLancamento,
            dthrInicio, dthrPrevisao, dthrConclusao;
        public long
            dtOrdemServicoTicks,
            dtInicioTicks, dtPrevisaoTicks, dtConclusaoTicks;

        public EOrdemServicoStatus status;
        public IList<OrdemServico_Item> __oSItens;

    }
    [AtEnt(EnumBanco.bancoCorp)]
    public class OrdemServico_Tipo
    {
        public int
            id, idClienteFuncionarioLogado;
        public string
            nome;
        public bool
            veiculo, placa, kilometragem, numMotor,
            maquina, implAgricola,
            equipamento, numSerie,
            servico, defeitoReclamado, defeitoConstatado;
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public class OrdemServico_Item
    {
        public int
            id, idOrdemServico, idItem, idIEE, idIEP;
        public double
            qtd,
            vlrUnitVendaInicial, vlrUnitVendaFinal,
            vlrUnitVendaFinalQtd;

        public string
            tipoItem,
            dthrInicio, dthrPrevisao, dthrConclusao;

        public string
            nomeCompl, item_nome, estoque_identificador,
            rf_unica, rf_auxiliar, unid_med;

        public long
            dtInicioTicks, dtPrevisaoTicks, dtConclusaoTicks;
        public EOrdemServicoStatus status;

        public bool
            __removaMe;

        public Item
            __item;
        public IList<MovItemEstoque>
            __oSIEstoques;
        public IList<OrdemServico_Executor>
            __executores;
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public class OrdemServico_Executor
    {
        public int
            id, idOrdemServico, idOrdemServicoItem, idClienteExecutor;

        public string
            dthrInicio, dthrPrevisao, dthrConclusao;
        public long
            dtInicioTicks, dtPrevisaoTicks, dtConclusaoTicks;
        public EOrdemServicoStatus status;

        public bool
            __removaMe;

        public Cliente __executor;
    }
}
