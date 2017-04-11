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

    MasterEnemyUnitData masterClone;

    public override void Init()
    {
      masterClone = Instantiate(master);
      base.Init();
    }

    public EnemyUnitData GetData(string _id)
    {
      return masterClone.List.First(d => d.EnemyUnitId == _id);
    }
  }
}