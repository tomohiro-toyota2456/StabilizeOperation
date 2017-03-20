namespace Title
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Common;
  using UnityEngine.UI;
  using UniRx;

  public class TitleManager : MonoBehaviour
  {
    [SerializeField]
    Button startButton;

    // Use this for initialization
    void Start()
    {
      SceneChanger.Instance.IsInitialize = true;

      startButton.OnClickAsObservable()
        .Subscribe(_ => 
        {
          SceneChanger.Instance.ChangeScene("Game");
        }).AddTo(gameObject);

    }
  }
}
