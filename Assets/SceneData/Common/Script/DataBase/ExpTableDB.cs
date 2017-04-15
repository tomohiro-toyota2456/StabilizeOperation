namespace Common.DataBase
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Exp;
  using Common.MasterData;
  using System.Linq;
  using Common;
  public class ExpTableDB : DataBase
  {
    [SerializeField]
    MasterExpTableData master;

    public struct ParamData
    {
      public int hp;//体力
      public int def;//防御
      public int atk;//攻撃
      public int rapid;//連射
      public int range;//射程
      public int spd;//速度
      public int cost;//コスト
      public int load;//荷重
      public int weight;//重さ
      public int fov;//視野　ロボからの視界半径とする？
      public int ctPer;//クリティカル率　ここでは1~100で行う
    }

    public ExpTableData GetData(string _id)
    {
      string headStr = _id.Substring(0, 1);
      ExpTableData data = null;
      switch (headStr)
      {
        case "h":
          data = GetHeadData(_id);
          break;

        case "w":
          data = GetWeponData(_id);
          break;

        case "l":
          data = GetLegData(_id);
          break;

        case "a":
          data = GetAccessoryData(_id);
          break;
      }

      return data;
    }

    public ExpTableData GetHeadData(string _id)
    {
      return master.HeadPartExpList.First(data => data.Id == _id);
    }

    public ExpTableData GetWeponData(string _id)
    {
      return master.WeponPartExpList.First(data => data.Id == _id);
    }

    public ExpTableData GetLegData(string _id)
    {
      return master.LegPartExpList.First(data => data.Id == _id);
    }

    public ExpTableData GetAccessoryData(string _id)
    {
      return master.AccessoryPartExpList.First(data => data.Id == _id);
    }

    ParamData GetParam(string _id,int curLv)
    {
      ParamData data = new ParamData();

      float t = CompletionFunctions.LvConvertCompVal(curLv, GameConstParam.maxLv);

      ExpTableData tData = GetData(_id);

      CompletionFunctions.CompFunc func = null;
      switch(tData.Type)
      {
        case ExpTableData.GrowthType.Fast:
          func = CompletionFunctions.Sin;
          break;

        case ExpTableData.GrowthType.Normal:
          func = CompletionFunctions.Liner;
          break;

        case ExpTableData.GrowthType.Slow:
          func = CompletionFunctions.InvSin;
          break;
      }

      data.atk = CompletionFunctions.Comp(tData.Atk, t, func);
      data.cost= CompletionFunctions.Comp(tData.Cost, t, func);
      data.ctPer= CompletionFunctions.Comp(tData.CtPer, t, func);
      data.def= CompletionFunctions.Comp(tData.Def, t, func);
      data.fov = CompletionFunctions.Comp(tData.Fov, t, func);
      data.hp = CompletionFunctions.Comp(tData.Hp, t, func);
      data.load= CompletionFunctions.Comp(tData.Load, t, func);
      data.range = CompletionFunctions.Comp(tData.Range, t, func);
      data.rapid= CompletionFunctions.Comp(tData.Rapid, t, func);
      data.spd = CompletionFunctions.Comp(tData.Spd, t, func);
      data.weight = CompletionFunctions.Comp(tData.Weight, t, func);

      return data;
    }

  }
}