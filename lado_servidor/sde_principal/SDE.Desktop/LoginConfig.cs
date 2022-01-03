using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using SDE.Desktop;

namespace SDE.Desktop
{
    public partial class LoginConfig : Form
    {
        public LoginConfig()
        {
            InitializeComponent();
        }

        public static bool login(string usuario, string senha)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("http://www.sistemadaempresa.com.br/programas/Desktop/SDE_DesktopUser.xml");
            XmlNodeList xUsuario = doc.GetElementsByTagName("usuario");
            XmlNodeList xSenha = doc.GetElementsByTagName("senha");


            int n = xUsuario.Count;
            int i = 0;
            bool resultado = false;
            while (i < n)
            {
                if ((xUsuario[i].InnerText == usuario) && (xSenha[i].InnerText == senha))
                {
                    resultado = true;
                    break;
                }
                else
                {
                    resultado = false;
                }

                i++;
            }
            return (resultado);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (login(textBox1.Text, textBox2.Text) == true)
            {
                SDEConfig frmConfig = new SDEConfig();
                frmConfig.usuario = textBox1.Text;
                Close();
                frmConfig.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("Usuário e/ou Senha Incorreto(s)!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoginConfig_Activated(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (login(textBox1.Text, textBox2.Text) == true)
                {
                    SDEConfig frmConfig = new SDEConfig();
                    frmConfig.usuario = textBox1.Text;
                    Hide();
                    frmConfig.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Usuário e/ou Senha Incorreto(s)!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } 
            }
        }

    }
}
