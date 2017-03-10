using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCount : MonoBehaviour {

	int frameCount;
	float nextTime;
	public Text FPS;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		frameCount++;

		if (Time.time >= nextTime) {
			FPS.text = "FPS:"+frameCount.ToString();
			frameCount = 0;
			nextTime += 1;

		}

	}
}
