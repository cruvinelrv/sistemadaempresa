using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using SDE.Desktop;

namespace SDE.Desktop
{
    public partial class SDEConfig : Form
    {
        #region Variaveis

        public string caminho = (@"C:\Documentos SDE\SDE_DesktopConfig.xml");
        public string usuario;

        #endregion

        public SDEConfig()
        {
            InitializeComponent();
        }

        #region Funções

        public void opDialog(string title, TextBox sender)
        {
            openFileDialog1.Title = title;

            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                sender.Text = openFileDialog1.FileName;
            }
        }

        public void gerarXmlConfig()
        {
            try
            {

                if (!Directory.Exists(@"C:\Documentos SDE\"))
                {
                    Directory.CreateDirectory(@"C:\Documentos SDE\");
                }
                if (File.Exists(caminho))
                {
                    File.SetAttributes(caminho, FileAttributes.Normal);
                }

                XmlTextWriter myXmlTextWriter = new XmlTextWriter(caminho, Encoding.UTF8);
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

        public void geraXmlBackup()
        {
            string caminho = (@"C:\Documentos SDE\configBackup.xml");
            int cont = dataGridView1.Rows.Count;

            if (!(File.Exists(caminho)))
            {
                File.Create(caminho).Close();
            }

            File.SetAttributes(caminho, FileAttributes.Normal);

            XmlTextWriter myXmlTextWriter = new XmlTextWriter(caminho, Encoding.UTF8);
            myXmlTextWriter.Formatting = Formatting.Indented;
            myXmlTextWriter.WriteStartDocument();
            myXmlTextWriter.WriteStartElement("Backup");
            myXmlTextWriter.WriteAttributeString("Empresa", txtIdCorp.Text);

            for (int i = 0; i < cont; i++)
            {
                string pasta = dataGridView1.Rows[i].Cells[0].Value.ToString();
                try
                {
                    myXmlTextWriter.WriteStartElement("pasta");
                    myXmlTextWriter.WriteAttributeString("servidor", dataGridView1.Rows[i].Cells[1].Value.ToString());
                    myXmlTextWriter.WriteString(pasta);
                    myXmlTextWriter.WriteEndElement();
                }
                catch (Exception erro)
                {
                    MessageBox.Show("ERRO:" + erro.Message);
                }
            }

            myXmlTextWriter.WriteEndElement();
            myXmlTextWriter.Close();

            File.SetAttributes(caminho, FileAttributes.Hidden);
        }

        public void lerConfigBakup(string configXMLBackup)
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
                    dataGridView1.Rows.Add(Xpasta[i].InnerText, Xpasta[i].Attributes[0].InnerText);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("ERRO: " + erro.Message);
            }
            }

        }

        public void salvarConfiguracoes()
        {
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

            gerarXmlConfig();
            Application.DoEvents();
            ProgressBarConfig.Value += 1;
            status.Text = "Salvando Configurações...";

            geraXmlBackup();
            Application.DoEvents();
            ProgressBarConfig.Value += 1;

            Backup bkp = new Backup();
            bkp.upload("Administrator", "paoc1710", @"C:\Documentos SDE\configBackup.xml", "ftp://ftp.sistemadaempresa.com/backup/" + txtIdCorp.Text + "/configBackup.xml");
            Application.DoEvents();
            ProgressBarConfig.Value += 1;
            status.Text = "Configurações Salvas com Sucesso!";

            System.Threading.Thread.Sleep(1000);

            ProgressBarConfig.Visible = false;
            status.Visible = false;

            MessageBox.Show("Configurações salvas com sucesso!"+"\n"+"Feche o SDE Desktop e o abra novamente.", "Painel de Configurações", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void editarBAT(TextBox txt)
        {
            System.Diagnostics.Process.Start("notepad.exe", txt.Text);
        }

        #endregion

        #region Caminhos do aplicativos

        private void btFrentCaixa_Click(object sender, EventArgs e)
        {
            opDialog("Frente de Caixa", txtFrentCaixa);
        }

        private void btMapResum_Click(object sender, EventArgs e)
        {
            opDialog("Mapa Resumo ECF", txtMapResum);
        }

        private void btMLoja_Click(object sender, EventArgs e)
        {
            opDialog("Multisoft Loja", txtMLoja);
        }

        private void btMSet_Click(object sender, EventArgs e)
        {
            opDialog("Multisoft Set", txtMSet);
        }

        private void btMultitef_Click(object sender, EventArgs e)
        {
            opDialog("MultiTEF", txtMultitef);
        }

        private void btTeftyna_Click(object sender, EventArgs e)
        {
            opDialog("Tef Tyna", txtTefTyna);
        }

        #endregion

        #region Botões

        private void btnAddPasta_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath != "")
            {
                dataGridView1.Rows.Add(folderBrowserDialog1.SelectedPath, copiaServidor.Checked.ToString());
            }
            folderBrowserDialog1.SelectedPath = "";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            salvarConfiguracoes();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region StripMenu

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Excluir pasta?"+"\n"+dataGridView1.CurrentCell.Value.ToString(), "Painel de Configurações", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        private void SDEConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("Deseja salvar as modificações?", "Painel de Configurações", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                salvarConfiguracoes();
            }

        }

        private void SDEConfig_Load(object sender, EventArgs e)
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


                txtFrentCaixa.Text = xFrentCaixa[0].InnerText;
                ckbFrentCaixa.Checked = Convert.ToBoolean(xFrentCaixa[0].Attributes["Enabled"].InnerText);

                txtMapResum.Text = xMapResum[0].InnerText;
                ckbMapResum.Checked = Convert.ToBoolean(xMapResum[0].Attributes["Enabled"].InnerText);

                txtMLoja.Text = xMLoja[0].InnerText;
                ckbMLoja.Checked = Convert.ToBoolean(xMLoja[0].Attributes["Enabled"].InnerText);

                txtMSet.Text = xMSet[0].InnerText;
                ckbMSet.Checked = Convert.ToBoolean(xMSet[0].Attributes["Enabled"].InnerText);

                txtMultitef.Text = xMultiTef[0].InnerText;
                ckbMultitef.Checked = Convert.ToBoolean(xMultiTef[0].Attributes["Enabled"].InnerText);

                ckbNFE.Checked = Convert.ToBoolean(xNFE[0].Attributes["Enabled"].InnerText);

                ckbSde.Checked = Convert.ToBoolean(xSDE[0].Attributes["Enabled"].InnerText);
                if (xSINTEGRA.Count > 0)
                {
                    ckbSintegra.Checked = Convert.ToBoolean(xSINTEGRA[0].Attributes["Enabled"].InnerText);
                }
                txtTefTyna.Text = xTefTyna[0].InnerText;
                ckbTeftyna.Checked = Convert.ToBoolean(xTefTyna[0].Attributes["Enabled"].InnerText);
            }

            lerConfigBakup(@"C:\Documentos SDE\configBackup.xml");
        }

        #region Editar BATs

        private void txtMLoja_DoubleClick(object sender, EventArgs e)
        {
            editarBAT(txtMLoja);
        }

        private void txtMapResum_DoubleClick(object sender, EventArgs e)
        {
            editarBAT(txtMapResum);
        }

        private void txtMSet_DoubleClick(object sender, EventArgs e)
        {
            editarBAT(txtMSet);
        }

        private void txtMultitef_DoubleClick(object sender, EventArgs e)
        {
            editarBAT(txtMultitef);
        }

        private void txtTefTyna_DoubleClick(object sender, EventArgs e)
        {
            editarBAT(txtTefTyna);
        }

        #endregion

    }
}
