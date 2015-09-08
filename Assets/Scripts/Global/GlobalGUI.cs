using UnityEngine;
using System.Collections;
using PoqXert.MessageBox;

public class GlobalGUI : MonoBehaviour {

	/// <summary>
	/// Reload the Scene
	/// </summary>
	public void ReloadScene()
	{
		DesactivarResto();
		MsgBox.Show(1, "¿Quieres recargar esta escena?", "Recargar Escena", MsgBoxButtons.YES_NO, MsgBoxStyle.Question, ReloadCallBack);
	}

	/// <summary>
	/// Closes the game.
	/// </summary>
	public void CloseGame()
	{
		DesactivarResto();
		MsgBox.Show(1, "¿Quieres salir del Juego?", "Salir del Juego", MsgBoxButtons.YES_NO, MsgBoxStyle.Question, CloseCallback);

	}

	/// <summary>
	/// Se llama cuando se termina el messagebox que pregunta que hacer.
	/// </summary>
	private void ReloadCallBack(int id, DialogResult btn)
	{
		if(btn == DialogResult.YES_OK)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		else
		{
			MsgBox.Close();
		}
		ActivarResto();
	}

	private void CloseCallback(int id, DialogResult btn)
	{
		if(btn == DialogResult.YES_OK)
		{
			
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#endif
			Application.Quit();
		}
		else
		{
			MsgBox.Close();
		}
		ActivarResto();
	}



	/// <summary>
	/// Desactiva otras cosas en la escena para que no interfieran con el dialogo que se muestra
	/// </summary>
	void DesactivarResto ()
	{
		Escena.instance.DesactivarTodo();
	}

	/// <summary>
	/// Reactiva las otras cosas en la escena para retornar a la normalidad
	/// </summary>
	void ActivarResto ()
	{
		Escena.instance.ActivarTodo();
	}

}
