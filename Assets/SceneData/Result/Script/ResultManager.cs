namespace Result
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.UI;
  using Common;
  using UniRx;

  public class ResultManager : MonoBehaviour
  {
    [SerializeField]
    Button button;
    // Use this for initialization
    void Start()
    {
      SceneChanger.Instance.IsInitialize = true;
      button.OnClickAsObservable()
        .Subscribe(_ => SceneChanger.Instance.ChangeScene("Title")).AddTo(gameObject);
    }

  }
}
