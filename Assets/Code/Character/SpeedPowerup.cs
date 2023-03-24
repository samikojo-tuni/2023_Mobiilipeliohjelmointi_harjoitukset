using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGooGame
{
   public class SpeedPowerup : MonoBehaviour
   {
      [SerializeField]
      private float speedModifier = 1;

      [SerializeField]
      private float time = 1;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.CompareTag("Player"))
         {
            PhysicsMover mover = collision.GetComponent<PhysicsMover>();
            if (mover != null)
            {
               mover.ApplySpeedModifier(speedModifier, time);

               Destroy(gameObject);
            }
         }
		}
	}
}