using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Camera
{
	public class CameraShake : MonoBehaviour {

		public float shakeTime = 0.5f;
		public Vector3 shakeRange = new Vector3 (0.2f, 0.2f, 0);

		private float currentShakeTime;
		private float timer;

		private Vector3 originPos;
		private bool isShakeEnd;


		// Use this for initialization
		void Start () {
			currentShakeTime = -1f;
			timer = 0f;
			originPos = transform.position;
			isShakeEnd = false;
		}
		
		// Update is called once per frame
		void Update () {
			if (timer <= currentShakeTime) {
				isShakeEnd = true;
				timer += Time.deltaTime;

				transform.position = originPos + MulVector3(shakeRange,Random.insideUnitSphere);
			}
			else {
				if (isShakeEnd) {
					transform.position = originPos;
					isShakeEnd = false;
				}
				originPos = transform.position;
			}
		}

		public void ShakeStart()
		{
			timer = 0;
			currentShakeTime = shakeTime;
		}

		private Vector3 MulVector3(Vector3 a , Vector3 b)
		{
			return new Vector3 (a.x * b.x, a.y * b.y, a.z * b.z);
		}



	}
}
