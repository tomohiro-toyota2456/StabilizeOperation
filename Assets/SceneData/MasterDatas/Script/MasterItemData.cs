namespace Common.MasterData
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Item;

  public class MasterItemData : ScriptableObject
  {
    [SerializeField]
    List<ItemData> list;

    public List<ItemData> List { get { return list; }set { list = value; } }
  }
}