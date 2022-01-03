using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Net;
using Ionic.Utils.Zip;
using SDE.Desktop;

namespace SDE.Desktop
{
    public partial class Backup : Form
    {
        #region Variaveis

        public string empresa, data;
        public int nArqServidor = 0;

        #endregion

        
        public Backup()
        {
            InitializeComponent();
        }

        #region Funcões

        //lê as configurações contidas no Xml para backup
        public void lerConfigBackup(string configXMLBackup)
        {
            if (File.Exists(configXMLBackup))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(configXMLBackup);
                    XmlNodeList Xempresa = doc.GetElementsByTagName("Backup");
                    XmlNodeList Xpasta = doc.GetElementsByTagName("pasta");
                    empresa = Xempresa[0].Attributes["Empresa"].InnerText;


                    for (int i = 0; i < Xpasta.Count; i++)
                    {
                        dataGridView1.Rows.Add(Xpasta[i].InnerText, Xpasta[i].Attributes[0].InnerText);
                    }
                }
                catch (Exception erro)
                {
                    MessageBox.Show("ERRO: " + erro.Message);
                }
            }

        }

        //copia a pasta com seus arquivos e subdiretorios
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }


            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(temppath, false);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        //gera uma pasta temporia com as pasta e arquivos a serem compactados
        public void GeraTmp()
        {
            #region verifica se existem as pastas

            if (!Directory.Exists(@"C:\Documentos SDE\tmp\SERVIDOR_ZIP\"))
            {
                Directory.CreateDirectory(@"C:\Documentos SDE\tmp\SERVIDOR_ZIP\");
            }

            if (!Directory.Exists(@"C:\Documentos SDE\tmp\LOCAL_ZIP\"))
            {
                Directory.CreateDirectory(@"C:\Documentos SDE\tmp\LOCAL_ZIP\");
            }

            #endregion

            Application.DoEvents();
            status.Text = "Gerando arquivos temporarios...";
            //recebe a quantidade de linhas do datagridview
            int cont = dataGridView1.Rows.Count;

            progressBar1.Maximum = cont;

            //laço de repetição do para gerar as copias da estrutura do datagridview
            for (int i = 0; i < cont; i++)
            {
                string caminho = dataGridView1.Rows[i].Cells[0].Value.ToString();
                bool copiaServidor = Convert.ToBoolean(dataGridView1.Rows[i].Cells[1].Value);

                #region copia as pasta do backup no servidor

                if (copiaServidor == true)
                {
                    try
                    {
                        DirectoryInfo pasta = new DirectoryInfo(caminho);
                        string destino = @"C:\Documentos SDE\tmp\SERVIDOR_ZIP\" + pasta.Name.ToString();
                        Directory.CreateDirectory(destino);
                        DirectoryCopy(caminho, destino, true);
                        nArqServidor += 1;
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("ERRO:" + erro.Message);
                    }
                }

                #endregion

                #region copia as pasta do backup local

                try
                {
                    DirectoryInfo pasta = new DirectoryInfo(caminho);
                    string destino = @"C:\Documentos SDE\tmp\LOCAL_ZIP\" + pasta.Name.ToString();
                    Directory.CreateDirectory(destino);
                    DirectoryCopy(caminho, destino, true);
                    progressBar1.Value = i;
                }
                catch (Exception erro)
                {
                    MessageBox.Show("ERRO:" + erro.Message);
                }

                #endregion

                Application.DoEvents();
                status.Text = "Arquivos temporarios gerados";
            }
        }

        //faz a compactação de uma pasta
        public void Compactar(string origem, string destino)
        {
            Application.DoEvents();
            status.Text = "Compactando Arquivos...";
            progressBar1.Maximum += 1;
            if (File.Exists(destino))
                File.Delete(destino);
            ZipFile arquivo = new ZipFile(destino);
            arquivo.AddDirectory(origem);
            arquivo.Save();
            arquivo.Dispose();
            status.Text = "Enviando Arquivos para o Servidor...";
            progressBar1.Value += 1;
            //MessageBox.Show("Compactação concluida!", "Concluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //envia uma copia do backup para o servidor
        public void upload(string usuario, string senha, string origem, string destino)
        {

            WebClient cliente = new WebClient();
            NetworkCredential credenciais = new NetworkCredential(usuario, senha);
            cliente.Credentials = credenciais;
            try
            {
                Application.DoEvents();
                status.Text = "Enviando Arquivos...";
                cliente.UploadFile(destino, "STOR", origem);
                
                
                Application.DoEvents();
                status.Text = "Arquivos Enviados com Sucesso!";
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao tentar enviar o arquivo!" + "\n" + "Erro: " + erro.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        #endregion

        #region Botões

        private void Backup_Load(object sender, EventArgs e)
        {
            
            lerConfigBackup(@"C:\Documentos SDE\configBackup.xml");
        }
        
        private void Backup_Activated(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount < 1)
            {
                btnFazerBackup.Enabled = false;
                btnBackupLocal.Enabled = false;
                toolTip1.Show("Não existe nenhuma pasta configurada para fazer o Backup" + "\n" + "Contate o Suporte", this, 6000);
            }
        }

        private void btnBackupLocal_Click(object sender, EventArgs e)
        {
            data = saveFileDialog1.FileName = DateTime.Now.ToString("dd_MM_yyyy HH_mm_ss");
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != data)
            {
                txtBackup.Text = saveFileDialog1.FileName;
            }
        }

        private void btnFazerBackup_Click(object sender, EventArgs e)
        {
            string zipServidor;

            if (txtBackup.Text != "")
            {
                status.Visible = true;
                progressBar1.Visible = true;
   
                GeraTmp();

                //gera ao backup do cliente
                Compactar(@"C:\Documentos SDE\tmp\LOCAL_ZIP\", txtBackup.Text);

                //verifica se existe alguma pasta para compactar e enviar ao servidor
                if (nArqServidor > 0)
                {
                    Compactar(@"C:\Documentos SDE\tmp\SERVIDOR_ZIP\", zipServidor = @"C:\Documentos SDE\tmp\" + data + ".zip");
                    Application.DoEvents();
                    upload("Administrator", "paoc1710", zipServidor, "ftp://ftp.sistemadaempresa.com/backup/" + empresa + "/" + data + ".zip");
                }

                Application.DoEvents();
                status.Text = "Backup Concluído!";

                try
                {   //apaga o diretorio temporario (tmp)
                    Directory.Delete(@"C:\Documentos SDE\tmp\", true);
                    progressBar1.Value += 1;
                    MessageBox.Show("Backup efetuado com Sucesso!");
                }
                catch (Exception erro)
                {
                    MessageBox.Show("ERRO:" + erro.Message);
                }

                status.Visible = false;
                progressBar1.Visible = false;
            }
            else
            {
                toolTip1.Show("Selecione a pasta para salvar o backup", btnBackupLocal, 2000);
            }
        }

        #endregion

    }
}
