namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using System;

  //追加効果用関数置き場
  static  public class ShotEffectFunctions 
  {
    public delegate void EffectDel(RoboParam _enemy, float _val, float _time, RoboParam.ParamType _paramType);
    public delegate void EffectDelSimple(RoboParam _enemy);
 
    static public void AddDebuff(RoboParam _enemy,float _val,float _time, RoboParam.ParamType _paramType)
    {
      //無い場合は無効
      if (_paramType == RoboParam.ParamType.Max)
        return;

      //デバフ
      _enemy.AddDebuff(_paramType, _val, _time);
    }

    static public void AddBuff(RoboParam _enemy, float _val, float _time, RoboParam.ParamType _paramType)
    {
      //無い場合は無効
      if (_paramType == RoboParam.ParamType.Max)
        return;

      //バフ
      _enemy.AddBuff(_paramType, _val, _time);
    }

    //HP継続ダメージ
    static public void DamageFixed(RoboParam _enemy,float _val,float _time,RoboParam.ParamType _paramType = RoboParam.ParamType.Max)
    {
      _enemy.AddDebuffHp(_val, _time);
    }

    //HP継続回復
    static public void RepairFixed(RoboParam _enemy,float _val,float _time,RoboParam.ParamType _paramType = RoboParam.ParamType.Max)
    {
      _enemy.AddBuffHp(_val, _time);
    }

    //割合ダメージ
    static public void DamagePer(RoboParam _enemy, float _val, float _time, RoboParam.ParamType _paramType = RoboParam.ParamType.Max)
    {
      float val = _enemy.Hp * _val;
      _enemy.AddDebuffHp(val, _time);
    }

    //割合回復
    static public void RepairPer(RoboParam _enemy, float _val, float _time, RoboParam.ParamType _paramType = RoboParam.ParamType.Max)
    {
      float val = _enemy.Hp * _val;
      _enemy.AddBuffHp(val, _time);
    }
  }
}