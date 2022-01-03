using System;
using System.Collections.Generic;
using System.Text;
using SDE.Enumerador;
using SDE.Atributos;

namespace SDE.Entidade
{
    [AtEnt(EnumBanco.bancoCorp, true, toString = "tipo.charAt(0)+' | '+ nome")]
    public sealed class Cliente
    {
        public int
            id, idPessoa, idCargo;
        public string
            obs,
            dtImportacao;

        public bool
            ehFuncionario, ehFornecedor, ehTransportador, ehParceiro,
            ehInativo,
            
            comissionado, usuarioSistema;

        public EPesTipo tipo;
        public EPesEstCivil estado;
        public EPesSexo sexo;
        //
        public string
            nome, cpf_cnpj, rg, rgUF, apelido_razsoc,
            nomePai, nomeMae,
            hash,
            dtNasc, dtRegistro;
        public long
            dtNascTicks, dtRegistroTicks;
        public bool
            isImportado;

        public string
            loginUsuario, loginSenha;

        public bool
            calculaMontanteTotal, calculaMaoDeObra, calculaMaoDeObraGeral,
            calculaMaoDeObraGarantia, calculaMaoDeObraGeralGarantia,
            calculaProdutosEmGarantia, calculaProdutos;

        public double
            comissaoMontanteTotal, comissaoMaoDeObra, comissaoMaoDeObraGeral,
            comissaoMaoDeObraGarantia, comissaoMaoDeObraGeralGarantia,
            comissaoProdutosEmGarantia, comissaoProdutos;

        public List<ClienteBancario> __bancarios;
        public List<ClienteVeiculo> __veiculos;
        public List<ClienteContato> __contatos;
        public List<ClienteEndereco> __enderecos;
        public List<ClienteFamiliar> __familiares;
    }
    /*
    public sealed class ClienteFuncionario
    {
        public int
            idCliente;
    }
    public sealed class ClienteParceiro
    {
        public int
            idCliente;
    }
    public sealed class ClienteFornecedor
    {
        public int
            idCliente;
    }
    public sealed class ClienteTransportador
    {
        public int
            idCliente;
    }
    */
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class ClienteBancario
    {
        public int
            id, idCliente;
        public bool
            isDeletado;
    }

    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class ClienteContato
    {
        public int
            id, idCliente;
        public string
            campo, valor, obs;
        public bool
            isDeletado;
        public EContatoTipo
            tipo;
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class ClienteEndereco
    {
        public int
            id, idCliente;
        public EEnderecoTipo
            tipo;
        public string
            campo,
            tipoLogradouro, logradouro, bairro, cep, cidade, cidadeIBGE, uf, ufIBGE, complemento,
            falarCom,
            inscr, inscrMun, fone, tamanho,
            obs;
        public int
            numero;
        public bool
            isDeletado;
    }

    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class ClienteFamiliar
    {
        public int
            id, idCliente;
        public string
            key, nome, data, fone, obs;
        public bool
            ehDependente, ehAutorizado, isDeletado;
    }


    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class ClienteVeiculo
    {
        public int
            id, idCliente;
        // Para Atender NFE
        public bool
            isDeletado;
        public string
            placaNumero, placaUF,
            regNacTranspCarga;
	    //
        public double valorFIPE;
        public EVeiculoTipo tipo;
        public string
            nome,
            chassi, numSerieMotor, franquia, ano,
            marca, modelo;
    }

    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class ClienteFuncionarioComissionamento
    {
        public int id, idEmp, idCliente;

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
    public sealed class ClienteFuncionarioPermissao
    {
        public int
            id, idEmp, idClienteFuncionarioUsuario, prioridade;
        public string
            variavel, classe, tipo, valor;
        public bool
            __forcaAtualizacao;
    }

    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class ClienteFuncionarioUsuario
    {
        public int
            id, idEmp, idCliente;

        public bool
            usuarioTecnico;

        public string
            login, senha;
    }
}