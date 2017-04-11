namespace Common.DataBase
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using MasterData;
  using Game.Robo;
  using System.Linq;

  public class MasterPartsDB : DataBase
  {
    [SerializeField]
    MasterPartsData masterPartsData;

    MasterPartsData masterClone;

    public override void Init()
    {
      masterClone = Instantiate(masterPartsData);
    }

    public RoboPartParam GetData(string _id)
    {
      string first = _id.Substring(0, 1);

      RoboPartParam param = null;
      switch(first)
      {
        case "h":
          param = GetHeadData(_id);
          break;

        case "w":
          param = GetWeponData(_id);
          break;

        case "l":
          param = GetLegData(_id);
          break;

        case "a":
          param = GetAccessoryData(_id);
          break;
      }

      return param;

    }

    public RoboPartParam GetHeadData(string _id)
    {
      var part = masterClone.HeadDataList.First(data=> data.Id == _id);
      return part;
    }

    public RoboPartParam GetWeponData(string _id)
    {
      var part = masterClone.WeponDataList.First(data => data.Id == _id);
      return part;
    }

    public RoboPartParam GetLegData(string _id)
    {
      var part = masterClone.LegDataList.First(data => data.Id == _id);
      return part;
    }

    public RoboPartParam GetAccessoryData(string _id)
    {
      var part = masterClone.AccessoryDataList.First(data => data.Id == _id);
      return part;
    }

    public RoboPartParam[] GetHeadDataArray()
    {
      return masterClone.HeadDataList.ToArray();
    }

    public RoboPartParam[] GetWeponDataArray()
    {
      return masterClone.WeponDataList.ToArray();
    }

    public RoboPartParam[] GetLegDataArray()
    {
      return masterClone.LegDataList.ToArray();
    }

    public RoboPartParam[] GetAccessoryDataArray()
    {
      return masterClone.AccessoryDataList.ToArray();
    }

  }
}
