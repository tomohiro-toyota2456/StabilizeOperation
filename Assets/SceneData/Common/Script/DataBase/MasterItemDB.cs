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

    public ItemData GetData(string _id)
    {
      return master.List.First(d => d.Id == _id);
    }

  }
}