namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UniRx;
  using UniRx.Triggers;
  //****************************************
  //RoboParam
  //ロボットのパラメータ定義
  //****************************************
  public class RoboParam : MonoBehaviour
  {
    [SerializeField]
    int hp;//体力
    [SerializeField]
    float def;//防御
    [SerializeField]
    float atk;//攻撃
    [SerializeField]
    float rapid;//連射
    [SerializeField]
    int range;//射程
    [SerializeField]
    float spd;//速度
    [SerializeField]
    int cost;//コスト
    [SerializeField]
    int load;//荷重
    [SerializeField]
    int weight;//重さ
    [SerializeField]
    int fov;//視野　ロボからの視界半径とする？
    [SerializeField]
    float[] Resistance = new float[3];
    [SerializeField]
    LegType legType;
    [SerializeField]
    ShotType shotType;



    public enum ParamType
    {
      Hp,
      Def,
      Rapid,
      Spd,
      Atk
    }

    public enum LegType
    {
      Land,
      Sky,
    }

    public enum ShotType
    {
      Bullet,
      Explosion,
      Laser
    }

    FloatReactiveProperty curHp = new FloatReactiveProperty();//現在HP
    FloatReactiveProperty curDef;//現在防御
    FloatReactiveProperty curRapid = new FloatReactiveProperty();//現在射撃速度
    FloatReactiveProperty curSpd = new FloatReactiveProperty();//現在速度
    FloatReactiveProperty curAtk = new FloatReactiveProperty();

    float correctPerDef = 1.0f;//補正%防御
    float correctPerRapid = 1.0f;//補正%
    float correctPerSpd = 1.0f;//補正速度
    float correctPerAtk = 1.0f;

    //プロパティ群
    public int Hp { get { return hp; } set { hp = value; } }
    public float Def { get { return def; } set { def = value; } }
    public float Atk { get { return atk; } set { atk = value; } }
    public float Rapid { get { return rapid; } set { rapid = value; } }
    public int Range { get { return range; } set { range = value; } }
    public float Spd { get { return spd; } set { spd = value; } }
    public int Cost { get { return cost; } set { cost = value; } }
    public int Load { get { return load; } set { load = value; } }
    public int Weight { get { return weight; } set { weight = value; } }
    public int Fov { get { return fov; } set { fov = value; } }

    public FloatReactiveProperty CurHp { get { return curHp; } }
    public FloatReactiveProperty CurDef { get { return curDef; } }
    public FloatReactiveProperty CurRapid { get { return curRapid; } }
    public FloatReactiveProperty CurSpd { get { return curSpd; } }
    public FloatReactiveProperty CurAtk { get { return curAtk; } }

    private void Start()
    {
      CurHp.Value = hp;
      CurSpd.Value = spd;
      curAtk.Value = atk;
      curRapid.Value = rapid;
    }

    public void CalHp(float _val)
    {
      CurHp.Value += _val;
    }

    //バフ・デバフをかけます
    //かける対象パラメータ　数値(2で２倍 0.5で半分) 時間 秒
    public void CreateEffectParam(ParamType _type,float _value,float _time,float _timePriod = 1.0f)
    {
      switch(_type)
      {
        case ParamType.Hp:

          Observable.Timer(System.TimeSpan.FromSeconds(0), System.TimeSpan.FromSeconds(_timePriod))
            .TakeWhile(_ => _ < _time)
            .Subscribe(_ =>
            {
              CurHp.Value += _value;
            }).AddTo(gameObject);

          break;

        case ParamType.Def:

          correctPerDef += _value;
          CurDef.Value = Def * correctPerDef;
          Observable.Timer(System.TimeSpan.FromSeconds(_time))
            .Subscribe(_ => 
            {
              correctPerDef += -_value;
              CurDef.Value = Def * correctPerDef;
            }).AddTo(gameObject);


          break;

        case ParamType.Rapid:
          correctPerRapid += _value;
          CurRapid.Value = Rapid * correctPerRapid;
          Observable.Timer(System.TimeSpan.FromSeconds(_time))
            .Subscribe(_ =>
            {
              correctPerRapid += -_value;
              CurRapid.Value = Rapid * correctPerRapid;
            }).AddTo(gameObject);

          break;

        case ParamType.Spd:
          correctPerSpd += _value;
          CurSpd.Value = Spd * correctPerSpd;
          Observable.Timer(System.TimeSpan.FromSeconds(_time))
            .Subscribe(_ =>
            {
              correctPerSpd += -_value;
              CurSpd.Value = spd * correctPerSpd;
            }).AddTo(gameObject); 
          break;

        case ParamType.Atk:
          correctPerAtk += _value;
          CurAtk.Value = atk * correctPerAtk;
          Observable.Timer(System.TimeSpan.FromSeconds(_time))
            .Subscribe(_ =>
            {
              correctPerAtk += -_value;
              CurAtk.Value = atk * correctPerAtk;
            }).AddTo(gameObject); 

          break;
      }
    }
  }
}