/*
 * Created by SharpDevelop.
 * User: aliss
 * Date: 04/10/2024
 * Time: 11:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Utils{
	
	public static class FormHelpers
	{
		public static void mudarVisibilidade(bool visibilidade, params object[] objs){
			foreach (Control obj in objs){
				obj.Visible = visibilidade;
			}
		}
	
		public static void mudarVisibilidade1<T>(bool visibilidade, IEnumerable<T> objs) where T : Control{
			foreach (T obj in objs){
				obj.Visible = visibilidade;
			}
		}
			
		public static void mudarAtivado(bool ativado, params object[] objs){
			foreach (Control obj in objs){
				obj.Enabled = ativado;
			}
		}
		
		public static void mudarAtivado1<T>(bool ativado, IEnumerable<T> objs) where T : Control{
			foreach (T obj in objs){
				obj.Enabled = ativado;
			}
		}
	}
}
