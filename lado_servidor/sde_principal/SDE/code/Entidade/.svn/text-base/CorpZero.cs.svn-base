using System.Collections.Generic;
using SDE.Enumerador;
using SDE.Atributos;

namespace SDE.Entidade
{
    [AtEnt(EnumBanco.bancoZero, false)]//, "idLogin"
    public sealed class Max
    {
        public int
            idLogin,
            idCorporacao,
            idCFOP,
            idEstado, idMunicipio,
            idLoginEmpresa;
    }

    [AtEnt(EnumBanco.bancoZero)]
    public class CFOP
    {
        public int id;
        public string codigo, descricao;
    }
    [AtEnt(EnumBanco.bancoZero)]
    public sealed class IBGEMunicipio
    {
        public int id;
        public string nome, codigo, codigoEstado;
    }
    [AtEnt(EnumBanco.bancoZero)]
    public sealed class IBGEEstado
    {
        public int id;
        public string nome, codigo, sigla;
    }
    /*
    [AtEnt(EnumBanco.bancoZero)]
    public sealed class Login
    {
        public int
            id,
            idCorp, idEmp, idCliente,
            qtd;
        public string
            empresa, usuario, senha,
            ultimo;
        public LoginTelas
            telas;
        public Cliente
            __cliente;
        public Empresa
            __emp;
    }
    */
    [AtEnt(EnumBanco.bancoZero)]
    public sealed class LoginEmpresa
    {
        public int
            id,
            idCorp, idEmp;
        public string
            empresa;
    }

    /*
    [AtEnt(EnumBanco.bancoZero)]
    public sealed class LoginTelas
    {
        public int
            idLogin, idCorp;
        public bool
            cadCli, cadItem, cadOutros, cadItemServico,
            estCancel, estEntSimples, estImpNFE,
            //Balanco, estPDV= pdv Normal, Nota Servico, estPDV4 = notaEntrada, estPDV2 = notaSaida
            estBal1, estPDV, estPDV3, estPDV4, estPDV2, 
           
            
            finCad,

            rel1, eti1,
            
            tecPrin;
    }
    /*
    public sealed class Pessoa
	{
        public int
            id;

        public EPesTipo tipo;
        public EPesEstCivil estado;
        public EPesSexo sexo;

        public string
            nome, cpf_cnpj, rg, rgUF, apelido_razsoc,
            nomePai, nomeMae,
            hash,
            dtNasc, dtRegistro;
        public long
            dtNascTicks, dtRegistroTicks;
        public bool
            isImportado;
    }
    /**/
}
