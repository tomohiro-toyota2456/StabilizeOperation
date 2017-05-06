namespace Organization
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.UI;
  using TMPro;
  using Common;

  //文字列にユニットの名前をいれるためのクラス
  //単純にそれようのテキストを用意してるだけ
  public class DeckShow : MonoBehaviour
  {
    [SerializeField]
    TextMeshProUGUI[] UnitNameTextArray = new TextMeshProUGUI[GameCommon.unitMax];
    
    public void SetName(int _idx,string _name)
    {
      UnitNameTextArray[_idx].text = _name;
    }

  }
}