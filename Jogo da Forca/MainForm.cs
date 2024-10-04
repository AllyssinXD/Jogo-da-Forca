/*
 * Created by SharpDevelop.
 * User: aliss
 * Date: 03/10/2024
 * Time: 12:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Diagnostics;
using Utils;

namespace Jogo_da_Forca
{

	public partial class MainForm : Form
	{
		List<Button> botoesColoridos = new List<Button>();
		List<Button> botoesTeclado;
		
		Jogo jogo;
		
		
		public MainForm()
		{
			InitializeComponent();
			jogo = new Jogo();
			
			botoesTeclado = new List<Button>
			{
			    button_A, button_B, button_C, button_D, button_E,
			    button_F, button_G, button_H, button_I, button_J,
			    button_K, button_L, button_M, button_N, button_O,
			    button_P, button_Q, button_R, button_S, button_T,
			    button_U, button_V, button_W, button_X, button_Y, button_Z
			};
			
		}
		
		void teclaPressionada(char caractere, Button botao){
			bool achou = jogo.procuraLetra(caractere);
			if(achou){
				botao.BackColor = Color.Green;
			}
			else{
				botao.BackColor = Color.Red;
				listBox2.Items.Add(caractere+",");
			}
			AtualizaDados();
			botoesColoridos.Add(botao);
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			jogo.setEstado(Estado.PARADO);
			AtualizaDados();

			for(int i =0; i<botoesColoridos.Count; i++){
				botoesColoridos[i].BackColor = Color.Transparent;
			}
			
			botoesColoridos.Clear();
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			FormHelpers.mudarVisibilidade(false,label1,label2,label3,label4,label5,label6,label7,
			                  panel2,listBox2,textBox1,panel1,button3,button2);
			FormHelpers.mudarVisibilidade1<Button>(false, botoesTeclado);
			
			button1.Visible = true;
			
			label1.Text = "Digite uma palavra:";
			
			textBox1.MaxLength = 3000;
			listBox2.Items.Clear();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			if(jogo.getEstado() == Estado.PARADO){
				jogo.ComecaJogo(textBox1.Text);
				ComecaJogo();
			} else {
				if (textBox1.Text.ToUpper() == jogo.getPalavra()){
					jogo.Ganhar();
				} else {
					jogo.Perder();
				} 
				AtualizaDados();
			}
		}

		void ButtonKeyboardClick(object sender, EventArgs e)
		{
			Button botao = sender as Button;
			
			if(botao != null){
				char letra = botao.Name.Last();
				teclaPressionada(letra, botao);
			}
		}
		
		void AtualizaDados(){
			label6.Text = jogo.getTentativas().ToString();
			label2.Text = jogo.getDescobertas();
			
			if(jogo.getEstado() == Estado.VITORIA){
				label3.Text = "Parabéns! A palavra era " + jogo.getPalavra();
			}
			else if(jogo.getEstado() == Estado.DERROTA){
				label3.Text = "Errou! A palavra era " + jogo.getPalavra();
			} else if (jogo.getEstado() == Estado.PARADO){
				FormHelpers.mudarVisibilidade(true,label1,textBox1,button3);
				FormHelpers.mudarVisibilidade(false,button1);
				FormHelpers.mudarAtivado(true, button3, textBox1);
				button3.Text = "CONFIRMAR";
			}
			
			if(jogo.getEstado() != Estado.OCORRENDO && jogo.getEstado() != Estado.PARADO){
				FormHelpers.mudarAtivado1(false, botoesTeclado);
				FormHelpers.mudarAtivado(false, button3, textBox1);
				button2.Visible = true;
			}
		}
		
		void ComecaJogo(){
			AtualizaDados();
			label3.Text = "A Palavra tem " + jogo.getPalavra().Length + " letras";
			label1.Text = "palpite:";
			
			FormHelpers.mudarVisibilidade(true,label2,label4,label5,panel2,listBox2,label3,
			                  button2,panel1,label6,label7);
			
			FormHelpers.mudarVisibilidade1<Button>(true, botoesTeclado);
			FormHelpers.mudarAtivado1<Button>(true, botoesTeclado);
		
			textBox1.Text = "";
			textBox1.MaxLength = jogo.getPalavra().Length;
			
			
			button3.Text = "CHUTAR";
		}
	}
}
