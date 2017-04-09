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

    public override void Init()
    {
      base.Init();
    }

    public MissionData GetData(string _missionId)
    {
      return master.List.First(d => d.MissionId == _missionId);
    }

    public MissionData[] GetDataArray(int _chapterId)
    {
      return master.List.Where(d => d.ChapterId == _chapterId).ToArray();
    }

    public string GetChapterName(int _chapterId)
    {
      return master.ChapterNameArray[_chapterId - 1];
    }
  }
}
