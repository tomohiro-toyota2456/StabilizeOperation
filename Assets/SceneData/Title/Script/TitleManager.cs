namespace Title
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Common;
  using UnityEngine.UI;
  using UniRx;
  using Common.DataBase;

  public class TitleManager : MonoBehaviour
  {
    [SerializeField]
    Button startButton;
    [SerializeField]
    Button DeleteUserData;

    // Use this for initialization
    void Start()
    {
      SceneChanger.Instance.IsInitialize = true;

      startButton.OnClickAsObservable()
        .Subscribe(_ => 
        {
          SceneChanger.Instance.ChangeScene("Game");
        }).AddTo(gameObject);

      DeleteUserData.OnClickAsObservable()
        .Subscribe(_ =>
        {
          DataBaseManager.Instance.GetDataBase<UserDB>().DeleteData();
        }).AddTo(gameObject);

    }
  }
}
