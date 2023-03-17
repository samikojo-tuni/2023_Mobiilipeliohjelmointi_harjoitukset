using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGooGame
{
   public class Coin : MonoBehaviour
   {
		[SerializeField]
		private int score = 1;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.CompareTag("Player"))
			{
				GameManager.AddScore(score);

				Destroy(gameObject);
			}
		}
	}
}