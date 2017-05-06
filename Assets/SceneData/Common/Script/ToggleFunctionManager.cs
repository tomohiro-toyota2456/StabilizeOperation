namespace Common
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UniRx;
  using System;
  //ToggleFunction管理用
  //ToggleGroup的な
  //基本これに管理される場合はグループ内でONが一つに保障される
  public class ToggleFunctionManager : MonoBehaviour
  {
    [SerializeField]
    ToggleFunction[] toggleFuntionArray;

    //状態が変わったときに呼び出すアクション:OnならそのIdxをOffなら-1を返す
    Action<int> changeStateAction;
    public Action<int> ChangeStateAction { set { changeStateAction = value; } } 

    // Use this for initialization
    void Start()
    {
      for (int i = 0; i < toggleFuntionArray.Length; i++)
      {
        int idx = i;
        toggleFuntionArray[i].ChangeStateAction = (isOn) =>
         {
           int onIdx = -1;

           //Onの場合は一度全部オフにして対象だけON
           if (isOn)
           {
             ChangeStateAll(false);
             toggleFuntionArray[idx].ChageState(true);

             onIdx = idx;
           }

           if(changeStateAction != null)
           {
             changeStateAction(onIdx);
           }

         };
      }
    }

    void ChangeStateAll(bool _isOn)
    {
      for (int i = 0; i < toggleFuntionArray.Length; i++)
      {
        toggleFuntionArray[i].ChageState(_isOn);
      }
    }

    //Onになっているボタンを探す　ない場合は-1が固定で帰る
    public int GetToggleOnIdx()
    {
      for (int i = 0; i < toggleFuntionArray.Length; i++)
      {
        if(toggleFuntionArray[i].IsOn)
        {
          return i;
        }
      }

      return -1;
    }
  }
}