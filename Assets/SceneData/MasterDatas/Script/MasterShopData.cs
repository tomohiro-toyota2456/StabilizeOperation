namespace Common.MasterData
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Shop;

  public class MasterShopData : ScriptableObject
  {
    [SerializeField]
    List<ProductData> headPartProductList;
    [SerializeField]
    List<ProductData> weponPartProductList;
    [SerializeField]
    List<ProductData> legPartProductList;
    [SerializeField]
    List<ProductData> accessoryPartProductList;

    public List<ProductData> HeadPartProductList { get { return headPartProductList; }set { headPartProductList = value; } }
    public List<ProductData> WeponPartProductList { get { return weponPartProductList; } set { weponPartProductList = value; } }
    public List<ProductData> LegPartProductList { get { return legPartProductList; }set { legPartProductList = value; } }
    public List<ProductData> AccessoryProductList { get { return accessoryPartProductList; }set { accessoryPartProductList = value; } }
  }
}