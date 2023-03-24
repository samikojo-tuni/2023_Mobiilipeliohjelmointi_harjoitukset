using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RedGooGame.UI
{
	public class Pause : MonoBehaviour
	{
		private bool isPaused = false;

		private void OnEnable()
		{
			gameObject.SetActive(isPaused);
		}

		public void OnPause(InputAction.CallbackContext context)
		{
			isPaused = !isPaused;

			Time.timeScale = isPaused ? 0 : 1;
			// Sama asia kuin alla oleva
			//if (isPaused == true)
			//{
			//	Time.timeScale = 0;
			//}
			//else
			//{
			//	Time.timeScale = 1;
			//}

			gameObject.SetActive(isPaused);
		}
	}
}