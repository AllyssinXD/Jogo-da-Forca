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

namespace Jogo_da_Forca
{

	public partial class MainForm : Form
	{
		int tentativas = 6;
		string palavra = "__";
		bool button4 = false;
		List<Button> botoesColoridos = new List<Button>();
		List<Button> botoesTeclado;
		
		
		public MainForm()
		{
			InitializeComponent();
			botoesTeclado = new List<Button>
			{
			    button_A, button_B, button_C, button_D, button_E,
			    button_F, button_G, button_H, button_I, button_J,
			    button_K, button_L, button_M, button_N, button_O,
			    button_P, button_Q, button_R, button_S, button_T,
			    button_U, button_V, button_W, button_X, button_Y, button_Z
			};
		}
		
		void mudarVisibilidade(bool visibilidade, params object[] objs){
			foreach (Control obj in objs){
				obj.Visible = visibilidade;
			}
		}
		
		void mudarVisibilidade1<T>(bool visibilidade, IEnumerable<T> objs) where T : Control{
			foreach (T obj in objs){
				obj.Visible = visibilidade;
			}
		}
		
		void mudarAtivado(bool ativado, params object[] objs){
			foreach (Control obj in objs){
				obj.Enabled = ativado;
			}
		}
		void mudarAtivado1<T>(bool ativado, IEnumerable<T> objs) where T : Control{
			foreach (T obj in objs){
				obj.Enabled = ativado;
			}
		}
		
		void teclaPressionada(char caractere, Button botao){
			bool caractereEncontrado = false;
			botao.Enabled = false;
			
			// Loop sobre todas as letras na palavra escolhida
			for(int i=0; i<palavra.Length; i++){
				/*
 				  * Se encontra a letra na palavra:
				  * - Muda a cor do botão
				  * - Adiciona letra entre as descobertas
				 */
				if(palavra[i] == caractere){
					caractereEncontrado = true;
					
					botao.BackColor = Color.Green;
					
					char[] charsDescobertas = label2.Text.ToCharArray();
					
					//Condições necessárias para calcular onde o caractere
					//descoberto há de aparecer. (Pular os espaços e substituir apenas o "_")
					if (i == 0) charsDescobertas[i] = caractere;
					else charsDescobertas[i+i] = caractere;
					
					label2.Text = new string(charsDescobertas);
				}
			}
						
			//Caractere não encontrado :
			if (!caractereEncontrado){
				botao.BackColor = Color.Red;
				listBox2.Items.Add(caractere+",");
				tentativas = tentativas - 1;
				label6.Text = tentativas.ToString();
			}
			
			botoesColoridos.Add(botao);
			
			//Verifica se ainda tem letras a serem descobertas
			if(!label2.Text.Contains("_")){
				label3.Text = "Parabéns! A palavra era " + palavra;
				mudarAtivado(false, textBox1, button3);
				mudarAtivado1<Button>(false, botoesTeclado);
			}
			
			//A cada Letra no Teclado Apertada, Verificar se as chances acabaram :
			if (tentativas == 0) {
				mudarAtivado1<Button>(false, botoesTeclado);
				label3.Text = "você ficou sem tentivas, chute a palavra";	
			}
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			mudarVisibilidade(true,label1,textBox1,button3);
			mudarVisibilidade(false,button1);
			mudarAtivado(true, button3, textBox1);
			
			button3.Text = "CONFIRMAR";

			for(int i =0; i<botoesColoridos.Count; i++){
				botoesColoridos[i].BackColor = Color.Transparent;
			}
			botoesColoridos.Clear();
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			mudarVisibilidade(false,label1,label2,label3,label4,label5,label6,label7,
			                  panel2,listBox2,textBox1,panel1,button3,button2);
			mudarVisibilidade1<Button>(false, botoesTeclado);
			
			button1.Visible = true;
			
			label1.Text = "Digite uma palavra:";
			
			textBox1.MaxLength = 3000;
			listBox2.Items.Clear();

			tentativas = 6;
			
			button4 = false;
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			
			if(button4 == false){
				palavra = textBox1.Text.ToUpper();
				label2.Text = (new string('_', palavra.Length)).Replace("_","_ ");
				label3.Text = "A Palavra tem " + textBox1.Text.Length + " letras";
				label1.Text = "palpite:";
				
				mudarVisibilidade(true,label2,label4,label5,panel2,listBox2,label3,
				                  button2,panel1,label6,label7);
				
				mudarVisibilidade1<Button>(true, botoesTeclado);
				mudarAtivado1<Button>(true, botoesTeclado);
			
				textBox1.Text = "";
				textBox1.MaxLength = palavra.Length;
				
				
				button3.Text = "CHUTAR";
				button4 = true;
				
			} else {
				if (textBox1.Text.ToUpper() == palavra){
					label3.Text = "Parabéns! A palavra era " + palavra;
					button2.Visible = true;
				} else {
					label3.Text = "Errou! A palavra era " + palavra;
					button2.Visible = true;
					
				}
				button3.Enabled = false;
				textBox1.Enabled = false;
				mudarVisibilidade1<Button>(false, botoesTeclado);
				label2.Text = palavra;
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
	}
}
