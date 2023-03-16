using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGooGame
{
   public class LevelLoader : MonoBehaviour
   {
      [SerializeField]
      private string levelName;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				GameManager.LoadLevel(levelName);
			}
		}
	}
}