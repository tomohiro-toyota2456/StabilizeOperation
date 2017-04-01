using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileText : MonoBehaviour {

	public string Name; // 後ほど読み取りに変更
	private int Money;
	private int LV;

	public Text profileName;
	public Text LVText;
	public Text moneyNumText;


	// Use this for initialization
	void Start () {
		//後ほど読み取りに変更
		Money = 0;
		LV = 999;

		profileName.text = Name ;
		LVText.text ="LV:" + LV.ToString ();
		moneyNumText.text = Money.ToString () + "円";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
