using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;


namespace Tutorial
{

public class TutorialHomeManager : MonoBehaviour {

		// Use this for initialization
		void Start () {
			this.UpdateAsObservable()
				.First(x => Time.time > 1.0f)
				.Subscribe(x => SetTextField());
		}
		
		// Update is called once per frame
		void Update () {

		}

		void SetMask()
		{
			maskImage.gameObject.SetActive (true);
			frameImage.gameObject.SetActive (true);
			arrowImage.gameObject.SetActive (true);
		}

		void SetBlockMask(bool isSet)
		{
			if (blockImage != null) {
				blockImage.gameObject.SetActive (isSet);
			}
		}

		void SetTextEvent()
		{

		}

		void SetTextField()
		{
			textImage.gameObject.SetActive (true);
			textImage.GetComponent<TutorialText> ().textEventNum = 4;
			textImage.GetComponent<TutorialText> ().ManagerSet = this.gameObject;
		}

		//文字送り用テキスト
		[SerializeField]
		private Image textImage;

		//マスク
		[SerializeField]
		private Image maskImage;

		//矢印イメージ
		[SerializeField]
		private Image arrowImage;

		//フレームイメージ（枠）
		[SerializeField]
		private Image frameImage;

		//ブロックマスク（壁）
		[SerializeField]
		private Image blockImage;

		private bool isCameraShake;
}



}
