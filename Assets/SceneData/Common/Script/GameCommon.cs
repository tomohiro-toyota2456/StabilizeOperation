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

    //ユーザーが最大レベルになるまでの経験値
    static public readonly int userMaxExp = 999999;
    
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