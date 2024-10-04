/*
 * Created by SharpDevelop.
 * User: aliss
 * Date: 04/10/2024
 * Time: 09:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Utils;

public enum Estado {OCORRENDO, VITORIA, DERROTA, PARADO}

public class Jogo{
	
	private Estado estado;
	private string palavra;
	private string descobertas;
	private int tentativas;
	
	public Jogo(){
		
		estado = Estado.PARADO;
		palavra = "";
		tentativas = 0;
		descobertas = "";
	}
	
	public void ComecaJogo(string palavra){
		estado = Estado.OCORRENDO;
		setPalavra(palavra.ToUpper());
		comecaDescobertas();
		setTentativas(6);
	}
	
	public Estado getEstado(){
		return estado;
	}
	
	public void setEstado(Estado e){
		estado = e;
	}
	
	public string getPalavra(){
		return palavra;
	}
	
	public void setPalavra(string e){
		palavra = e;
	}
	
	public string getDescobertas(){
		return descobertas;
	}
	
	public void setDescobertas(string e){
		descobertas = e;
	}
	
	public int getTentativas(){
		return tentativas;
	}
	
	public void setTentativas(int i){
		tentativas = i;
	}
	
	void AtualizaDescobertas(int i, char caractere){
		char[] charsDescobertas = getDescobertas().ToCharArray();

		if (i == 0) charsDescobertas[i] = caractere;
		else charsDescobertas[i+i] = caractere;
		
		setDescobertas(new string(charsDescobertas));
		
		if(!descobertas.Contains("_") && estado == Estado.OCORRENDO){
			Ganhar();
		}
	}
	
	public bool procuraLetra(char letra){
		bool caractereEncontrado = false;
		
		for(int i=0; i<palavra.Length; i++){
			if(palavra[i] == letra){
				caractereEncontrado = true;
				AtualizaDescobertas(i, letra);
			}
		}
					
		if (!caractereEncontrado){
			setTentativas(getTentativas() - 1);
		}
		
		return caractereEncontrado;
	}
	
	void comecaDescobertas(){
		setDescobertas(new string('_', getPalavra().Length).Replace("_","_ "));
	}
	
	public void Ganhar(){
		estado = Estado.VITORIA;
	}
	
	public void Perder(){
		estado = Estado.DERROTA;
	}
}

