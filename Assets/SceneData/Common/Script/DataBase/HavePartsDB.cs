namespace Common.DataBase
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using System.Linq;
  //************************************************************
  //HavePartsDB
  //所持パーツ管理用
  //************************************************************
  public class HavePartsDB : DataBase
  {
    [SerializeField]
    HavePartsInitData initData;

    //セーブ用キー群
    readonly string headPartKey = "odjwjfpqkoejgpo[wekg@";
    readonly string weponPartKey = "fgnjienhgingegmneogn";
    readonly string legPartKey = "odjpkfMOJFGP{FMOg[gggg";
    readonly string accessoryPartKey = "odjwjfwfdPO`WF`{FKQW@";

    //パーツデータ　リストをクラスにおいてるのはJsonに変換する都合上
    HaveParts headParts = new HaveParts();
    HaveParts weponParts = new HaveParts();
    HaveParts legParts = new HaveParts();
    HaveParts accessoryParts = new HaveParts();

    [System.Serializable]
    public struct HavePartData
    {
      public string id;
      public int lv;
      public int curExp;
    }

    [System.Serializable]
    public class HaveParts
    {
      public List<HavePartData> partList;
    } 

    //パーツ所持データの一括ロード
    public void LoadData()
    {
      var json = PlayerPrefs.GetString(headPartKey);

      if (!string.IsNullOrEmpty(json))
      {
        headParts = JsonUtility.FromJson<HaveParts>(json);
      }
      else
      {
        headParts = new HaveParts();
        headParts.partList = new List<HavePartData>();

        //初期所持データを入れていく
        HavePartData data = new HavePartData();
        data.id = initData.HeadPartId.Substring(0);
        data.lv = 1;
        data.curExp = 0;

        SetHeadData(data);
        
      }

      json = PlayerPrefs.GetString(weponPartKey);

      if (!string.IsNullOrEmpty(json))
      {
        weponParts = JsonUtility.FromJson<HaveParts>(json);
      }
      else
      {
        weponParts = new HaveParts();
        weponParts.partList = new List<HavePartData>();

        //初期所持データを入れていく
        HavePartData data = new HavePartData();
        data.id = initData.WeponPartId.Substring(0);
        data.lv = 1;
        data.curExp = 0;

        SetWeponData(data);

      }

      json = PlayerPrefs.GetString(legPartKey);


      if (!string.IsNullOrEmpty(json))
      {
        legParts = JsonUtility.FromJson<HaveParts>(json);
      }
      else
      {
        legParts = new HaveParts();
        legParts.partList = new List<HavePartData>();

        //初期所持データを入れていく
        HavePartData data = new HavePartData();
        data.id = initData.LegPartId.Substring(0);
        data.lv = 1;
        data.curExp = 0;

        SetLegData(data);

      }

      json = PlayerPrefs.GetString(accessoryPartKey);

      if (!string.IsNullOrEmpty(json))
      {
        accessoryParts = JsonUtility.FromJson<HaveParts>(json);
      }
      else
      {
        accessoryParts = new HaveParts();
        accessoryParts.partList = new List<HavePartData>();
      }

      SaveData();
    }

    //データ追加
    public void AddData(HavePartData _data)
    {
      string headStr = _data.id.Substring(0,1);

      switch(headStr)
      {
        case "h":
          headParts.partList.Add(_data);
          break;
        case "w":
          weponParts.partList.Add(_data);
          break;
        case "l":
          legParts.partList.Add(_data);
          break;
        case "a":
          accessoryParts.partList.Add(_data);
          break;
        default:
          break;
      }

    }

    #region GetData
    //データゲット
    public HavePartData GetData(string _id)
    {
      string headStr = _id.Substring(0,1);

      HavePartData data;
      switch(headStr)
      {
        case "h":
          data = GetHeadData(_id);
          break;
        case "w":
          data = GetWeponData(_id);
          break;
        case "l":
          data = GetLegData(_id);
          break;
        case "a":
          data = GetAccessoryData(_id);
          break;
        default:
          data = new HavePartData();
          break;
      }

      return data;
    }

    public HavePartData GetHeadData(string _id)
    {
      var data = headParts.partList.First(d => d.id == _id);
      return data;
    }
    public HavePartData GetWeponData(string _id)
    {
      var data = weponParts.partList.First(d => d.id == _id);
      return data;
    }
    public HavePartData GetLegData(string _id)
    {
      var data = legParts.partList.First(d => d.id == _id);
      return data;
    }
    public HavePartData GetAccessoryData(string _id)
    {
      var data = accessoryParts.partList.First(d => d.id == _id);
      return data;
    }

    public List<HavePartData> GetHeadDataList()
    {
      List<HavePartData> list = new List<HavePartData>(headParts.partList);
      return list;
    }

    public List<HavePartData> GetWeponDataList()
    {
      List<HavePartData> list = new List<HavePartData>(weponParts.partList);
      return list;
    }

    public List<HavePartData> GetLegDataList()
    {
      List<HavePartData> list = new List<HavePartData>(legParts.partList);
      return list;
    }

    public List<HavePartData> GetAccessoryDataList()
    {
      List<HavePartData> list = new List<HavePartData>(accessoryParts.partList);
      return list;
    }

    #endregion

    #region SetData

    public void SetData(HavePartData _data)
    {
      string headStr = _data.id.Substring(0,1);
      switch (headStr)
      {
        case "h":
          SetHeadData(_data);
          break;
        case "w":
          SetWeponData(_data);
          break;
        case "l":
          SetLegData(_data);
          break;
        case "a":
          SetAccessoryData(_data);
          break;
        default:
          break;
      }
    }

    public void SetHeadData(HavePartData _data)
    {
      for(int i = 0; i < headParts.partList.Count; i++)
      {
        var data = headParts.partList[i];
        if(_data.id == data.id)
        {
          headParts.partList[i]= _data;
          return;
        }
      }

      //新規取得の場合は追加
      headParts.partList.Add(_data);

    }

    public void SetWeponData(HavePartData _data)
    {
      for (int i = 0; i < weponParts.partList.Count; i++)
      {
        var data = weponParts.partList[i];
        if (_data.id == data.id)
        {
          weponParts.partList[i] = _data;
          return;
        }
      }

      //新規取得の場合は追加
      weponParts.partList.Add(_data);
    }

    public void SetLegData(HavePartData _data)
    {
      for (int i = 0; i < legParts.partList.Count; i++)
      {
        var data = legParts.partList[i];
        if (_data.id == data.id)
        {
          legParts.partList[i] = _data;
          return;
        }
      }

      //新規取得の場合は追加
      legParts.partList.Add(_data);
    }

    public void SetAccessoryData(HavePartData _data)
    {
      for (int i = 0; i < accessoryParts.partList.Count; i++)
      {
        var data = accessoryParts.partList[i];
        if (_data.id == data.id)
        {
          accessoryParts.partList[i] = _data;
          return;
        }
      }

      //新規取得の場合は追
      accessoryParts.partList.Add(_data);
    }

    #endregion

    //所持パーツ毎のid配列を返す
    public string[] GetHaveHeadIds()
    {
      return headParts.partList.Select(d => d.id).ToArray();
    }

    public string[] GetHaveWeponIds()
    {
      return weponParts.partList.Select(d => d.id).ToArray();
    }

    public string[] GetHaveLegIds()
    {
      return legParts.partList.Select(d => d.id).ToArray();
    }

    public string[] GetHaveAccessoryIds()
    {
      return accessoryParts.partList.Select(d => d.id).ToArray();
    }

    //パーツ所持データ保存
    public void SaveData()
    {
      SaveHeadPartsData();
      SaveWeponPartsData();
      SaveLegPartsData();
      SaveAccessoryPartsData();
    }

    #region PartTypeSave
    //パーツ所持状況セーブ　パーツ毎
    public void SaveHeadPartsData()
    {
      string json = JsonUtility.ToJson(headParts);

      if(string.IsNullOrEmpty(json))
      {
#if UNITY_EDITOR
        Debug.Assert(false,"headPartsSaveFailed!!!!!");
#endif
        return;
      }

      PlayerPrefs.SetString(headPartKey, json);
    }

    public void SaveWeponPartsData()
    {
      string json = JsonUtility.ToJson(weponParts);

      if (string.IsNullOrEmpty(json))
      {
#if UNITY_EDITOR
        Debug.Assert(false, "weponPartsSaveFailed!!!!!");
#endif
        return;
      }

      PlayerPrefs.SetString(weponPartKey, json);
    }

    public void SaveLegPartsData()
    {
      string json = JsonUtility.ToJson(legParts);

      if (string.IsNullOrEmpty(json))
      {
#if UNITY_EDITOR
        Debug.Assert(false, "legPartsSaveFailed!!!!!");
#endif
        return;
      }

      PlayerPrefs.SetString(legPartKey, json);
    }

    public void SaveAccessoryPartsData()
    {
      string json = JsonUtility.ToJson(accessoryParts);

      if (string.IsNullOrEmpty(json))
      {
#if UNITY_EDITOR
        Debug.Assert(false, "accessoryPartsSaveFailed!!!!!");
#endif
        return;
      }
      PlayerPrefs.SetString(accessoryPartKey, json);
    }
    #endregion

    public void DeleteData()
    {
      PlayerPrefs.DeleteKey(headPartKey);
      PlayerPrefs.DeleteKey(weponPartKey);
      PlayerPrefs.DeleteKey(legPartKey);
      PlayerPrefs.DeleteKey(accessoryPartKey);
    }

  }
}