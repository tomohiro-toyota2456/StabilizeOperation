namespace Organization
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.UI;
  using Common;
  using Common.DataBase;
  using UniRx;
  using TMPro;
  public class UnitSelectManager : MonoBehaviour
  {
    [SerializeField]
    DeckShow deckShow;
    [SerializeField]
    TextMeshProUGUI deckNameText;
    [SerializeField]
    ToggleFunctionManager toggleFunctionManager;
    [SerializeField]
    Button moveButton;
    [SerializeField]
    UnitModelViewer unitModelViewer;

    static int selectDeckId = -1;
    static int selectUnitId = -1;
    public static int SelectDeckId { set { selectDeckId = value; } }
    public static int SelectUnitId { get { return selectUnitId; } set { selectUnitId = value; } }

    UserDataObject.OrganizationData deck;
    // Use this for initialization
    void Start()
    {
      var useDB = DataBaseManager.Instance.GetDataBase<UserDB>();

      deck = useDB.GetDeck(selectDeckId);

      //デッキ名反映
      deckNameText.text = "デッキ" + (selectDeckId + 1).ToString();

      for(int i = 0;i < GameCommon.unitMax;i++)
      {
        bool isUse = deck.unitDataArray[i].isUse;

        //使ってない場合はEmpty表記にする
        if (isUse && !string.IsNullOrEmpty(deck.unitDataArray[i].unitName))
        {
          deckShow.SetName(i,deck.unitDataArray[i].unitName);
        }
        else
        {
          deckShow.SetName(i, "Empty");
        }
      }

      //推してるIDが変わったらユニットも変更
      toggleFunctionManager.ChangeStateAction = (idx) =>
       {
         if(idx != -1)
         {
           selectUnitId = idx;
         }
         else
         {
           selectUnitId = -1;
         }

         ChangeShow3DModel(idx);
       };

      //移動ボタン
      moveButton.OnClickAsObservable()
        .Where(_=>selectUnitId >= 0)
        .Take(1)
        .Subscribe(_ =>
        {
          selectUnitId = toggleFunctionManager.GetToggleOnIdx();
          Debug.Log("Move");
        }).AddTo(gameObject);

      SceneChanger.Instance.IsInitialize = true;
    }

    void ChangeShow3DModel(int _idx)
    {
      if(_idx == -1)
      {
        //非表示処理
        unitModelViewer.Disable();
        return;
      }

      if(!deck.unitDataArray[_idx].isUse)
      {
        //非表示処理
        unitModelViewer.Disable();
        return;
      }

      //表示処理
      string head  = deck.unitDataArray[_idx].headId;
      string wepon = deck.unitDataArray[_idx].weponId;
      string leg   = deck.unitDataArray[_idx].legId;
      unitModelViewer.View(head, wepon, leg);
    }

  }
}