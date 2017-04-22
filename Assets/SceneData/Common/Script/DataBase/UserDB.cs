namespace Common.DataBase
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  //*************************************************************
  //UserDB
  //ユーザーデータDB
  //
  //************************************************************* 
  public class UserDB : DataBase
  {
    [SerializeField]
    UserDataObject initUserData;

    UserData userData;

    readonly string key = "oJFPOWHFO2e{}QFHJwq";

    //******************************************************
    //UserData
    //データ保持用　Josn化はこちらのクラスで行う
    //******************************************************
    public struct UserData
    {
      public string userName;
      public int userLv;
      public int curExp;
      public int money;
      public UserDataObject.OrganizationData[] deckDataArray;
      public int selectDeckId;
    }

    public bool CheckExistData()
    {
      string json = PlayerPrefs.GetString(key);
      if(string.IsNullOrEmpty(json))
      {
        return false;
      }

      return true;
    }

    public void DeleteData()
    {
      PlayerPrefs.DeleteKey(key);
    }

    public void LoadUserData()
    {
      string json = PlayerPrefs.GetString(key);

      if (string.IsNullOrEmpty(json))
      {
        userData = new UserData();
        userData.userName = initUserData.UserName;
        userData.userLv = initUserData.UserLv;
        userData.money = initUserData.Money;
        userData.deckDataArray = initUserData.OrganizationDataArray;
        userData.curExp = 0;

        json = JsonUtility.ToJson(userData);

        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
      }

      userData = JsonUtility.FromJson<UserData>(json);
    }

    public void SaveUserData()
    {
      string json = JsonUtility.ToJson(userData);
      PlayerPrefs.SetString(key, json);
      PlayerPrefs.Save();
    }

    public string GetUserName()
    {
      return userData.userName;
    }

    public int GetUserLv()
    {
      return userData.userLv;
    }

    public int GetMoney()
    {
      return userData.money;
    }

    public void SetUseName(string _name)
    {
      userData.userName = _name;
    }

    public void SetUserLv(int _lv)
    {
      userData.userLv = _lv;
    }

    public void SetMoney(int _money)
    {
      userData.money = _money;
    }
    //******************************************************
    //GetDeck
    //ユニット構成デッキを取得
    //******************************************************
    public UserDataObject.OrganizationData[] GetDeck()
    {
      if (userData.deckDataArray != null)
      {
        return userData.deckDataArray;
      }

      //データがないor不正なので
      UserDataObject.OrganizationData[] deck = new UserDataObject.OrganizationData[3];

      return deck;
    }
    //******************************************************
    //GetUsingDeck
    //使用しているデッキを取得
    //******************************************************
    public UserDataObject.OrganizationData GetUsingDeck()
    {
      if(userData.deckDataArray != null)
      {
        return userData.deckDataArray[userData.selectDeckId];
      }

      return null;
    }
    //******************************************************
    //SetDeck
    //デッキデータをセットする
    //******************************************************
    public void SetDeck(UserDataObject.OrganizationData[] _deckArray)
    {
      if (_deckArray != null)
      userData.deckDataArray = _deckArray;
    }
  }
}