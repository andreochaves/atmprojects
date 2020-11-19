﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lets_Musics
{
    public partial class Cadastro_Tela : Form
    {
        string contas_File = File.ReadAllText("../../Contas.txt");
        ControladorContas controlador = new ControladorContas();
        string nome, senha, email;

        public Cadastro_Tela()
        {
            InitializeComponent();
        }

        private void Cadastro_Tela_Load(object sender, EventArgs e)
        {
            
        }

        private void Cadastro_Tela_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (this.DialogResult == DialogResult.Cancel)
            {
                 Program.tela_login.Close();
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.tela_login = new Login_Tela();
            Program.tela_login.Show();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            nome = txtNome.Text;
            senha = txtSenha.Text;
            email = txtEmail.Text;
            bool x = false;
            foreach(Conta c in controlador.GetContas())
            {
            	if(c.Email == email)
            	{
            		x = true;
            	}
            }
            if (!x)
            {
                controlador.GetContas().Add(new ContaNormal(nome, senha, email));
                Conta ct = controlador.GetContas().Last();
                contas_File += "}" + '\n' + "Codigo:" + ct.Codigo + ",\n" + "TipoConta:" + ct.Tipo_Conta + ",\n" + "Nome:" + ct.Nome + ",\n" + "Senha:" + ct.Senha + ",\n" + "Email:" + ct.Email + ",\n" + "Possui_Banda:" + ct.PossuiBanda + '\n';
                File.WriteAllText("../../Contas.txt", contas_File);
                MessageBox.Show("Cadastrado");
                this.Close();
                Program.tela_login.Show();
            }
            else
            {
                MessageBox.Show("Email já Cadastrado.");
            }
        }
    }
}
