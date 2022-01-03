using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Db4objects.Db4o;
using System.Configuration;
using Ionic.Utils.Zip;
using System.Text;

/// <summary>
/// Numa aplicação web, só vai existir uma instancia dessa classe,
/// que vai ficar dentro de AppFacade
/// Essa classe cuida dos bancos de dados
/// </summary>
public class GerenteConectividadeBancoDados
{
    public enum TipoBanco
    {
        cadastros, incremento
    }
    /// <summary>
    /// Invoque esse contrutor de dentro de um AppFacade
    /// </summary>
    /// <param name="appFacade"></param>
    public GerenteConectividadeBancoDados(AppFacade appFacade)
    {
        //this.servers = new Dictionary<int, IObjectServer>();
        //this.bancos = new Dictionary<string, IObjectContainer>();
        this.bancos = new Dictionary<int, IObjectContainer>();
        a = appFacade;
    }

    AppFacade a;

    public void FechaBancos()
    {
        //foreach (string key in this.bancos.Keys)
        foreach (int key in this.bancos.Keys)
        {
            this.bancos[key].Close();
            this.bancos.Remove(key);
        }
        /*
        foreach (int key in this.servers.Keys)
        {
            this.servers[key].Close();
            this.servers.Remove(key);
        }
         * */
        
    }

    //variáveis
    //Dictionary<int, IObjectServer> servers;
    //Dictionary<string, IObjectContainer> bancos;
    Dictionary<int, IObjectContainer> bancos;


    public IObjectContainer get(int idCorp)
    {
        return get(idCorp, TipoBanco.cadastros);
    }
    /// <summary>
    /// Pegamos a instancia do conexaoBanco (essa instancia será sempre a mesma, para otimizarmos o cache)
    /// </summary>
    /// <param name="idCorp"></param>
    /// <param name="tipoBanco">Normalmente vamos usar um conexaoBanco do tipo "cadastros"</param>
    /// <returns>Instancia do Banco</returns>
    public IObjectContainer get(int idCorp, TipoBanco tipoBanco)
    {
        lock (lock_container)
        {
            if (!this.bancos.ContainsKey(idCorp))
            {
                if (AppFacade.get.isExecutandoBackup)
                    ExecutaBackup(idCorp);
                this.bancos[idCorp] = Db4oFactory.OpenFile(getBancoArquivo(idCorp));
            }
        }
        return this.bancos[idCorp];
    }

    static object lock_log = new object();
    static object lock_container = new object();

    public void geraLog(int idCorp, Exception ex)
    {
        lock (lock_log)
        {
            string pastaLogs = getBancoPastaLogs();

            if (!Directory.Exists(pastaLogs))
                Directory.CreateDirectory(pastaLogs);

            HttpContext http = HttpContext.Current;
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("DB INDEX: {0}\r\n", idCorp);
            sb.AppendFormat("DTHR: {0}\r\n", DateTime.Now);
            if (http != null)
            {
                sb.AppendFormat("USUARIO: {0} / {1}\r\n", http.Request.UserHostAddress, http.Request.UserAgent);
                sb.AppendFormat("PAGINA: {0}\r\n", http.Request.Url.PathAndQuery);
            }
            sb.AppendFormat("Exception na DLL {0}\r\n", ex.Source);
            sb.AppendFormat("msg: {0}\r\n", ex.Message);
            sb.AppendFormat("stack: {0}\r\n", ex.StackTrace);
            string texto = sb.ToString();


            string sLogFile = string.Concat(pastaLogs, "log_", idCorp, ".txt");
            using (StreamWriter sw = new StreamWriter(sLogFile, true))
            {
                //sw.WriteLine("-----------------");
                sw.Write(texto);
                sb.AppendLine("-----------------");
                sb.AppendLine();
                sw.Flush();
            }
        }
    }

    #region Coisas Legais e Backup

    //algumas funções simples
    string getBancoPasta2()
    {
        return string.Format("{0}\\Banco", a.pastaDados);
    }
    string getBancoPastaBackup(string nomeApp)
    {
        return string.Format("{0}\\Backup\\{1}\\ano-{2:yyyy}\\mes-{2:MM}\\dia-{2:dd}", a.pastaDados, nomeApp, DateTime.Today);
    }
    string getBancoPastaLogs()
    {
        return string.Format("{0}\\Logs\\", a.pastaDados);
    }
    string getBancoArquivo(int idCorp)
    {
        return string.Format("{0}\\{1}.dbf", this.getBancoPasta2(), idCorp);
    }

    //estamos dentro de um lock-this
    private void ExecutaBackup(int idCorp)
    {
        //rotina de backup, pega arquivos da pasta Banco e zipa para a pasta Backup

        //1. pega arquivo banco
        string pastaBanco = getBancoPasta2();
        if (!Directory.Exists(pastaBanco))
            return;
        string arqBanco = getBancoArquivo(idCorp);

        //2. define onde backup será feito
        string pastaBackup = getBancoPastaBackup(a.nomeApp);

        if (!Directory.Exists(pastaBackup))
            Directory.CreateDirectory(pastaBackup);

        //string arqBack = string.Concat(pastaBackup, a.nomeApp, n.ToString("_yyyy_MM_dd_HH_mm_ss"), ".zip");
        string arqBack = string.Format("{0}\\{1}.zip", pastaBackup, idCorp);

        //3. executa backup
        using (ZipFile zip = new ZipFile())
        {
            //zip.AddDirectory(arqBanco); // recurses subdirs
            zip.AddFile(arqBanco, ""); // recurses subdirs
            zip.Save(arqBack);
        }
    }
    #endregion
}