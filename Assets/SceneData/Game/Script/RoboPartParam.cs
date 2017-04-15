namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  //****************************************
  //RoboPartParam
  //ロボットのパーツのパラメータ定義
  //****************************************
  public class RoboPartParam : ScriptableObject
  {
    //ロボパーツの部位
    public enum PartType
    {
      Head,
      Leg,
      Wepon,
      Accessory
    }

    //パーツ属性　基本脚部に使う
    //Air 飛行系
    //Land 陸系
    
    //桁で意味合いを分ける
    public enum PartAttribute
    {
      None =0,
      Air,
      Land,

      Bullet =11,
      Explosion,
      Laser
    }

    public enum AddEffectType
    {
      None,

    }

    [SerializeField]
    string id;
    [SerializeField]
    PartType partType;//部位タイプ
    [SerializeField]
    PartAttribute partAttribute;//部位属性
    [SerializeField]
    AddEffectType addEffectType;//追加効果
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
    int shotSpd;//弾の速さ
    [SerializeField]
    float[] regist = new float[3];//耐性
    [SerializeField]
    int fov;//視野　ロボからの視界半径とする？
    [SerializeField]
    string dist;
    [SerializeField]
    string name;
    [SerializeField]
    int ctPer;//クリティカル率　ここでは1~100で行う
    


    //プロパティ群
    public string Id { get { return id; } set { id = value; } } 
    public int Hp     { get { return hp; }     set { hp = value; } }
    public int Def    { get { return def; }    set { def = value; } }
    public int Atk    { get { return atk; }    set { atk = value; } }
    public int Rapid  { get { return rapid; }  set { rapid = value; } }
    public int Range  { get { return range; }  set { range = value; } }
    public int Spd    { get { return spd; }    set { spd = value; } }
    public int Cost   { get { return cost; }   set { cost = value; } }
    public int Load   { get { return load; }   set { load = value; } }
    public int Weight { get { return weight; } set { weight = value; } }
    public int Fov    { get { return fov; }    set { fov = value; } }
    public float[] Regist { get { return regist; } set { regist = value; } }
    public string Dist { get { return dist; } set { dist = value; } }
    public string Name { get { return name; } set { name = value; } }
    public int CtPer { get { return ctPer; }set { ctPer = value; } }
    
    public PartType RoboType { get { return partType; }set { partType = value; } }
    public PartAttribute RoboAttribute { get { return partAttribute; } set { partAttribute = value; } }
    public AddEffectType EffectType { get { return addEffectType; } set { addEffectType = value; } }
  }
}
