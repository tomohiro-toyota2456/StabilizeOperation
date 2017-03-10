using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelCreator : MonoBehaviour {
	public GameObject Prefab;
	public Text AllCount;
	private int count =0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		AllCount.text = count.ToString ();
	}

	public void ObjOne()
	{
		
		Instantiate (Prefab);
		count++;
	}

	public void Obj10()
	{
		for (int i = 0; i < 10; i++) {
			Instantiate (Prefab);
			count++;
		}
	}

	public void Obj100()
	{
		for (int i = 0; i < 100; i++) {
			Instantiate (Prefab);
			count++;
		}
	}

	public void ChangeFPS30()
	{
		Application.targetFrameRate = 30;
	}

	public void ChangeFPS60()
	{
		Application.targetFrameRate = 60;
	}
}
