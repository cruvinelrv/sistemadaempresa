using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using Db4objects.Db4o;
using SDE.Entidade;

public class AppFacade
{

    public static readonly AppFacade get = new AppFacade();
    private AppFacade()
    {
        //primeiro lê as configurações
        //isso daria erro numa aplicação desktop
        try
        {
            pastaDados = ConfigurationSettings.AppSettings["PastaDados"];
            nomeApp = "SDE";
            isAmbienteTeste = Convert.ToBoolean(ConfigurationSettings.AppSettings["IsAmbienteTeste"]);
            isExecutandoBackup = Convert.ToBoolean(ConfigurationSettings.AppSettings["IsExecutandoBackup"]);
        }
        catch
        {
            throw new Exception("SDE.dll deveria rodar em modo web ou alguma configuração não pode ser lida, vide AppFacade#ctor");
        }

        this.conexaoBanco = new GerenteConectividadeBancoDados(this);
        this.cacheDados = new GerenteCacheBancoDadosCliente(this);
        this.reaproveitamento = new ReaproveitamentoCodigo(this);
        

        
    }

    //

    public GerenteConectividadeBancoDados conexaoBanco { get; private set; }
    public GerenteCacheBancoDadosCliente cacheDados { get; private set; }
    public ReaproveitamentoCodigo reaproveitamento { get; private set; }
    public bool isAmbienteTeste { get; private set; }
    public bool isExecutandoBackup { get; private set; }
    public string pastaDados { get; private set; }
    public string nomeApp { get; private set; }

    public string msg = "vazio";
    /*
    Dictionary<int, GerenteCacheBancoDadosCliente> anotacoesAtualizacoes;
    public GerenteCacheBancoDadosCliente getAnotacoesAtualizacoes(int idCorp)
    {
        if (!anotacoesAtualizacoes.ContainsKey(idCorp))
            anotacoesAtualizacoes[idCorp] = new GerenteCacheBancoDadosCliente();
        return anotacoesAtualizacoes[idCorp];
    }
    */
}