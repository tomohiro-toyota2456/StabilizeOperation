namespace Mission
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class MissionData : ScriptableObject
  {
    [SerializeField]
    int chapterId;
    [SerializeField]
    string missionId;
    [SerializeField]
    string name;
    [SerializeField]
    string[] rewardIdArray;
    [SerializeField]
    string[] dropIdArray;
    [SerializeField]
    EnemyUnitData[] enemyUnitDataArray;

    [System.Serializable]
    public struct EnemyUnitData
    {
      public string enemyUnitId;
      public int lv;
    }

    public int ChapterId { get { return chapterId; }set { chapterId = value; } }
    public string MissionId { get { return missionId; }set { missionId = value; } }
    public string Name { get { return name; }set { name = value; } }
    public string[] RewardIdArray { get { return rewardIdArray; } set { rewardIdArray = value; } }
    public string[] DropIdArray { get { return dropIdArray; }set { dropIdArray = value; } }
    public EnemyUnitData[] EUnitDataArray { get { return enemyUnitDataArray; }set { enemyUnitDataArray = value; } }

  }
}