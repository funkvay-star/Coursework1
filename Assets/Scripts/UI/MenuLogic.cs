using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
	public void NewGameButtonPressed()
	{
		Debug.Log("Pressed menu");
		LevelChanger._instance.LoadNextLevel();
	}

	public void ExitGameButtonPressed()
	{
		Application.Quit();
	}
}
