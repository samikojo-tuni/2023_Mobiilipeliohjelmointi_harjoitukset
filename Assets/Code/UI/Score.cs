using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RedGooGame.UI
{
   public class Score : MonoBehaviour
   {
      private TMP_Text scoreText;

		private void Awake()
		{
         scoreText = GetComponent<TMP_Text>();
		}

		private void Update()
      {
         scoreText.text = $"Score: {GameManager.GetScore()}";
      }
   }
}