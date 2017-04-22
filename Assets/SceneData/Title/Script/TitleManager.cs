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

    HavePartsDB havePartDB;
    HaveItemDB haveItemDB;
    UserDB userDB;


    // Use this for initialization
    void Start()
    {
      //DB取得
      havePartDB = DataBaseManager.Instance.GetDataBase<HavePartsDB>();
      haveItemDB = DataBaseManager.Instance.GetDataBase<HaveItemDB>();
      userDB = DataBaseManager.Instance.GetDataBase<UserDB>();

      //開始ボタン
      startButton.OnClickAsObservable()
        .Subscribe(_ => 
        {
          Load();
        }).AddTo(gameObject);

      //データ削除ボタン
      DeleteUserData.OnClickAsObservable()
        .Subscribe(_ =>
        {
          DeleteData();
        }).AddTo(gameObject);

      SceneChanger.Instance.IsInitialize = true;
    }

    void Load()
    {
      bool isExistUserData = userDB.CheckExistData();

      userDB.LoadUserData();
      havePartDB.LoadData();
      haveItemDB.LoadData();

      //ここで分岐
      SceneChanger.Instance.ChangeScene("Game");

    } 

    void DeleteData()
    {
      havePartDB.DeleteData();
      haveItemDB.DeleteData();
      userDB.DeleteData();
    }
  }
}
