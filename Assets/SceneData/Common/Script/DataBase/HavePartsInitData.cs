namespace Common.DataBase
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  [CreateAssetMenu(fileName ="HavePartsInitData",menuName ="ScriptableObject/HavePartsInitData",order =100)]
  public class HavePartsInitData : ScriptableObject
  {
    [SerializeField]
    string headPartId;
    [SerializeField]
    string weponPartId;
    [SerializeField]
    string legPartId;

    public string HeadPartId { get { return headPartId; } }
    public string WeponPartId { get { return weponPartId; } }
    public string LegPartId { get { return legPartId; } }
  }
}
