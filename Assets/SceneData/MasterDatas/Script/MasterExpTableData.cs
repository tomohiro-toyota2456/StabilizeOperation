namespace Exp
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class MasterExpTableData : ScriptableObject
  {
    [SerializeField]
    List<ExpTableData> headPartExpList;
    [SerializeField]
    List<ExpTableData> weponPartExpList;
    [SerializeField]
    List<ExpTableData> legPartExpList;
    [SerializeField]
    List<ExpTableData> accessoryPartExpList;

    public List<ExpTableData> HeadPartExpList { get { return headPartExpList; }set { headPartExpList = value; } }
    public List<ExpTableData> WeponPartExpList { get { return weponPartExpList; } set { weponPartExpList = value; } }
    public List<ExpTableData> LegPartExpList { get { return legPartExpList; } set { legPartExpList = value; } }
    public List<ExpTableData> AccessoryPartExpList { get { return accessoryPartExpList; } set { accessoryPartExpList = value; } }
  }
}