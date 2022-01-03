using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDE.Atributos
{
    public class AtEnt : System.Attribute
    {
        //esse campo primaryKey não está sendo usaaado

        public AtEnt(EnumBanco banco)
        {
            this.banco = banco;
        }
        public AtEnt(EnumBanco banco, bool baixaLocal)
        {
            this.banco = banco;
            this.baixaLocal = baixaLocal;
            this.ehEntidadeInterna = false;
        }
        public AtEnt(EnumBanco banco, bool baixaLocal, bool ehEntidadeInterna)
        {
            this.banco = banco;
            this.baixaLocal = baixaLocal;
            this.ehEntidadeInterna = ehEntidadeInterna;
        }
        public AtEnt(EnumBanco banco, bool baixaLocal, params Pesquisa[] pesquisas)
        {
            this.banco = banco;
            this.baixaLocal = baixaLocal;
            this.pesquisas = pesquisas;
        }
        /*
        public AtEnt(EnumBanco banco, Pesquisa[] pesquisas)
        {
            this.banco = banco;
            this.pesquisas = pesquisas;
        }
        public AtEnt(EnumBanco banco, bool baixaLocal, Pesquisa[] pesquisas)
        {
            this.banco = banco;
            this.baixaLocal = baixaLocal;
            this.pesquisas = pesquisas;
        }
        public AtEnt(EnumBanco banco, bool baixaLocal, string primaryKey, Pesquisa[] pesquisas)
        {
            this.banco = banco;
            this.baixaLocal = baixaLocal;
            this.primaryKey = primaryKey;
            this.pesquisas = pesquisas;
        }
         * */
        //
        public EnumBanco banco;// = EnumBanco.ambos;
        public bool baixaLocal = true;
        public bool ehEntidadeInterna = false;
        public string primaryKey = "id";
        public string toString = null;
        public Pesquisa[] pesquisas;
    }
    /*
    [AtEnt(
        EnumBanco.bancoCorp,
        true,
        new Pesquisa(EnumRetorno.array, "idOperacao")
        )]
     * */
    public class Pesquisa
    {
        public EnumRetorno retorno;
        public string[] camposAmarrados;
        
        public Pesquisa(EnumRetorno retorno, params string[] camposAmarrados)
        {
            this.retorno = retorno;
            this.camposAmarrados = camposAmarrados;
        }
    }
    public class AtributoUtils
    {
        public static AtEnt getAtributos(Type t)
        {
            object[] atributosClasse = t.GetCustomAttributes(false);
            foreach (Object atObj in atributosClasse)
                if (atObj is AtEnt)
                {
                    AtEnt at = (AtEnt)atObj;
                    /*
                    if (t.GetField("dthr") == null)
                        throw new ExcecaoSDE("A Classe '" + t.Name + "' é considerada um classe interna, crie um campo 'dthr' do tipo 'string'");
                    */
                    if (at.ehEntidadeInterna)
                        if (t.GetField("dthr") == null || t.GetField("dthr").FieldType != typeof(string))
                            throw new ExcecaoSDE(t.GetField("dthr").FieldType.Name+"  A Classe '" + t.Name + "' é considerada um classe interna, crie um campo 'dthr' do tipo 'string'");

                    return at;
                }
            throw new Exception("CLASSE "+t.FullName+" NÃO POSSUI ATRIBUTOS");
        }
    }
    public enum EnumBanco
	{
        /*ambos, */bancoZero, bancoCorp
	}
    public enum EnumRetorno
	{
        array, entidade
	}
}
