﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//******************************************************
//UserDataObject
//初期データ用オブジェクト
//******************************************************
[CreateAssetMenu(fileName ="UserDataObject",menuName ="CreateScriptableObject/UserDataObject",order = 100)]
public class UserDataObject : ScriptableObject
{
  [SerializeField]
  string userName;
  [SerializeField]
  int userLv;//レベル
  [SerializeField]
  int money;//所持金
  [SerializeField]
  int clearMissionId;
  [SerializeField]
  OrganizationData[] organizationDataArray;//編成データ

  [System.Serializable]
  public class OrganizationData
  {
    public UnitData[] unitDataArray;
  }

  [System.Serializable]
  public class UnitData
  {
    public bool isUse = false;
    public string headId;
    public string weponId;
    public string legId;
    public string accessoryId;
  }

  public string UserName { get { return userName; } }
  public int UserLv { get { return userLv; } }
  public int Money { get { return money; } }
  public OrganizationData[] OrganizationDataArray { get { return organizationDataArray; } }
}
