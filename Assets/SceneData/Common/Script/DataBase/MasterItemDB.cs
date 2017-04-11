namespace Common.DataBase
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using MasterData;
  using Item;
  using System.Linq;
  public class MasterItemDB : DataBase
  {
    [SerializeField]
    MasterItemData master;

    MasterItemData masterClone;

    public override void Init()
    {
      masterClone = Instantiate(master);
      base.Init();
    }

    public ItemData GetData(string _id)
    {
      return masterClone.List.First(d => d.Id == _id);
    }

  }
}