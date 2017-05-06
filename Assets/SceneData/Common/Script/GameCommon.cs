namespace Common
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Game.Robo;

  public static class GameCommon
  {
    //最大レベル
    static public readonly int maxLv = 100;

    //１デッキに使う最大ユニット数
    static public readonly int unitMax = 10;

    static public readonly int maxExistUnit = 20;//

    //1秒あたりに増える資金
    static public readonly int AddFundOneSec = 50;
    //最大資金
    static public readonly int maxFund = 9999;

    //ユーザーが最大レベルになるまでの経験値
    static public readonly int userMaxExp = 999999;

    //継続ダメージ間隔
    static public readonly float DamageInterval = 1.5f;

    static public void CalDamage(RoboParam _targetParam,float _atk,float _ctPer)
    {
      float per = Random.Range(0, _ctPer);
      float ctAtk = 100 * per;
      float damage = (_atk + ctAtk - _targetParam.CurDef.Value * DetermineNumberFromSpd(_targetParam.CurSpd.Value)) + 4;

      damage = damage < 0 ? 0 : damage;

      _targetParam.CalHp(-damage);
    }

    //速度から確定数を決定する
    static int DetermineNumberFromSpd(float _spd)
    {
      //鈍足
      if(_spd <= 30)
      {
        return 2;
      }
      else if(_spd <= 60)
      {
        return 4;
      }
      else if(_spd <= 90)
      {
        return 6;
      }

      return 7;
    }

    //ロボパーツデータのPartAttributeからLegTypeに変換する
    static public RoboParam.LegType ConvertLegTypeFromPartData(RoboPartParam.PartAttribute _attr)
    {
      RoboParam.LegType type = RoboParam.LegType.Land;

      switch(_attr)
      {
        case RoboPartParam.PartAttribute.Air:
          type = RoboParam.LegType.Air;
          break;

        case RoboPartParam.PartAttribute.Land:
          type = RoboParam.LegType.Land;
          break;
      }

      return type;
    }

    //ロボパーツデータのPartAttrobuteからShotTypeに変換する
    static public RoboParam.ShotType ConvertShotTypeFromPartData(RoboPartParam.PartAttribute _attr)
    {
      RoboParam.ShotType shotType = RoboParam.ShotType.Bullet;

      switch(_attr)
      {
        case RoboPartParam.PartAttribute.Bullet:
          shotType = RoboParam.ShotType.Bullet;
          break;

        case RoboPartParam.PartAttribute.Explosion:
          shotType = RoboParam.ShotType.Explosion;
          break;

        case RoboPartParam.PartAttribute.Laser:
          shotType = RoboParam.ShotType.Laser;
          break;
      }

      return shotType;
    }
  }
}