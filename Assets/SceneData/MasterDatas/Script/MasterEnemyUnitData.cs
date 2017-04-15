namespace Common.MasterData
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using EnemyUnitData;
  public class MasterEnemyUnitData : ScriptableObject
  {
    [SerializeField]
    List<EnemyUnitData> list;

    public List<EnemyUnitData> List { get { return list; }set { list = value; } }
  }
}