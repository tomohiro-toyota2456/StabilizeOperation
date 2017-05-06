namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UniRx;
  using UniRx.Triggers;

  //*********************************************************
  //Shooter
  //ショット攻撃クラス
  //*********************************************************
  public class Shooter : MonoBehaviour
  {
    [SerializeField]
    Transform shotRoot;
    [SerializeField]
    Transform shotBase;
    [SerializeField]
    int range;//射程
    [SerializeField]
    float ctPer;//CT率
    [SerializeField]
    FloatReactiveProperty rapid = new FloatReactiveProperty();
    [SerializeField]
    FloatReactiveProperty atk = new FloatReactiveProperty();
    [SerializeField]
    RoboParam.ShotEffectData shotEffectData;

    public FloatReactiveProperty Rapid { set { rapid = value; } }
    public FloatReactiveProperty Atk { set { atk = value; } }
    public int Range { set { range = value; } }
    public float CtPer { set { ctPer = value; } }
    public Vector3 ShotVec { get; set; }
    public RoboParam.ShotEffectData ShotEffect { set { shotEffectData = value; } }

    float shotEffectVal;//
    float shotEffectTime;


    ShotEffectFunctions.EffectDelSimple effectDelSimple = null;
   // ShotEffectFunctions.EffectDel effectDel;

    float bufRapid = 0;
    public void Fire()
    {
      Vector3 vec = ShotVec;
      vec.y = 0;

      shotBase.rotation = Quaternion.LookRotation(vec);

      //無い場合は変換しておく
      if(effectDelSimple ==null || true)
      {
        ConvertDelFromShotEffectData(shotEffectData);
      }

      if (bufRapid <= 0)
      {
        bufRapid = 1;// rapid.Value;
        ShotManager.Instance.Shot(atk.Value, range,ctPer,shotRoot.transform.position,ShotVec,effectDelSimple);

        this.UpdateAsObservable()
          .TakeWhile(_ => bufRapid >0)
          .Subscribe(
          _ => { bufRapid -= Time.deltaTime; },
          () => bufRapid = 0
          ).AddTo(gameObject);
      }
    }

    public void ConvertDelFromShotEffectData(RoboParam.ShotEffectData _shotEffectData)
    {
      //追加効果がなければ何もしない
      if(_shotEffectData.vType == RoboPartParam.EffectValueType.None)
        return;

      //すでに設定されている場合はしない
      if (effectDelSimple != null)
        return;

      string vStr = _shotEffectData.vType.ToString().Replace("Percent", "");
      vStr = vStr.Replace("Fixed", "");

      float val = float.Parse(vStr);

      if(_shotEffectData.vType.ToString().Contains("Percent"))
      {
        val *= 0.01f;//パーセントなので0.01系の表記に変更
      }

      ShotEffectFunctions.EffectDel effectDel = null;

      //1以下なら割合系のはず
      if (val < 1 && _shotEffectData.paramType != RoboParam.ParamType.Hp)
      {
        if(_shotEffectData.isBuf)
        {
          effectDel = ShotEffectFunctions.AddBuff;
        }
        else
        {
          effectDel = ShotEffectFunctions.AddDebuff;
        }
      }
      else if(val < 1 && _shotEffectData.paramType == RoboParam.ParamType.Hp)
      {
        if (_shotEffectData.isBuf)
        {
          effectDel = ShotEffectFunctions.RepairPer;
        }
        else
        {
          effectDel = ShotEffectFunctions.DamagePer;
        }
      }
      else if(_shotEffectData.paramType == RoboParam.ParamType.Hp)//固定値系が許されているのが現在HP系のみ
      {
        if(_shotEffectData.isBuf)
        {
          effectDel = ShotEffectFunctions.RepairFixed;
        }
        else
        {
          effectDel = ShotEffectFunctions.DamageFixed;
        }
      }

      shotEffectVal = val;
      shotEffectTime = float.Parse(_shotEffectData.tType.ToString().Replace("Sec", ""));

      float time = shotEffectTime;
      var type = _shotEffectData.paramType;

      effectDelSimple = (enemy) =>
      {
        effectDel(enemy, val, time,type);
      };
      

    }

  }
}
