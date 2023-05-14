using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
	public static LevelChanger _instance;
	private int _levelIndex;
	private int _gemsCount;

	private void Awake()
	{
		if (_instance != null)
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		_instance = this;

		// Subscribe to the sceneLoaded event
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	// This method is called whenever a scene is loaded
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (LevelManager._instance)
		{
			LevelManager._instance.Gems = _gemsCount;
			UIController._instance.UpdateGems();
		}
	}

	public void LoadNextLevel()
	{
		Debug.Log(_levelIndex);

		if (LevelManager._instance)
		{
			_gemsCount = LevelManager._instance.Gems;
		}

		SceneManager.LoadScene(++_levelIndex);
	}

	public void EndGame()
	{
		_levelIndex = 0;
		SceneManager.LoadScene(_levelIndex);
	}

	private void OnDestroy()
	{
		// Unsubscribe from the sceneLoaded event when the object is destroyed
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
