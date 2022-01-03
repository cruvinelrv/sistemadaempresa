using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDE.Atributos;

namespace SDE.Entidade
{
    [AtEnt(EnumBanco.bancoCorp, true)]
    public class Cad_Marca
    {
        public int id, idClienteFuncionarioLogado;
        public string marca, modelo, __orderBy;
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public class Cad_Grade
    {
        public int id, idClienteFuncionarioLogado;
        public string mae, mae_rf, filha, filha_rf, __orderBy;
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public class Cad_Secao
    {
        public int id, idClienteFuncionarioLogado;
        public string secao, grupo, subgrupo, __orderBy;
    }
}
