using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using FTP;

namespace SDEConfiguracoes
{
    public class funcoes
    {
        public string idCorp;
        /// <summary>
        /// Executa o OpenFileDialog
        /// </summary>
        /// <param name="title">Titulo do OpenFileDialog</param>
        /// <param name="txt">TextBox que recebe o filename</param>
        public void opDialog(string title, TextBox txt)
        {

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Aplicativos | *.exe; *.bat";
            openFile.Title = title;

            openFile.ShowDialog();
            if (openFile.FileName != "")
            {
                txt.Text = openFile.FileName;
            }
        }

        /// <summary>
        /// Gera o XML com as pastas para fazer o backup
        /// </summary>
        /// <param name="caminho">Caminho do XML de backup</param>
        /// <param name="dataGrid">Grid onde sera adiconado as pastas e configuracoes</param>
        public void geraXmlBackup(string caminho, DataGridView dataGrid)
        {
            //string caminho = (@"C:\Documentos SDE\configBackup.xml");
            int cont = dataGrid.Rows.Count;

            if (!(File.Exists(caminho)))
            {
                File.Create(caminho).Close();
            }

            File.SetAttributes(caminho, FileAttributes.Normal);

            XmlTextWriter myXmlTextWriter = new XmlTextWriter(caminho, Encoding.UTF8);
            myXmlTextWriter.Formatting = Formatting.Indented;
            myXmlTextWriter.WriteStartDocument();
            myXmlTextWriter.WriteStartElement("Backup");
            myXmlTextWriter.WriteAttributeString("Empresa", idCorp);

            for (int i = 0; i < cont; i++)
            {
                string pasta = dataGrid.Rows[i].Cells[0].Value.ToString();
                try
                {
                    myXmlTextWriter.WriteStartElement("pasta");
                    myXmlTextWriter.WriteAttributeString("servidor", dataGrid.Rows[i].Cells[1].Value.ToString());
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
        
        /// <summary>
        /// Abre o notepad
        /// </summary>
        /// <param name="txt">TextBox com o caminho do bat</param>
        public void editarBAT(TextBox txt)
        {
            System.Diagnostics.Process.Start("notepad.exe", txt.Text);
        }

        /// <summary>
        /// Faz o download do XML de configurações do Backup
        /// </summary>
        public void downloadConfigBackup()
        {

            if (File.Exists(@"C:\Documentos SDE\configBackup.xml"))
            {
                File.SetAttributes(@"C:\Documentos SDE\configBackup.xml", FileAttributes.Normal);

                XmlDocument doc = new XmlDocument();
                doc.Load(@"C:\Documentos SDE\configBackup.xml");
                XmlNodeList Xempresa = doc.GetElementsByTagName("Backup");
                string idCorp = Xempresa[0].Attributes["Empresa"].InnerText;

                try
                {
                    string configBk = ("ftp://ftp.sistemadaempresa.com/backup/" + idCorp + "/configBackup.xml");
                    ftp FTP = new ftp();
                    FTP.download("Administrator", "paoc1710", configBk, @"C:\Documentos SDE\configBackup.xml");

                    File.SetAttributes(@"C:\Documentos SDE\configBackup.xml", FileAttributes.Hidden);
                }
                catch (Exception err)
                {
                    //MessageBox.Show("Erro Download:\n"+err.Message);
                }
            }
        }

       
    }
}
