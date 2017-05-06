namespace Common
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.UI;
  using System;
  using UniRx;
  //ToggleFunction
  //ボタンでOnOffのトグルを実装するクラス
  public class ToggleFunction : MonoBehaviour
  {
    [SerializeField]
    Button button;
    [SerializeField]
    Sprite onSprite;//スプライトが設定されてない場合のみカラー設定を使う
    [SerializeField]
    Sprite offSprite;
    [SerializeField]
    Color onColor;
    [SerializeField]
    Color offColor;

    bool isOn;
    public bool IsOn { get { return isOn; }}

    Action<bool> changeAction;//切り替えアクション
    Action<bool> changeStateAction;//

    public Action<bool> ChangeStateAction { set { changeStateAction = value; } }

    // Use this for initialization
    void Start()
    {
      changeAction = ChangeSprite;

      if(onSprite == null || offSprite == null)
      {
        changeAction = ChangeColor;
      }

      button.OnClickAsObservable()
        .Subscribe(_ =>
        {
          isOn = !isOn;

          if(changeStateAction != null)
          {
            changeStateAction(isOn);
          }

          changeAction(isOn);
        }).AddTo(gameObject);
    }

    //外部からOn/Off切り替える関数
    public void ChageState(bool _isOn)
    {
      isOn = _isOn;
      changeAction(isOn);
    }

    void ChangeSprite(bool _isOn)
    {
      button.image.sprite = _isOn ? onSprite : offSprite;
    } 

    void ChangeColor(bool _isOn)
    {
      button.image.color = _isOn ? onColor : offColor;
    }

  }
}