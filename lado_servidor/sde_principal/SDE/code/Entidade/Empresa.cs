using System;
using System.Collections.Generic;
using System.Text;
using SDE.Atributos;

namespace SDE.Entidade
{
    [AtEnt(EnumBanco.bancoCorp, true, toString="usuario")]
    public class Empresa
    {
        public int
            id, idCliente, idClienteAdmin;
        public string
            usuario;
        public Cliente
            __cliente ,__clienteAdmin;
        /*
        public EmpresaParam
            __param;
        */
        public bool
            isOptanteSimplesNacional;
    }
    /*
    public class EmpresaListas
    {
        public int idEmp;
        public List<Finan_CentroCusto> centroCustos;
        public List<Finan_PlanoConta> planosConta;
        public List<Finan_TipoDocumento> tiposDocumento;
        public List<Finan_Portador> portadores;
        public List<Finan_Conta> contas;
        public List<Finan_TipoPagamento> formas;
    }
    public class EmpresaParam
    {
        public int idEmp;

        public bool
            isOptanteSimplesNacional;


        //telas opcionais do sistema
        public bool
            utilizaItemVeiculo,
            utilizaImpNFE;
    }
    */

}
