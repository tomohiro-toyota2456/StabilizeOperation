namespace Common.MasterData
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Game.Robo;

  public class MasterPartsData : ScriptableObject
  {
    //各パーツリスト
    [SerializeField]
    List<RoboPartParam> headDataList;
    [SerializeField]
    List<RoboPartParam> weponDataList;
    [SerializeField]
    List<RoboPartParam> legDataList;
    [SerializeField]
    List<RoboPartParam> accessoryDataList;

    public List<RoboPartParam> HeadDataList { get { return headDataList; } set { headDataList = value; } }
    public List<RoboPartParam> WeponDataList { get { return weponDataList; } set { weponDataList = value; } }
    public List<RoboPartParam> LegDataList { get { return legDataList; } set { legDataList = value; } }
    public List<RoboPartParam> AccessoryDataList { get { return accessoryDataList; } set { accessoryDataList = value; } }
  }
}
