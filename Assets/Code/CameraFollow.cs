using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGooGame
{
   public class CameraFollow : MonoBehaviour
   {
		[SerializeField]
		private Transform target;

		[SerializeField]
		private float smoothTime = 0.3f;

		[SerializeField]
		private Vector3 offset;

		private Vector3 velocity = Vector3.zero;

		void Update()
		{
			// Define a target position above and behind the target transform
			Vector3 targetPosition = target.TransformPoint(offset);

			// Smoothly move the camera towards that target position
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
		}

		public void SetTarget(GameObject newTarget)
		{
			this.target = newTarget.transform;
		}
	}
}