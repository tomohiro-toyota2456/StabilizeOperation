namespace Common.DataBase
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using MasterData;
  using Shop;
  using System.Linq;
  //*********************************************************
  //ShopDB
  //ショップデータ格納用　データ変更を基本行わない
  //*********************************************************
  public class ShopDB : DataBase
  {
    [SerializeField]
    MasterShopData master;

    public override void Init()
    {

    }

    //*******************************************************
    //GetData
    //ids : partId配列　配列内のIDに合致しないものだけを返す
    //*******************************************************
    public List<ProductData> GetHeadData(string [] ids = null)
    {
      List<ProductData> list;
      if(ids != null)
      {
        list = master.HeadPartProductList.Where(p => !ids.Any(id => id == p.ItemId)).ToList();
      }
      else
      {
        list = master.HeadPartProductList;
      }

      return list;
    }

    public List<ProductData> GetWeponData(string[] ids = null)
    {
      List<ProductData> list;
      if (ids != null)
      {
        list = master.WeponPartProductList.Where(p => !ids.Any(id => id == p.ItemId)).ToList();
      }
      else
      {
        list = master.WeponPartProductList;
      }

      return list;
    }

    public List<ProductData> GetLegData(string[] ids = null)
    {
      List<ProductData> list;
      if (ids != null)
      {
        list = master.LegPartProductList.Where(p => !ids.Any(id => id == p.ItemId)).ToList();
      }
      else
      {
        list = master.LegPartProductList;
      }

      return list;
    }

    public List<ProductData> GetAccessoryData(string[] ids = null)
    {
      List<ProductData> list;
      if (ids != null)
      {
        list = master.AccessoryProductList.Where(p => !ids.Any(id => id == p.ItemId)).ToList();
      }
      else
      {
        list = master.AccessoryProductList;
      }

      return list;
    }


  }
}