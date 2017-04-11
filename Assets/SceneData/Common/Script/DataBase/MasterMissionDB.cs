namespace Common.DataBase
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using MasterData;
  using Mission;
  using System.Linq;

  public class MasterMissionDB : DataBase
  {
    [SerializeField]
    MasterMissionData master;

    MasterMissionData masterClone;

    public override void Init()
    {
      masterClone = Instantiate(master);
      base.Init();
    }

    public MissionData GetData(string _missionId)
    {
      return masterClone.List.First(d => d.MissionId == _missionId);
    }

    public MissionData[] GetDataArray(int _chapterId)
    {
      return masterClone.List.Where(d => d.ChapterId == _chapterId).ToArray();
    }

    public string GetChapterName(int _chapterId)
    {
      return masterClone.ChapterNameArray[_chapterId - 1];
    }
  }
}
