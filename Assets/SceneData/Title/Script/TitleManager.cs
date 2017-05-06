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
    Button deleteUserData;
    [SerializeField]
    Button popupTest;
    [SerializeField]
    SimplePopup simplePopup;
    [SerializeField]
    PagePopup pagePopup;

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
      deleteUserData.OnClickAsObservable()
        .Subscribe(_ =>
        {
          DeleteData();
        }).AddTo(gameObject);

      //pptest
      popupTest.OnClickAsObservable()
        .Subscribe(_ =>
        {
          /*
           var pp = PopupManager.Instance.Create<SimplePopup>(simplePopup);
           pp.SetTitle("TestTitle!!!!");
           pp.SetDist("This is TestPopup");*/

          var pp = PopupManager.Instance.Create<PagePopup>(pagePopup);
          pp.SetData(Resources.Load<PagePopupData>("PagePopupData"));
          PopupManager.Instance.OpenPopup(pp, null);
          
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
      SceneChanger.Instance.ChangeScene("Home");

    } 

    void DeleteData()
    {
      havePartDB.DeleteData();
      haveItemDB.DeleteData();
      userDB.DeleteData();
    }
  }
}
