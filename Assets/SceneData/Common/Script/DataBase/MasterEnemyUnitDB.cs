namespace Common.DataBase
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using EnemyUnitData;
  using MasterData;
  using System.Linq;

  public class MasterEnemyUnitDB : DataBase
  {
    [SerializeField]
    MasterEnemyUnitData master;

    public override void Init()
    {
      base.Init();
    }

    public EnemyUnitData GetData(string _id)
    {
      return master.List.First(d => d.EnemyUnitId == _id);
    }
  }
}