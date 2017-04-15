namespace Item
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  //*********************************************
  //ItemData
  //アイテムデータ
  //*********************************************
  public class ItemData : ScriptableObject
  {
    public enum ItemType
    {
      Exp,
    }

    [SerializeField]
    string id;
    [SerializeField]
    string name;
    [SerializeField]
    ItemType type;//アイテム形式　
    [SerializeField]
    int effect;//効果量
    [SerializeField]
    string dist;//アイテム説明分

    public string Id { get { return id; } set { id = value; } }
    public string Name { get { return name; }set { name = value; } }
    public ItemType Type { get { return type; }set { type = value; } }
    public string Dist { get { return dist; } set { dist = value; } }
    public int Effect { get { return effect; }set { effect = value; } }
  }
}