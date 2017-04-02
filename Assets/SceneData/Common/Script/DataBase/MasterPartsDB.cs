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

    public override void Init()
    {

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

        case "i":
          param = GetAccessoryData(_id);
          break;
      }

      return param;

    }

    public RoboPartParam GetHeadData(string _id)
    {
      var part = masterPartsData.HeadDataList.First(data=> data.Id == _id);
      return part;
    }

    public RoboPartParam GetWeponData(string _id)
    {
      var part = masterPartsData.WeponDataList.First(data => data.Id == _id);
      return part;
    }

    public RoboPartParam GetLegData(string _id)
    {
      var part = masterPartsData.LegDataList.First(data => data.Id == _id);
      return part;
    }

    public RoboPartParam GetAccessoryData(string _id)
    {
      var part = masterPartsData.AccessoryDataList.First(data => data.Id == _id);
      return part;
    }

    public RoboPartParam[] GetHeadDataArray()
    {
      return masterPartsData.HeadDataList.ToArray();
    }

    public RoboPartParam[] GetWeponDataArray()
    {
      return masterPartsData.WeponDataList.ToArray();
    }

    public RoboPartParam[] GetLegDataArray()
    {
      return masterPartsData.LegDataList.ToArray();
    }

    public RoboPartParam[] GetAccessoryDataArray()
    {
      return masterPartsData.AccessoryDataList.ToArray();
    }

  }
}
