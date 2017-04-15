namespace Exp
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class ExpTableData : ScriptableObject
  {
    public enum GrowthType
    {
      Fast,
      Normal,
      Slow
    }

    [SerializeField]
    string id;
    [SerializeField]
    GrowthType growthType;
    [SerializeField]
    int hp;//体力
    [SerializeField]
    int def;//防御
    [SerializeField]
    int atk;//攻撃
    [SerializeField]
    int rapid;//連射
    [SerializeField]
    int range;//射程
    [SerializeField]
    int spd;//速度
    [SerializeField]
    int cost;//コスト
    [SerializeField]
    int load;//荷重
    [SerializeField]
    int weight;//重さ
    [SerializeField]
    int fov;//視野　ロボからの視界半径とする？
    [SerializeField]
    int ctPer;//クリティカル率　ここでは1~100で行う

    //プロパティ群
    public string Id { get { return id; } set { id = value; } }
    public int Hp { get { return hp; } set { hp = value; } }
    public int Def { get { return def; } set { def = value; } }
    public int Atk { get { return atk; } set { atk = value; } }
    public int Rapid { get { return rapid; } set { rapid = value; } }
    public int Range { get { return range; } set { range = value; } }
    public int Spd { get { return spd; } set { spd = value; } }
    public int Cost { get { return cost; } set { cost = value; } }
    public int Load { get { return load; } set { load = value; } }
    public int Weight { get { return weight; } set { weight = value; } }
    public int Fov { get { return fov; } set { fov = value; } }
    public int CtPer { get { return ctPer; } set { ctPer = value; } }

    public GrowthType Type { get { return growthType; }set { growthType = value; } }
  }
}