namespace Common.DataBase
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using System.Linq;
  public class HaveItemDB : DataBase
  {
    readonly string key = "ofjeih`IEGH`EIGNHEI[@";

    HaveItemList haveItemList;

    [System.Serializable]
    public class HaveItemList
    {
      public List<HaveItemData> list;
    }

    [System.Serializable]
    public struct HaveItemData
    {
      public string id;
      public int haveNum;
    }

    public override void Init()
    {
      LoadData();
    }

    public void LoadData()
    {
      string json = PlayerPrefs.GetString(key);

      if(string.IsNullOrEmpty(json))
      {
        haveItemList = new HaveItemList();
      }

      haveItemList = JsonUtility.FromJson<HaveItemList>(json);

    }

    public void SaveData()
    {
      string json = JsonUtility.ToJson(haveItemList);

      if(string.IsNullOrEmpty(json))
      {

        return;
      }

      PlayerPrefs.SetString(key, json);
    }

    public HaveItemData[] GetHaveItemArray()
    {
      var list = haveItemList.list;

      return list.Where(d => d.haveNum > 0).ToArray();
    }

    public void AddItem(HaveItemData _data)
    {
      //すでにIDがある場合は加算する
      if (haveItemList.list.Any(d => d.id == _data.id))
      {
        for(int i = 0; i < haveItemList.list.Count; i++)
        {
          if(haveItemList.list[i].id == _data.id)
          {
            _data.haveNum += haveItemList.list[i].haveNum;
            haveItemList.list[i] = _data;
            break;
          }
        }
      }
      else
      {
        haveItemList.list.Add(_data);
      }
    }



  }
}