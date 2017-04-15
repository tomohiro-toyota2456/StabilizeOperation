namespace Shop
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  //ショップの商品データ
  public class ProductData : ScriptableObject
  {
    [SerializeField]
    int productId;
    [SerializeField]
    string itemId;//商品データのID w00001など
    [SerializeField]
    int price;
    [SerializeField]
    int needLevel;//商品が並ぶための条件レベル

    public int ProductId { get { return productId; } set { productId = value; } }
    public string ItemId { get { return itemId; }set { itemId = value; } }
    public int Price { get { return price; }set { price = value; } }
    public int NeedLevel { get { return needLevel; } set { needLevel = value; } }
  }
}