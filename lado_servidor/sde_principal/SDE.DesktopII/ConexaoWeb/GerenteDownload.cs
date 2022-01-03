using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace SDE.Desktop.ConexaoWeb
{
    class GerenteDownload
    {
        public void BaixaArquivo(string file_url, string file_path, bool executar)
        {
            string folder_path = Path.GetDirectoryName(file_path);

            System.Windows.Forms.MessageBox.Show("vai baixar: " + file_url);
            System.Windows.Forms.MessageBox.Show("e salvar na pasta: " + folder_path);
            System.Windows.Forms.MessageBox.Show("com nome: " + file_path);

            if (!Directory.Exists(folder_path))
                Directory.CreateDirectory(folder_path);

            WebClient webClient = new WebClient();
            webClient.DownloadFile(file_url, file_path);

            if (executar)
                Process.Start(file_path);
        }
    }
}
