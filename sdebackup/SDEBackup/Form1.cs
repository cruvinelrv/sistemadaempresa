using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FTP;


namespace SDEBackup
{
    public partial class frmSDEBackup : Form
    {
        public funcoes func = new funcoes();
        public ftp FTP = new ftp();
        public string data, empresa;
        public int nArqServidor;

        public frmSDEBackup()
        {
            InitializeComponent();
            func.downloadConfigBackup();
        }
        
        private void frm_Load(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(@"C:\Documentos SDE\tmp\"))
                {
                    Directory.Delete(@"C:\Documentos SDE\tmp\", true);
                }

                func.lerConfigBackup(@"C:\Documentos SDE\configBackup.xml", dataGridView1);

                empresa = func.empresa.ToString();
            }
            catch (Exception ex)
            {
                //
            }
        }

        /// <summary>
        /// Gera estrutura de pastas temporarias
        /// </summary>
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

            status.Text = "Gerando arquivos temporarios...";
            Application.DoEvents();
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
                        func.DirectoryCopy(caminho, destino, true);
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
                    func.DirectoryCopy(caminho, destino, true);
                    progressBar1.Value = i;
                }
                catch (Exception erro)
                {
                    MessageBox.Show("ERRO:" + erro.Message);
                }

                #endregion

                status.Text = "Arquivos temporarios gerados";
                Application.DoEvents();
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

                status.Text = "Compactando arquivo(s)...";
                Application.DoEvents();

                func.Compactar(@"C:\Documentos SDE\tmp\LOCAL_ZIP\", txtBackup.Text);

                if (nArqServidor > 0)
                {
                    
                    status.Text = "Compactando arquivo(s) para o servidor...";
                    Application.DoEvents();

                    func.Compactar(@"C:\Documentos SDE\tmp\SERVIDOR_ZIP\", zipServidor = @"C:\Documentos SDE\tmp\" + data + ".zip");

                    status.Text = "Enviando arquivo(s) para o servidor...";
                    Application.DoEvents();

                    FTP.upload("Administrator", "paoc1710", zipServidor, "ftp://ftp.sistemadaempresa.com/backup/" + empresa + "/" + data + ".zip");
                }

                status.Text = "Backup Concluído!";
                Application.DoEvents();

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

        private void toolStripSplitButton1_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }

        
    }
}
