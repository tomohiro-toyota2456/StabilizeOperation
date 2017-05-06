namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UniRx;
  using UniRx.Triggers;
  using System;
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
    float[] resistance = new float[3];
    [SerializeField]
    LegType legType;
    [SerializeField]
    ShotType shotType;
    [SerializeField]
    float ctPer;
    [SerializeField]
    ShotEffectData shotEffectData = new ShotEffectData();

    EffectData[] debuffArray = new EffectData[ParamType.Max.GetHashCode()];
    EffectData[] buffArray = new EffectData[ParamType.Max.GetHashCode()];

    float[] correctPerArray = new float[ParamType.Max.GetHashCode()];

    EffectData buffHp = new EffectData();
    EffectData debuffHp = new EffectData();

    //バフデバフ管理用
    struct EffectData
    {
      public float val;
      public IDisposable dispose;
    }

    [System.Serializable]
    public struct ShotEffectData
    {
      public RoboPartParam.EffectValueType vType;
      public RoboPartParam.EffectTimeType tType;
      public ParamType paramType;
      public bool isBuf;
    }

    //
    public enum ParamType
    {
      Atk,
      Def,
      Rapid,
      Spd,
      Max,
      Hp,//配列にはいれたくない
    }
    
    //脚部タイプ
    public enum LegType
    {
      Land,
      Air,
    }

    //ショットの形式
    public enum ShotType
    {
      Bullet,
      Explosion,
      Laser
    }

    FloatReactiveProperty curHp = new FloatReactiveProperty();//現在HP
    FloatReactiveProperty curDef = new FloatReactiveProperty();
    FloatReactiveProperty curRapid = new FloatReactiveProperty();//現在射撃速度
    FloatReactiveProperty curSpd = new FloatReactiveProperty();//現在速度
    FloatReactiveProperty curAtk = new FloatReactiveProperty();

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
    public float[] Resistance { get { return resistance; } set { resistance = value; } }
    public float CtPer { get { return ctPer; }set { ctPer = value; } }

    public FloatReactiveProperty CurHp { get { return curHp; } }
    public FloatReactiveProperty CurDef { get { return curDef; } }
    public FloatReactiveProperty CurRapid { get { return curRapid; } }
    public FloatReactiveProperty CurSpd { get { return curSpd; } }
    public FloatReactiveProperty CurAtk { get { return curAtk; } }

    public LegType Leg { get { return legType; }set { legType = value; } }
    public ShotType Shot { get { return shotType; }set { shotType = value; } }
    public ShotEffectData ShotEffect { get { return shotEffectData; } set { shotEffectData = value; } }

    private void Start()
    {
      CurHp.Value = hp;
      CurSpd.Value = spd;
      curAtk.Value = atk;
      curRapid.Value = rapid;

      for(int i = 0; i < correctPerArray.Length;i++)
      {
        correctPerArray[i] = 1.0f;
      }
    }

    public void CalHp(float _val)
    {
      CurHp.Value += _val;
    }

    //バフを追加
    public void AddBuff(ParamType _type,float _val,float _time)
    {
      float timer = 0;

      _val = _val <= 0 ? _val * -1 : _val;//引数がマイナス値の場合は補正する

      int idx = _type.GetHashCode();
      var dispose = buffArray[idx].dispose;

      if(dispose == null)
      {
        correctPerArray[idx] += _val;
        SetCurParam(_type);

        dispose = this.UpdateAsObservable()
                  .Where(_ => true)
                  .TakeWhile(_ => timer <= _time)
                  .Subscribe(_ =>
                  {
                    timer += Time.deltaTime;
                  },
                  () =>
                  {
                    correctPerArray[idx] -= _val;
                    SetCurParam(_type);
                    buffArray[idx].dispose = null;
                  });

        buffArray[idx].dispose = dispose;
        buffArray[idx].val = _val;
      }
      else if(buffArray[idx].val < _val)
      {
        buffArray[idx].dispose.Dispose();
        correctPerArray[idx] -= buffArray[idx].val;

        correctPerArray[idx] += _val;
        SetCurParam(_type);

        dispose = this.UpdateAsObservable()
                  .Where(_ => true)
                  .TakeWhile(_ => timer <= _time)
                  .Subscribe(_ =>
                  {
                    timer += Time.deltaTime;
                  },
                  () =>
                  {
                    correctPerArray[idx] -= _val;
                    SetCurParam(_type);
                    buffArray[idx].dispose = null;
                  });

        buffArray[idx].dispose = dispose;
        buffArray[idx].val = _val;
      }

    }

    //デバフを追加
    public void AddDebuff(ParamType _type, float _val, float _time)
    {
      float timer = 0;

      _val = _val >= 0 ? _val * -1 : _val;//引数がマイナス値の場合は補正する

      int idx = _type.GetHashCode();
      var dispose = debuffArray[idx].dispose;

      if (dispose == null)
      {
        correctPerArray[idx] += _val;
        SetCurParam(_type);
        dispose = this.UpdateAsObservable()
                  .Where(_ => true)
                  .TakeWhile(_ => timer <= _time)
                  .Subscribe(_ =>
                  {
                    timer += Time.deltaTime;
                  },
                  () =>
                  {
                    correctPerArray[idx] -= _val;
                    SetCurParam(_type);
                    debuffArray[idx].dispose = null;
                  });

        debuffArray[idx].dispose = dispose;
        debuffArray[idx].val = _val;
      }
      else if (buffArray[idx].val < _val)
      {
        debuffArray[idx].dispose.Dispose();
        correctPerArray[idx] -= debuffArray[idx].val;

        correctPerArray[idx] += _val;
        SetCurParam(_type);

        dispose = this.UpdateAsObservable()
                  .Where(_ => true)
                  .TakeWhile(_ => timer <= _time)
                  .Subscribe(_ =>
                  {
                    timer += Time.deltaTime;
                  },
                  () =>
                  {
                    correctPerArray[idx] -= _val;
                    SetCurParam(_type);
                    debuffArray[idx].dispose = null;
                  });

        debuffArray[idx].dispose = dispose;
        debuffArray[idx].val = _val;
      }

    }

    //Hpバフを追加
    public void AddBuffHp(float _val,float _time)
    {

      _val = _val <= 0 ? _val * -1 : _val;//引数がマイナス値の場合は補正する
      var dispose = buffHp.dispose;
      float timer = 0;
      if(dispose == null)
      {
        buffHp.dispose = Observable.Interval(TimeSpan.FromSeconds(_time))
          .TakeWhile(_=>timer <= _time)
          .Subscribe(val =>
          {
            timer += Time.deltaTime;
            curHp.Value += _val;
          },
          ()=>
          {
            buffHp.dispose = null;
          }).AddTo(gameObject);
      }
      else if(buffHp.val < _val)
      {
        buffHp.dispose.Dispose();
        buffHp.val = _val;

        buffHp.dispose = Observable.Interval(TimeSpan.FromSeconds(_time))
          .TakeWhile(_ => timer <= _time)
          .Subscribe(val =>
          {
            timer += Time.deltaTime;
            curHp.Value += _val;
          },
          () =>
          {
            buffHp.dispose = null;
          }).AddTo(gameObject);
      }

    }

    //Hpデバフを追加
    public void AddDebuffHp(float _val, float _time)
    {
      _val = _val >= 0 ? _val * -1 : _val;//引数がプラスの場合マイナスにする
      var dispose = buffHp.dispose;

      float damageInterval = Common.GameCommon.DamageInterval;

      int damageCount = (int)(_time / damageInterval);//ダメージ間隔と継続時間から発生回数を算出(端数切捨て)
      if (dispose == null)
      {
        buffHp.dispose = Observable.Interval(TimeSpan.FromSeconds(damageInterval))//Intervalは初回0から回数をカウントアップしてくれる
          .TakeWhile(cnt => cnt < damageCount)//カウントが規定数超えるまで継続
          .Subscribe(val =>
          {
            curHp.Value += _val;
          },
          () =>
          {
            buffHp.dispose = null;
          }).AddTo(gameObject);
      }
      else if (buffHp.val < _val)
      {
        buffHp.dispose.Dispose();
        buffHp.val = _val;

        buffHp.dispose = Observable.Interval(TimeSpan.FromSeconds(damageInterval))
          .TakeWhile(cnt => cnt< damageCount)
          .Subscribe(val =>
          {
            curHp.Value += _val;
          },
          () =>
          {
            buffHp.dispose = null;
          }).AddTo(gameObject);
      }

    }

    public void ResetEffect()
    {
      for(int i = 0;i < buffArray.Length;i++)
      {
        if (buffArray[i].dispose != null)
          buffArray[i].dispose.Dispose();

        if (debuffArray[i].dispose != null)
          debuffArray[i].dispose.Dispose();

        correctPerArray[i] = 1.0f;
      }

      if(buffHp.dispose != null)
      {
        buffHp.dispose.Dispose();
      }

      if (debuffHp.dispose != null)
      {
        debuffHp.dispose.Dispose();
      }
    }

    void SetCurParam(ParamType _type)
    {
      switch(_type)
      {
        case ParamType.Atk:
          curAtk.Value = atk * correctPerArray[_type.GetHashCode()];
          break;

        case ParamType.Def:
          curDef.Value = def * correctPerArray[_type.GetHashCode()];
          break;

        case ParamType.Rapid:
          curRapid.Value = rapid * correctPerArray[_type.GetHashCode()];
          break;

        case ParamType.Spd:
          curSpd.Value = spd * correctPerArray[_type.GetHashCode()];
          break;
      }
    }


  }
}