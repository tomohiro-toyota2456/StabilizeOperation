namespace EnemyUnitData
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class EnemyUnitData : ScriptableObject
  {
    [SerializeField]
    string enemyUnitId;
    [SerializeField]
    string[] partIdArray;//Head Wepon leg Accessoryの順　Accessoryのみなくても動く

    public string EnemyUnitId { get { return enemyUnitId; }set { enemyUnitId = value; } }
    public string[] PartIdArray { get { return partIdArray; }set { partIdArray = value; } }
  }
}