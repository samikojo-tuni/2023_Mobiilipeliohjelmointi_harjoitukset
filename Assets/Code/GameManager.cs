using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RedGooGame.UI;
using System;

namespace RedGooGame
{
	public class GameManager : MonoBehaviour
	{
		public enum LoadingState
		{
			None,
			FadeIn,
			LoadingScene,
			FadeOut
		}

		public const string LoadingSceneName = "loader";

		// Staattisten muuttujien arvo säilyy scenelatausten välillä
		private static int Score = 0;

		// The current state of the level loader
		private static LoadingState state = LoadingState.None;

		// A reference to the original scene
		private static Scene originalScene;

		// The name of the next scene
		private static string nextSceneName;

		// A reference to the loading scene
		private static Scene loadingScene;

		private Fader fader;

		private void Start()
		{
			DontDestroyOnLoad(gameObject);
		}

		private void OnEnable()
		{
			// Starts listening to the SceneManager.sceneLoaded event.
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		private void OnDisable()
		{
			// Stops listening to the event.
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}

		private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadMode)
		{
			switch (state)
			{
				case LoadingState.FadeIn:
					loadingScene = loadedScene;

					// Loading scene is just loaded.
					// Fetch references to all root level GameObjects in Loading Screen.
					GameObject[] loadingSceneObjects = loadedScene.GetRootGameObjects();
					foreach (GameObject go in loadingSceneObjects)
					{
						fader = go.GetComponentInChildren<Fader>();
						if (fader != null)
						{
							float time = fader.FadeIn();
							StartCoroutine(FadeAndLoadNextScene(time));
							break; // Exits foreach since Fader was found
						}
					}
					break;

				case LoadingState.LoadingScene:
					// The next scene is now loaded.
					if (fader != null)
					{
						float time = fader.FadeOut();
						StartCoroutine(FinalizeLoad(time));
						state = LoadingState.FadeOut;
					}
					break;
			}
		}

		public static void LoadLevel(string sceneName)
		{
			// A very simple way of loading a new scene
			//SceneManager.LoadScene(sceneName);

			// Loading a new scene with a loading screen
			nextSceneName = sceneName;

			// Fetch a reference to the current scene
			originalScene = SceneManager.GetActiveScene();

			// Start the loading sequense by loading the loading screen.
			// Additive load mode because we don't want to unload original scene just yet.
			state = LoadingState.FadeIn;
			SceneManager.LoadSceneAsync(LoadingSceneName, LoadSceneMode.Additive);
		}

		private IEnumerator FadeAndLoadNextScene(float fadeTime)
		{
			// Wait until the fade is done
			yield return new WaitForSeconds(fadeTime);

			// Unload the original scene.
			SceneManager.UnloadSceneAsync(originalScene);

			// Load the next scene.
			SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);

			state = LoadingState.LoadingScene;
		}

		private IEnumerator FinalizeLoad(float time)
		{
			yield return new WaitForSeconds(time);

			SceneManager.UnloadSceneAsync(loadingScene);

			state = LoadingState.None;
			fader = null;
		}

		public static void AddScore(int score)
		{
			Score += score;
			Debug.Log("Pisteet: " + Score);
		}

		public static int GetScore()
		{
			return Score;
		}
	}
}