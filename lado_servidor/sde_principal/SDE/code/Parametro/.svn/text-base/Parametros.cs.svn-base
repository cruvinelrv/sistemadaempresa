using System;
using System.Collections;
using System.Collections.Generic;
using SDE.Enumerador;

namespace SDE.Parametro
{
   
    public sealed class ParamFiltroCliente
    {
        public EPesTipo
            tipo;
        public bool
            funcionario, fornecedor, transportador, parceiro, cliente;
        public string
            texto, cpf;
        public int
            offSet, limit;
    }
    public sealed class ParamLoadCliente
    {
        public bool
            ignorar,
            funcionario, fornecedor, transportador, parceiro,
            bancarios, veiculos,
            contatos, enderecos, familiares;
    }

    public sealed class ParamLoadItem
    {
        public bool
            ignorar,
            precos, estoques, movimentacoes,
            fornecedores,
            aliquotas,
            aliqEntDentro, aliqEntFora, aliqSaiDentro, aliqSaiFora;
    }
    public sealed class ParamFiltroItem
    {
        public int
            offSet, limit;
        public string
            texto,
            secao, grupo, subgrupo;
        public bool
            produto, servico;
    }

    public sealed class ParamLoadMov
    {
        public bool
            ignora,
            movItens, movValores, itens, clientes;
    }
    public sealed class ParamFiltroMov
    {
        public string
            dtInicial, dtFinal, tipos;
        public int
            idCliente, idMov;
    }

    public sealed class ParamFiltroTipoPagamento
    {

        public bool
            parcelas, portador, tipoDocumento;
    }



}
