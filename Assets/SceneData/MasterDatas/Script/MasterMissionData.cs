namespace Common.MasterData
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Mission;

  public class MasterMissionData : ScriptableObject
  {
    [SerializeField]
    List<MissionData> list;
    [SerializeField]
    string[] chapterNameArray;

    public List<MissionData> List { get { return list; } set { list = value; } }
    public string[] ChapterNameArray { get { return chapterNameArray; }set { chapterNameArray = value; } }
  }
}