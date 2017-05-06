namespace Organization
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Common;
  using Common.DataBase;

  //編成トップのマネージャ
  public class OrganizationTopManager : MonoBehaviour
  {
    [SerializeField]
    DeckShow[] deckShowArray = new DeckShow[GameCommon.deckMax];

    UserDataObject.OrganizationData[] deckArray;
    // Use this for initialization
    void Start()
    {
      //デッキロード
      var userDB = DataBaseManager.Instance.GetDataBase<UserDB>();
      deckArray = userDB.GetDeck();

      //デッキの状態を表示するためにデータをいれていく
      for(int i = 0; i < GameCommon.unitMax; i++)
      {
        bool isUse = deckArray[0].unitDataArray[i].isUse;

        //使ってない場合はEmpty表記にする
        if(isUse && !string.IsNullOrEmpty(deckArray[0].unitDataArray[i].unitName))
        {
          deckShowArray[0].SetName(i, deckArray[0].unitDataArray[i].unitName);
        }
        else
        {
          deckShowArray[0].SetName(i, "Empty");
        }

        isUse = deckArray[1].unitDataArray[i].isUse;

        if (isUse && !string.IsNullOrEmpty(deckArray[1].unitDataArray[i].unitName))
        {
          deckShowArray[1].SetName(i, deckArray[1].unitDataArray[i].unitName);
        }
        else
        {
          deckShowArray[1].SetName(i, "Empty");
        }

        isUse = deckArray[2].unitDataArray[i].isUse;

        if (isUse && !string.IsNullOrEmpty(deckArray[2].unitDataArray[i].unitName))
        {
          deckShowArray[2].SetName(i, deckArray[2].unitDataArray[i].unitName);
        }
        else
        {
          deckShowArray[2].SetName(i, "Empty");
        }

      }

      SceneChanger.Instance.IsInitialize = true;
    }

  }
}
