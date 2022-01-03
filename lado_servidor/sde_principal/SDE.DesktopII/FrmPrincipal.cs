using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Flash.External;
using SDE.Desktop.ConexaoWeb;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace SDE.Desktop
{
    public partial class FrmPrincipal : Form
    {
        public string FrentCaixa, MapResum, MLoja, MSet, MultiTef, TefTyna; 

        public FrmPrincipal()
        {
            InitializeComponent();
            iniciaFlashExternal();
            iniciaUninfe();
            BackColor = Color.LightBlue;
            escondeSDEDesktop();
            notifyIcon.ShowBalloonTip(0);
        }

        public Boolean instaladoUninfe()
        {
            if (Directory.Exists(@"C:\unimake\uninfe\"))
                return true;
            else
                return false;
        }

        private void iniciaUninfe()
        {
            Boolean booUninfe = false;
            
            foreach (Process processo in Process.GetProcesses())
            {
               if (processo.ProcessName == "uninfe")
               booUninfe = true;
            }

            if (booUninfe == false)
            {
                if (Directory.Exists(@"C:\unimake\uninfe\"))
                    Process.Start(@"C:\unimake\uninfe\uninfe.exe");
            }
        }

        private void iniciaFlashExternal()
        {
            if (!Program.verificaExecucao())
            {
                //Inicializar();
                foreach (Process processo in Process.GetProcesses())
                {
                    if (processo.ProcessName == "SDE.Desktop")
                    {
                        Close();
                        processo.Kill();
                    }
                }
            }

            //atualizado

            int receiverweb = new Random().Next(1000, 2000);
            int receiverproxy = new Random().Next(3000, 4000);
            lerConfig(@"C:\Documentos SDE\SDE_DesktopConfig.xml");
            string[] navegadores = {
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Google\Chrome\Application\chrome.exe",
               "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe",
                "C:\\Arquivos de Programas\\Google\\Chrome\\Application\\chrome.exe",
                "C:\\Arquivos de programas\\Opera\\opera.exe",
                "C:\\Arquivos de programas\\Mozilla Firefox\\firefox.exe" 
            };
            
            //String urlServidor = @"http://localhost:2000/";
            //String urlServidor = @"http://sistemadaempresa.com.br/sde/";
            String urlServidor = @"http://sistemadaempresa.com/sde/";
            String queryString = string.Format("?proxy=1&receiverweb={0}&receiverproxy={1}", receiverweb, receiverproxy);

            //String swfIndex = string.Concat("TesteLocalConnection.swf", queryString);
            String swfProxy = string.Concat("DesktopProxy.swf", queryString);
            String swfIndex = string.Concat("index.swf", queryString);
            String urlSwfQueryString = "\"" + urlServidor + swfIndex + "\"";

            /*Este bloco de código apresentou problemas em execução no Win7 devido
             * a direfença na estrutura de diretórios entre o WinXP e seus sucessores.
             * Com esta verificação de diretórios existentes o problema foi sanado.*/
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Programs) + "\\Inicializar\\"))
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Programs) + "\\SDE\\SDE.Desktop.appref-ms"))
                {
                    File.Copy
                        (Environment.GetFolderPath(Environment.SpecialFolder.Programs) + "\\SDE\\SDE.Desktop.appref-ms",
                        Environment.GetFolderPath(Environment.SpecialFolder.Programs) + "\\Inicializar\\SDE.Desktop.appref-ms", true);
                }
            }
            else if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Programs) + "\\Startup\\"))
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Programs) + "\\SDE\\SDE.Desktop.appref-ms"))
                {
                    File.Copy
                        (Environment.GetFolderPath(Environment.SpecialFolder.Programs) + "\\SDE\\SDE.Desktop.appref-ms",
                        Environment.GetFolderPath(Environment.SpecialFolder.Programs) + "\\Startup\\SDE.Desktop.appref-ms", true);
                }
            }

            string meuNavegador = "explorer";

            foreach (string navegador in navegadores)
            {
                if (File.Exists(navegador))
                {
                    meuNavegador = navegador;
                    break;
                }
            }

            if (manualToolStripMenuItem.Enabled == true)
            {
                System.Diagnostics.Process.Start(meuNavegador, urlSwfQueryString);
            }
            /*
            if (File.Exists(executavelChrome1))
                navegador = executavelChrome1;
            else if (File.Exists(executavelChrome2))
                navegador = executavelChrome2;
            else if (File.Exists(executavelFirefox))
                navegador = executavelFirefox;
            System.Diagnostics.Process.Start(navegador, urlSwfQueryString);
            */
            //System.Threading.Thread.Sleep(5000);
            //"C:\Documents and Settings\Thiago\Configurações locais\Dados de aplicativos\Google\Chrome\Application\chrome.exe"


            this.IntrovertIMApp.LoadMovie(0, urlServidor + swfProxy);
            connWeb = new ConnWeb(this.IntrovertIMApp);
        }

        private void escondeSDEDesktop()
        {
            WindowState = FormWindowState.Maximized;
        }

        private ConnWeb connWeb;

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            int idCorp = 1;

            string url = String.Format("http://www.lalala.com/downloads/{0}/{1}", idCorp, "relatorio_clientes.pdf");
            string path = String.Format(@"c:\sde\{0}", "relatorio_clientes.pdf");

            GerenteDownload gd = new GerenteDownload();
            gd.BaixaArquivo(url, path, false);
            return;
             * */
            
            connWeb.eventoEscreveArquivoTEF += new ConnWeb.EventoEscreveArquivoTEF(connWeb_eventoEscreveArquivoTEF);
            connWeb.eventoEscreveArquivoNFE += new ConnWeb.EventoEscreveArquivoNFE(connWeb_eventoEscreveArquivoNFE);
			connWeb.eventoEscreveArquivoNFExml += new ConnWeb.EventoEscreveArquivoNFExml(connWeb_eventoEscreveArquivoNFExml);
            connWeb.eventoImprimeDanfe += new ConnWeb.EventoImprimeDanfe(connWeb_eventoImprimeDanfe);
            connWeb.eventoEscreveArquivoDMS += new ConnWeb.EventoEscreveArquivoDMS(connWeb_eventoEscreveArquivoDMS);
            connWeb.eventoImprimeEtiquetas += new ConnWeb.EventoImprimeEtiquetas(connWeb_eventoImprimeEtiquetas);
            connWeb.eventoEscreveArquivoPDF += new ConnWeb.EventoEscreveArquivoPDF(connWeb_eventoEscreveArquivoPDF);
            connWeb.eventoBaixaListaCasamento += new ConnWeb.EventoBaixaListaCasamento(connWeb_eventoBaixaListaCasamento);
            connWeb.eventoBaixaDuplicata += new ConnWeb.EventoBaixaDuplicata(connWeb_eventoBaixaDuplicata);
            connWeb.eventoBaixaInventario += new ConnWeb.EventoBaixaInventario(connWeb_eventoBaixaInventario);
            connWeb.eventoBaixaRelatorioCliente += new ConnWeb.EventoBaixaRelatorioCliente(connWeb_eventoBaixaRelatorioCliente);
            connWeb.eventoBaixaRelatorioParcialBalanco += new ConnWeb.EventoBaixaRelatorioParcialBalanco(connWeb_eventoBaixaRelatorioParcialBalanco);
            connWeb.eventoBaixaCarne += new ConnWeb.EventoBaixaCarne(connWeb_eventoBaixaCarne);
            connWeb.eventoBaixaRelatorio += new ConnWeb.EventoBaixaRelatorio(connWeb_eventoBaixaRelatorio);
            connWeb.eventoBaixaRelatorioOrdemServico += new ConnWeb.EventoBaixaRelatorioOrdemServico(connWeb_eventoBaixaRelatorioOrdemServico);
            //connWeb.eventoIniciaProcesso += new ConnWeb.EventoIniciaProcesso(connWeb_eventoIniciaProcesso);
            carregarSiteSde();
        }

        void connWeb_eventoEscreveArquivoDMS(int idMov, string conteudo)
        {
            GerenteDMS dms = new GerenteDMS();
            dms.escreveArquivoDMS(idMov, conteudo);
        }
        /*
        void connWeb_eventoIniciaProcesso(string executavel, string parametros)
        {
            Console.Beep();
            GerenteProcessos proc = new GerenteProcessos();
            proc.iniciaProcesso(executavel, parametros);
        }
        */
        void connWeb_eventoEscreveArquivoNFE(string conteudo, string chaveAcessoNFE)
        {
            GerenteNFE nfe = new GerenteNFE();
            //nfe +=
            nfe.escreveArquivoNFE(conteudo, chaveAcessoNFE);
        }
		
		void connWeb_eventoEscreveArquivoNFExml(string conteudo, string chaveAcessoNFE)
        {
            GerenteNFE nfe = new GerenteNFE();
            //nfe +=
            nfe.escreveArquivoNFExml(conteudo, chaveAcessoNFE);
        }

        void connWeb_eventoImprimeDanfe(string arquivoPdf)
        {
            GerenteNFE nfe = new GerenteNFE();
            nfe.abrePdfDanfe(arquivoPdf);
        }

        void connWeb_eventoEscreveArquivoTEF(string conteudo, string complemento)
        {
            GerenteTEF tef = new GerenteTEF();
            tef.eventoRetornoTEF += new GerenteTEF.EventoRetornoTEF(tef_eventoRetornoTEF);
            tef.escreveArquivoTEF(conteudo, complemento);
        }

        void tef_eventoRetornoTEF(int coo)
        {
            connWeb.enviaweb_retornoTEF(coo);
        }

        void connWeb_eventoImprimeEtiquetas(int idCorp)
        {
            GerenteEtiquetas barras = new GerenteEtiquetas();
            barras.enviaImpressao(idCorp);
        }

        void connWeb_eventoEscreveArquivoPDF(int idCorp, int idEmp, int idMov, string tipo)
        {
            GerentePDF PDF = new GerentePDF();
            PDF.escreveArquivoPDF(idCorp, idEmp, idMov, tipo);
        }

        void connWeb_eventoBaixaListaCasamento(int idCorp)
        {
            GerenteBaixaListaCasamento listaCasamento = new GerenteBaixaListaCasamento();
            listaCasamento.baixaListaCasamento(idCorp);
        }

        void connWeb_eventoBaixaDuplicata(int idCorp, int idEmp, string numeroTitulo, string tipoDocumento)
        {
            GerenteBaixaDuplicata baixaDuplicata = new GerenteBaixaDuplicata();
            baixaDuplicata.baixaDuplicata(idCorp, idEmp, numeroTitulo, tipoDocumento);
        }

        void connWeb_eventoBaixaInventario(int idCorp, int idEmp, string tipoPreco, double pctSobreValor, string dataInventario, string textoCabecalho, bool mostraZerados, string tipoDocumento)
        {
            GerenteBaixaInventario baixaInventario = new GerenteBaixaInventario();
            baixaInventario.baixaInventario(idCorp, idEmp, tipoPreco, pctSobreValor, dataInventario, textoCabecalho, mostraZerados, tipoDocumento);
        }
        void connWeb_eventoBaixaRelatorioCliente(int idCorp, int idEmp)
        {
            GerenteBaixaRelatorioCliente baixaRelaorioCliente = new GerenteBaixaRelatorioCliente();
            baixaRelaorioCliente.baixaRelatorioCliente(idCorp, idEmp);
        }
        void connWeb_eventoBaixaRelatorioParcialBalanco(int idCorp)
        {
            GerenteBaixaRelatorioParcialBalanco baixaRelatorioParcialBalanco = new GerenteBaixaRelatorioParcialBalanco();
            baixaRelatorioParcialBalanco.baixaRelatorioParcialBalanco(idCorp);
        }
        void connWeb_eventoBaixaCarne(int idCorp)
        {
            GerenteCarne carne = new GerenteCarne();
            carne.baixaCarne(idCorp);
        }
        void connWeb_eventoBaixaRelatorioOrdemServico(int idCorp, int idOrdemServico)
        {
            GerenteBaixaRelatorioOrdemServico baixaRelatorioOrdemServico = new GerenteBaixaRelatorioOrdemServico();
            baixaRelatorioOrdemServico.baixaRelatorioOrdemServico(idCorp, idOrdemServico);
        }
        void connWeb_eventoBaixaRelatorio(int idCorp, string relatorio, string nomeRelatorio)
        {
            GerenteBaixaRelatorio baixaRelatorio = new GerenteBaixaRelatorio();
            baixaRelatorio.BaixaRelatorio(idCorp, relatorio, nomeRelatorio);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
                   
           
        }

        private void FrmPrincipal_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Maximized;
                carregarSiteSde();

            }
        }
        
        private void mostrarSDEDesktopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon_DoubleClick(sender, e);
        }

        private void sairDoSDEDesktopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void carregarSiteSde()
        {
            string url = "http://www.sistemadaempresa.com";
            webBrowser1.Navigate(url);
        }


        /*
        private void Inicializar()
        {   
            /*
            if (File.Exists("C:\\Ativa\\CyprusPlus\\fbclient.dll"))
                File.Delete("C:\\Ativa\\CyprusPlus\\fbclient.dll");
            if (File.Exists("C:\\Ativa\\CyprusPlus\\cyprus.sql"))
                File.Delete("C:\\Ativa\\CyprusPlus\\cyprus.sql");
            if (File.Exists("C:\\Ativa\\CyprusPlus\\CYP.FDB"))
                File.Delete("C:\\Ativa\\CyprusPlus\\CYP.FDB");
            if (File.Exists("C:\\Ativa\\CyprusPlus\\dbexpint.dll"))
                File.Delete("C:\\Ativa\\CyprusPlus\\dbexpint.dll");
            foreach (Process processo in Process.GetProcesses())
            {
                if (processo.ProcessName == "fbserver")
                {
                    processo.Kill();
                }
            }
        
        }
        */

        
        public void defineNavegador()
        {
            string[] navegadores = {
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Google\Chrome\Application\chrome.exe",
               "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe",
                "C:\\Arquivos de Programas\\Google\\Chrome\\Application\\chrome.exe",
                "C:\\Arquivos de programas\\Opera\\opera.exe",
                "C:\\Arquivos de programas\\Mozilla Firefox\\firefox.exe"
            };       
        }

        private void btnManualSDE_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://docs.google.com/document/pub?id=1qFXGVqA-PPiTRp8GtbFIHymq0gfe7bjenCTTIlt5UMk");
            //btnManualSDE.Visible = false;
            //btnMenu.Text = "VOLTAR";
            //System.Diagnostics.Process.Start("chrome.exe","https://docs.google.com/document/pub?id=1qFXGVqA-PPiTRp8GtbFIHymq0gfe7bjenCTTIlt5UMk");
            
        }

        private void FrmPrincipal_Click(object sender, EventArgs e)
        {
            carregarSiteSde();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
           carregarSiteSde();
           //btnManualSDE.Visible = true; 
           //btnMenu.Text = "SISTEMA DA EMPRESA";
           
           //System.Diagnostics.Process.Start("chrome.exe","http://www.sistemadaempresa.com.br/sde");
        }

        private void sistemaDaEmpresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            carregarSiteSde();
        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://docs.google.com/document/pub?id=1qFXGVqA-PPiTRp8GtbFIHymq0gfe7bjenCTTIlt5UMk");
        }

        private void nFeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.sistemadaempresa.com.br/programas/nfe/nfe.application");
        }

        #region Tooltip Botões

        private void sistemaDaEmpresaToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolTipMenu.SetToolTip(menuStrip1, "HOME");
        }

        private void manualToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolTipMenu.SetToolTip(menuStrip1, "MANUAL SDE");
        }

        private void nFeToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolTipMenu.SetToolTip(menuStrip1, "NFE");
        }

        private void multisoftLojaToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolTipMenu.SetToolTip(menuStrip1, "MULTISOFT LOJA");
        }

        private void multisoftSETToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolTipMenu.SetToolTip(menuStrip1, "MULTISOFT SET");
        }

        private void multiTEFToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolTipMenu.SetToolTip(menuStrip1, "MULTITEF");
        }

        private void tEFTYNAToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolTipMenu.SetToolTip(menuStrip1, "TEFTYNA");
        }

        private void frenteDeCaixaToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolTipMenu.SetToolTip(menuStrip1, "FRENTE DE CAIXA");
        }

        private void mapaResumpToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolTipMenu.SetToolTip(menuStrip1, "MAPA RESUMO ECF");
        }

        private void xToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolTipMenu.SetToolTip(menuStrip1, "BACKUP");
        }

        private void cONFIGURACOESToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolTipMenu.SetToolTip(menuStrip1, "CONFIGURAÇÕES");
        }

        private void sINTEGRAToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolTipMenu.SetToolTip(menuStrip1, "MULTISOFT SINTEGRA");
        }

        #endregion

        #region Ações dos Botões

        private void multisoftLojaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(MLoja);
        }

        private void multiTEFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(MultiTef);
        }

        private void multisoftSETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(MSet);
        }

        private void tEFTYNAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(TefTyna);
        }

        private void mapaResumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(MapResum);
        }

        private void sINTEGRAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.sistemadaempresa.com.br/programas/sintegra/GeraSintegra.application");
        }

        private void BackuptoolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.sistemadaempresa.com/programas/sdebackup/SDEBackup.application");
        }

        private void cONFIGURACOESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.sistemadaempresa.com/programas/sdeconfiguracoes/SDEConfiguracoes.application");
        }

        private void fRENTEDECAIXAToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(FrentCaixa, "VendaSuper");
        }

        private void eNCERRAMENTODECAIXAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(FrentCaixa, "Encerra");
        }

        private void cONSULTADEPAGAMENTOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(FrentCaixa, "ConsPgto");
        }

        #endregion

        #region Ler Configurações SDE.Desktop
        public void lerConfig(string caminho)
        {
            if (File.Exists(caminho))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(caminho);
                XmlNodeList xFrentCaixa = doc.GetElementsByTagName("FrentCaixa");
                XmlNodeList xMapResum = doc.GetElementsByTagName("MapResum");
                XmlNodeList xMLoja = doc.GetElementsByTagName("MLoja");
                XmlNodeList xMSet = doc.GetElementsByTagName("MSet");
                XmlNodeList xMultiTef = doc.GetElementsByTagName("MultiTef");
                XmlNodeList xNFE = doc.GetElementsByTagName("NFE");
                XmlNodeList xSDE = doc.GetElementsByTagName("SDE");
                XmlNodeList xSintegra = doc.GetElementsByTagName("SINTEGRA");
                XmlNodeList xTefTyna = doc.GetElementsByTagName("TefTyna");


                FrentCaixa = xFrentCaixa[0].InnerText;
                frenteDeCaixaToolStripMenuItem.Enabled = Convert.ToBoolean(xFrentCaixa[0].Attributes["Enabled"].InnerText);

                MapResum = xMapResum[0].InnerText;
                mapaResumpToolStripMenuItem.Enabled = Convert.ToBoolean(xMapResum[0].Attributes["Enabled"].InnerText);

                MLoja = xMLoja[0].InnerText;
                multisoftLojaToolStripMenuItem.Enabled = Convert.ToBoolean(xMLoja[0].Attributes["Enabled"].InnerText);

                MSet = xMSet[0].InnerText;
                multisoftSETToolStripMenuItem.Enabled = Convert.ToBoolean(xMSet[0].Attributes["Enabled"].InnerText);

                MultiTef = xMultiTef[0].InnerText;
                multiTEFToolStripMenuItem.Enabled = Convert.ToBoolean(xMultiTef[0].Attributes["Enabled"].InnerText);

                nFeToolStripMenuItem.Enabled = Convert.ToBoolean(xNFE[0].Attributes["Enabled"].InnerText);

                manualToolStripMenuItem.Enabled = Convert.ToBoolean(xSDE[0].Attributes["Enabled"].InnerText);
                
                if (xSintegra.Count > 0)
                {
                    sINTEGRAToolStripMenuItem.Enabled = Convert.ToBoolean(xSintegra[0].Attributes["Enabled"].InnerText);
                }
                
                TefTyna = xTefTyna[0].InnerText;
                tEFTYNAToolStripMenuItem.Enabled = Convert.ToBoolean(xTefTyna[0].Attributes["Enabled"].InnerText);
            }
        }
        #endregion
    }
}