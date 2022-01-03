using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using Ionic.Utils.Zip;
using FTP;


namespace SDEBackup
{
   public class funcoes
    {
        public string empresa, data;
        public int nArqServidor = 0;

       /// <summary>
       /// Lê as configurações do gravadas no XML do backup
       /// </summary>
       /// <param name="configXMLBackup">Caminho do XML do backup</param>
       /// <param name="datagrid">Gride onde mostra as pasta selecionadas no XML</param>
        public void lerConfigBackup(string configXMLBackup, DataGridView datagrid)
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
                        datagrid.Rows.Add(Xpasta[i].InnerText, Xpasta[i].Attributes[0].InnerText);
                    }
                }
                catch (Exception erro)
                {
                    MessageBox.Show("ERRO: " + erro.Message);
                }
            }

        }

        /// <summary>
        /// Copia a pasta com seus arquivos e/ou subdiretorios
        /// </summary>
        /// <param name="sourceDirName">Caminho da Pasta de Origem</param>
        /// <param name="destDirName">Caminho do Destino Final</param>
        /// <param name="copySubDirs">Copiar Subdiretorios</param>
        public void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
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

        /// <summary>
        /// Compacta uma pasta
        /// </summary>
        /// <param name="origem">Caminho da pasta orignal</param>
        /// <param name="destino">Caminho de saida do arquivo compactado</param>
        public void Compactar(string origem, string destino)
        {
            try
            {
                if (File.Exists(destino))
                File.Delete(destino);
                ZipFile arquivo = new ZipFile(destino);
                arquivo.AddDirectory(origem);
                arquivo.Save();
                arquivo.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show("Erro:\n" + err.Message,"ATENÇÂO",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Faz o Download das configurações das pasta de backup do Servidor.
        /// Isso se ja foi configurado corretamente local.
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
