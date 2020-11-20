﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lets_Musics
{
    class ControladorContas
    {
        public static List<Conta> ListaGeralContas = new List<Conta>();
        public static Conta Atual;
		
        public ControladorContas(){}
        public List<Conta> GetContas()
        {
            return ListaGeralContas;
        }
        public Conta getAtual()
        {
            return Atual;
        }
        public void setAtual(Conta c)
        {
            Atual = c;
        }
        public void AtualizarContas()
        {
        	LimparContas();
        	ListaGeralContas = new List<Conta>();
        	CarregarContas();
        }
        public void LimparContas()
        {
        	ListaGeralContas = null;
        }
        public void CarregarContas()
        {
        	string contas_File = File.ReadAllText("../../Contas.txt");
            string[] load_contas = contas_File.Split('}');

            string nome ="", senha="", email="",musicapath="",playlists="";
            int tipo = 0;
            bool pBanda = false;

            foreach (string k in load_contas)
            {
                
                int count = 0;
                string[] conta_info = k.Split(',');
                foreach (string j in conta_info)
                {
                    switch (count)
                    {
                        case 1:
                            tipo = int.Parse(j.Split(':')[1]);
                            break;
                        case 2:
                            nome = j.Split(':')[1];
                            break;
                        case 3:
                            senha = j.Split(':')[1];
                            break;
                        case 4:
                            email = j.Split(':')[1];
                            break;
                        case 5:
                            pBanda = bool.Parse(j.Split(':')[1]);
                            break;
                        case 6:
                            musicapath = j.Split(':')[1];
                            break;
                        case 7:
                            playlists = j.Split(':')[1];
                            break;
                        
                    }
                    count++;
                }
                if (tipo == 0)
                {
                    ListaGeralContas.Add(new ContaNormal(nome, senha, email,musicapath));
                }
                else
                {
                    ListaGeralContas.Add(new ContaPremium(nome, senha, email, musicapath));
                }
                ListaGeralContas.Last().PossuiBanda = pBanda;
                ListaGeralContas.Last().playlistscodes = playlists;
            }
        }
    }
    
}
