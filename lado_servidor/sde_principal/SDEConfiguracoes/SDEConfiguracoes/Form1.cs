using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using FTP;

namespace SDEConfiguracoes
{
    public partial class frmSDEConfig : Form
    {
        public string usuario;
        public funcoes func = new funcoes();
        public ftp FTP = new ftp();


        public frmSDEConfig()
        {
            InitializeComponent();
            func.downloadConfigBackup();
        }

        #region Funções

        /// <summary>
        /// Gera o XML com as Configurações do SDE.DESKTOP
        /// </summary>
        /// <param name="caminho">Caminho do XML de configurações</param>
        public void gerarXmlConfig(string caminho)
        {
            try
            {
                string xmlConf = caminho + "SDE_DesktopConfig.xml";
                if (!Directory.Exists(caminho))
                {
                    Directory.CreateDirectory(caminho);
                }
                if (File.Exists(xmlConf))
                {
                    File.SetAttributes(xmlConf, FileAttributes.Normal);
                }

                XmlTextWriter myXmlTextWriter = new XmlTextWriter(xmlConf, Encoding.UTF8);
                myXmlTextWriter.Formatting = Formatting.Indented;
                myXmlTextWriter.WriteStartDocument();
                myXmlTextWriter.WriteStartElement("SdeDesktop");
                myXmlTextWriter.WriteAttributeString("usuario", usuario);

                myXmlTextWriter.WriteStartElement("FrentCaixa");
                myXmlTextWriter.WriteAttributeString("Enabled", ckbFrentCaixa.Checked.ToString());
                myXmlTextWriter.WriteValue(txtFrentCaixa.Text);
                myXmlTextWriter.WriteEndElement();

                myXmlTextWriter.WriteStartElement("MapResum");
                myXmlTextWriter.WriteAttributeString("Enabled", ckbMapResum.Checked.ToString());
                myXmlTextWriter.WriteValue(txtMapResum.Text);
                myXmlTextWriter.WriteEndElement();

                myXmlTextWriter.WriteStartElement("MLoja");
                myXmlTextWriter.WriteAttributeString("Enabled", ckbMLoja.Checked.ToString());
                myXmlTextWriter.WriteValue(txtMLoja.Text);
                myXmlTextWriter.WriteEndElement();

                myXmlTextWriter.WriteStartElement("MSet");
                myXmlTextWriter.WriteAttributeString("Enabled", ckbMSet.Checked.ToString());
                myXmlTextWriter.WriteValue(txtMSet.Text);
                myXmlTextWriter.WriteEndElement();

                myXmlTextWriter.WriteStartElement("MultiTef");
                myXmlTextWriter.WriteAttributeString("Enabled", ckbMultitef.Checked.ToString());
                myXmlTextWriter.WriteValue(txtMultitef.Text);
                myXmlTextWriter.WriteEndElement();

                myXmlTextWriter.WriteStartElement("TefTyna");
                myXmlTextWriter.WriteAttributeString("Enabled", ckbTeftyna.Checked.ToString());
                myXmlTextWriter.WriteValue(txtTefTyna.Text);
                myXmlTextWriter.WriteEndElement();

                myXmlTextWriter.WriteStartElement("NFE");
                myXmlTextWriter.WriteAttributeString("Enabled", ckbNFE.Checked.ToString());
                myXmlTextWriter.WriteEndElement();

                myXmlTextWriter.WriteStartElement("SDE");
                myXmlTextWriter.WriteAttributeString("Enabled", ckbSde.Checked.ToString());
                myXmlTextWriter.WriteEndElement();

                myXmlTextWriter.WriteStartElement("SINTEGRA");
                myXmlTextWriter.WriteAttributeString("Enabled", ckbSintegra.Checked.ToString());
                myXmlTextWriter.WriteEndElement();

                myXmlTextWriter.WriteEndElement();
                myXmlTextWriter.Close();

                File.SetAttributes(caminho, FileAttributes.Hidden);


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.Message.ToString(), "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lê as configurações do gravadas no XML do backup
        /// </summary>
        /// <param name="configXMLBackup">Caminho do XML do backup</param>
        /// <param name="datagrid">Gride onde mostra as pasta selecionadas no XML</param>
        public void lerConfigBakup(string configXMLBackup, DataGridView dataGrid)
        {
            if (File.Exists(configXMLBackup))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(configXMLBackup);
                    XmlNodeList Xempresa = doc.GetElementsByTagName("Backup");
                    XmlNodeList Xpasta = doc.GetElementsByTagName("pasta");
                    txtIdCorp.Text = Xempresa[0].Attributes["Empresa"].InnerText;


                    for (int i = 0; i < Xpasta.Count; i++)
                    {
                        dataGrid.Rows.Add(Xpasta[i].InnerText, Xpasta[i].Attributes[0].InnerText);
                    }
                }
                catch (Exception erro)
                {
                    MessageBox.Show("ERRO: " + erro.Message);
                }
            }

        }

        /// <summary>
        /// Lê as Configurações do SDE.DESKTOP
        /// </summary>
        /// <param name="caminho">Caminho do XML</param>
        public void lerXmlConfig(string caminho)
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
                XmlNodeList xSINTEGRA = doc.GetElementsByTagName("SINTEGRA");
                XmlNodeList xTefTyna = doc.GetElementsByTagName("TefTyna");

                if (xFrentCaixa.Count > 0)
                {
                    txtFrentCaixa.Text = xFrentCaixa[0].InnerText;
                    ckbFrentCaixa.Checked = Convert.ToBoolean(xFrentCaixa[0].Attributes["Enabled"].InnerText);
                }
                if (xMapResum.Count > 0)
                {
                    txtMapResum.Text = xMapResum[0].InnerText;
                    ckbMapResum.Checked = Convert.ToBoolean(xMapResum[0].Attributes["Enabled"].InnerText);
                }
                if (xMLoja.Count > 0)
                {
                    txtMLoja.Text = xMLoja[0].InnerText;
                    ckbMLoja.Checked = Convert.ToBoolean(xMLoja[0].Attributes["Enabled"].InnerText);
                }
                if (xMSet.Count > 0)
                {
                    txtMSet.Text = xMSet[0].InnerText;
                    ckbMSet.Checked = Convert.ToBoolean(xMSet[0].Attributes["Enabled"].InnerText);
                }
                if (xMultiTef.Count > 0)
                {
                    txtMultitef.Text = xMultiTef[0].InnerText;
                    ckbMultitef.Checked = Convert.ToBoolean(xMultiTef[0].Attributes["Enabled"].InnerText);
                }
                if (xNFE.Count > 0)
                {
                    ckbNFE.Checked = Convert.ToBoolean(xNFE[0].Attributes["Enabled"].InnerText);
                }
                if (xSDE.Count > 0)
                {
                    ckbSde.Checked = Convert.ToBoolean(xSDE[0].Attributes["Enabled"].InnerText);
                }
                if (xSINTEGRA.Count > 0)
                {
                    ckbSintegra.Checked = Convert.ToBoolean(xSINTEGRA[0].Attributes["Enabled"].InnerText);
                }
                if (xTefTyna.Count > 0)
                {
                    txtTefTyna.Text = xTefTyna[0].InnerText;
                    ckbTeftyna.Checked = Convert.ToBoolean(xTefTyna[0].Attributes["Enabled"].InnerText);
                }
            }
        }

        /// <summary>
        /// Salva em um XML as configurações do SDE.DESKTOP
        /// </summary>
        public void salvarConfiguracoes()
        {
            func.idCorp = txtIdCorp.Text;

            if (txtFrentCaixa.Text == "")
                ckbFrentCaixa.Checked = false;

            if (txtMapResum.Text == "")
                ckbMapResum.Checked = false;

            if (txtMLoja.Text == "")
                ckbMLoja.Checked = false;

            if (txtMSet.Text == "")
                ckbMSet.Checked = false;

            if (txtMultitef.Text == "")
                ckbMultitef.Checked = false;

            if (txtTefTyna.Text == "")
                ckbTeftyna.Checked = false;

            ProgressBarConfig.Value = 0;

            ProgressBarConfig.Visible = true;
            status.Visible = true;

            gerarXmlConfig(@"C:\Documentos SDE\");
            
            ProgressBarConfig.Value += 1;
            status.Text = "Salvando Configurações...";
            Application.DoEvents();

            func.geraXmlBackup(@"C:\Documentos SDE\configBackup.xml",dataGridView1);
            ProgressBarConfig.Value += 1;

            FTP.upload("Administrator", "paoc1710", @"C:\Documentos SDE\configBackup.xml", "ftp://ftp.sistemadaempresa.com/backup/" + txtIdCorp.Text + "/configBackup.xml");
            
            status.Text = "Configurações Salvas com Sucesso!";
            Application.DoEvents();
            ProgressBarConfig.Value += 1;

            System.Threading.Thread.Sleep(1000);

            ProgressBarConfig.Visible = false;
            status.Visible = false;

            MessageBox.Show("Configurações salvas com sucesso!" + "\n" + "Feche o SDE Desktop e o abra novamente.", "Painel de Configurações", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region StripMenu

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Excluir pasta?" + "\n" + dataGridView1.CurrentCell.Value.ToString(), "Painel de Configurações", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
            }
        }

        private void trueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value = "True";
        }

        private void falseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value = "False";
        }

        #endregion

        #region Caminhos do aplicativos

        private void btFrentCaixa_Click(object sender, EventArgs e)
        {
            func.opDialog("Frente de Caixa", txtFrentCaixa);
        }

        private void btMapResum_Click(object sender, EventArgs e)
        {
            func.opDialog("Mapa Resumo", txtMapResum);
        }

        private void btMLoja_Click(object sender, EventArgs e)
        {
            func.opDialog("Multisoft Loja", txtMLoja);
        }

        private void btMSet_Click(object sender, EventArgs e)
        {
            func.opDialog("Multisoft SET", txtMSet);
        }

        private void btMultitef_Click(object sender, EventArgs e)
        {
            func.opDialog("MultiTef", txtMultitef);
        }

        private void btTeftyna_Click(object sender, EventArgs e)
        {
            func.opDialog("TefTyna", txtTefTyna);
        }
        #endregion

        #region Edição dos BATs
        
        private void txtFrentCaixa_DoubleClick(object sender, EventArgs e)
        {
            func.editarBAT(txtFrentCaixa);
        }

        private void txtMapResum_DoubleClick(object sender, EventArgs e)
        {
            func.editarBAT(txtMapResum);
        }

        private void txtMLoja_DoubleClick(object sender, EventArgs e)
        {
            func.editarBAT(txtMLoja);
        }

        private void txtMSet_DoubleClick(object sender, EventArgs e)
        {
            func.editarBAT(txtMSet);
        }

        private void txtMultitef_DoubleClick(object sender, EventArgs e)
        {
            func.editarBAT(txtMultitef);
        }

        private void txtTefTyna_DoubleClick(object sender, EventArgs e)
        {
            func.editarBAT(txtTefTyna);
        }
        #endregion

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }

        private void frmSDEConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("Deseja salvar as modificações?", "Painel de Configurações", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                salvarConfiguracoes();
            }
            Application.Exit();
        }

        private void frmSDEConfig_Load(object sender, EventArgs e)
        {
            lerConfigBakup(@"C:\Documentos SDE\configBackup.xml", dataGridView1);
            lerXmlConfig(@"C:\Documentos SDE\SDE_DesktopConfig.xml");
        }

        private void btnAddPasta_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog BrowserDialog = new FolderBrowserDialog();
            BrowserDialog.ShowDialog();
            if (BrowserDialog.SelectedPath != "")
            {
                dataGridView1.Rows.Add(BrowserDialog.SelectedPath, copiaServidor.Checked.ToString());
            }
            BrowserDialog.SelectedPath = "";
       }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtIdCorp.Text != "")
            {
                salvarConfiguracoes();
            }
            else 
            {
                ToolTip toolTip = new ToolTip();
                toolTip.IsBalloon = true;

                toolTip.Show("Selecione a pasta para salvar o backup", label9, 2000);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
