using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace Tutorial
{
	public class TutorialText : MonoBehaviour {

	
		public bool textEventFlag{ get { return isEventFlag; } }
		public int textEventNum{set{ eventTextNum = value;}}
		public GameObject ManagerSet{ set { Manager = value; } }

		// Use this for initialization	
		void Start () {
			SetNextLine ();
		}

		public bool IsCompleteText{
			get{ return Time.time > timeElapsed + timeDisplay; }
		}

		// Update is called once per frame
		void Update () {

			if (IsCompleteText) {
				//タップした時　高速文字送り
				if (currentLine < text_.Length && Input.GetMouseButtonDown (0)) {
					SetNextLine ();
				}
				else if (currentLine >= text_.Length && Input.GetMouseButtonDown (0)) {
					if (Manager != null) {
						Manager.SendMessage ("SetTextEvent", currentEventNum);
						Debug.Log ("TEST");
					}
				}
			} else {
				if (Input.GetMouseButtonDown (0)) {
					timeDisplay = 0;
				}
			}

			int displayCount = (int)(Mathf.Clamp01 ((Time.time - timeElapsed) / timeDisplay) * currentText.Length);

			//表示文字数が前回の表示文字数と異なるならテキストを更新する
			if(displayCount != lastCharacter)
			{
				UIText.text = currentText.Substring (0, displayCount);
				lastCharacter = displayCount;
			}

		}
			

		void SetNextLine()
		{
			currentText = text_ [currentLine];
			currentLine ++;

			//想定表示時間と現在の時刻をキャッシュ
			timeDisplay = currentText.Length * textDisplayTime;
			timeElapsed = Time.time;

			//文字カウントを初期化
			lastCharacter = -1;

			//特定の文字条件下でイベントを起こす
			if (currentLine == eventTextNum) {
				//オブジェクトにメッセージを送る
				if (Manager != null) {
					//本当は使い回したいけど　いい処理が思い浮かばん・・・
					//なんか新しいのがあるっぽいけど、現状使いたい場合文字列を統一化する感じでやる
					//この処理がいらない時は呼び出し先のオブジェを入れなければいい。
					Manager.SendMessage ("SetTextEvent",currentEventNum);
					currentEventNum++;
				}
			}
		}
			
		//内部変数


		//テキストの格納
		[SerializeField]
		private string[] text_;

		//インスペクタ
		public Text UIText;


		//一文字あたりの表示時間
		[SerializeField][Range(0.001f,0.3f)]
		float textDisplayTime = 0.05f;

		//現在の文字
		private int currentLine = 0;
		//現在の文字列
		private string currentText = string.Empty;
		//表示にかかる時間
		private float timeDisplay = 0;
		//文字列の表示開始
		private float timeElapsed = 1;
		//表示中の文字列
		private int lastCharacter = -1;

		//
		private bool isEventFlag = false;

		//何番目のテキストでイベントが起きるか
		private int eventTextNum = -1;

		//処理したい各シーンのマネージャー
		private GameObject Manager = null;

		//処理したイベントの数、必要に応じて返す
		private int currentEventNum = 0;
	}
}