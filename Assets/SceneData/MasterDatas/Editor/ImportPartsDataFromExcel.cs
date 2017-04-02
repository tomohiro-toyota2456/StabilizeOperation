namespace EditorTool
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEditor;
  using Excel;
  using Common.MasterData;
  using Game.Robo;

  public class ImportPartsDataFromExcel : EditorWindow
  {
    static string excelPath = "Assets/SceneData/MasterDatas/Editor/PartDatas.xlsx";
    static string masterPartsDataPath = "Assets/SceneData/MasterDatas/MasterPartsData/MasterPartsData.asset";
    static string basePath = "Assets/SceneData/MasterDatas/PartsData/";
    [MenuItem("ExcelImporter/OpenImportWindow")]
    static public void OpenWindow()
    {
      GetWindow<ImportPartsDataFromExcel>();
    }

    private void OnGUI()
    {
      using (new EditorGUILayout.HorizontalScope())
      {
        EditorGUILayout.LabelField("PartsDataExcelPath:");
        excelPath = EditorGUILayout.TextField(excelPath);
      }

      using (new EditorGUILayout.VerticalScope())
      {
        if(GUILayout.Button("ImportPartsData"))
        {
          ImportPartsData();
        }
      }
    }

    static void ImportPartsData()
    {
      ExcelReader excelReader = new ExcelReader();
      if (!excelReader.Open(excelPath))
      {
        Debug.Log("ExcelRead Failed!!!");
        return;
      }

      var masterPartsData = CreateInstance<MasterPartsData>();
      masterPartsData.HeadDataList = ImportHeadParts(excelReader);
      masterPartsData.WeponDataList = ImportWeponParts(excelReader);
      masterPartsData.LegDataList = ImportLegParts(excelReader);
      masterPartsData.AccessoryDataList = ImportAccessoryParts(excelReader);

      AssetDatabase.CreateAsset(masterPartsData, masterPartsDataPath);
      AssetDatabase.Refresh();

      Debug.Log("FnishCreateMasterPartsData!!!");
    }

    static List<RoboPartParam> ImportHeadParts(ExcelReader _excelReader)
    {
      _excelReader.SetSheet("Head");
      List<RoboPartParam> headList = new List<RoboPartParam>();
      int cnt = 1;
      //Headパーツ読み込み
      while (true)
      {
        var data = _excelReader.GetCellData(cnt, 0);

        if (data == null)
        {
          break;
        }

        var partData = CreateInstance<RoboPartParam>();

        string id = data;
        int hp = int.Parse(_excelReader.GetCellData(cnt, 1));
        int def = int.Parse(_excelReader.GetCellData(cnt, 2));
        int atk = int.Parse(_excelReader.GetCellData(cnt, 3));
        int rapid = int.Parse(_excelReader.GetCellData(cnt, 4));
        int spd = int.Parse(_excelReader.GetCellData(cnt, 5));
        int weight = int.Parse(_excelReader.GetCellData(cnt, 6));
        int cost = int.Parse(_excelReader.GetCellData(cnt, 7));
        int fov = int.Parse(_excelReader.GetCellData(cnt, 8));
        float regist1 = float.Parse(_excelReader.GetCellData(cnt, 9));
        float regist2 = float.Parse(_excelReader.GetCellData(cnt, 10));
        float regist3 = float.Parse(_excelReader.GetCellData(cnt, 11));
        string text = _excelReader.GetCellData(cnt, 12);

        float[] regists = new float[3];
        regists[0] = regist1;
        regists[1] = regist2;
        regists[2] = regist3;

        partData.Id = id;
        partData.Hp = hp;
        partData.Def = def;
        partData.Atk = atk;
        partData.Rapid = rapid;
        partData.Spd = spd;
        partData.Weight = weight;
        partData.Cost = cost;
        partData.Fov = fov;
        partData.Regist = regists;
        partData.Dist = text;
        partData.RoboType = RoboPartParam.PartType.Head;

        AssetDatabase.CreateAsset(partData, basePath + "Head/" + id + ".asset");
        AssetDatabase.Refresh();
        headList.Add(partData);

        cnt++;
      }

      return headList;
    }

    static List<RoboPartParam> ImportWeponParts(ExcelReader _excelReader)
    {
      _excelReader.SetSheet("Wepon");
      List<RoboPartParam> weponList = new List<RoboPartParam>();
      int cnt = 1;
      //Headパーツ読み込み
      while (true)
      {
        var data = _excelReader.GetCellData(cnt, 0);

        if (data == null)
        {
          break;
        }

        var partData = CreateInstance<RoboPartParam>();

        string id = data;
        int hp = int.Parse(_excelReader.GetCellData(cnt, 1));
        int def = int.Parse(_excelReader.GetCellData(cnt, 2));
        int atk = int.Parse(_excelReader.GetCellData(cnt, 3));
        int rapid = int.Parse(_excelReader.GetCellData(cnt, 4));
        int spd = int.Parse(_excelReader.GetCellData(cnt, 5));
        int weight = int.Parse(_excelReader.GetCellData(cnt, 6));
        int cost = int.Parse(_excelReader.GetCellData(cnt, 7));
        int range = int.Parse(_excelReader.GetCellData(cnt, 8));
        int shotType = int.Parse(_excelReader.GetCellData(cnt, 9));
        int addEffectType = int.Parse(_excelReader.GetCellData(cnt, 10));
        string text = _excelReader.GetCellData(cnt, 11);

        partData.Id = id;
        partData.Hp = hp;
        partData.Def = def;
        partData.Atk = atk;
        partData.Rapid = rapid;
        partData.Spd = spd;
        partData.Weight = weight;
        partData.Cost = cost;
        partData.Range = range;
        partData.EffectType = (RoboPartParam.AddEffectType)addEffectType;
        partData.RoboAttribute = (RoboPartParam.PartAttribute)(10 + shotType);
        partData.Dist = text;
        partData.RoboType = RoboPartParam.PartType.Wepon;

        AssetDatabase.CreateAsset(partData, basePath + "Wepon/" + id + ".asset");
        AssetDatabase.Refresh();
        weponList.Add(partData);

        cnt++;
      }

      return weponList;
    }

    static List<RoboPartParam> ImportLegParts(ExcelReader _excelReader)
    {
      _excelReader.SetSheet("Leg");
      List<RoboPartParam> legList = new List<RoboPartParam>();
      int cnt = 1;
      //Headパーツ読み込み
      while (true)
      {
        var data = _excelReader.GetCellData(cnt, 0);

        if (data == null)
        {
          break;
        }

        var partData = CreateInstance<RoboPartParam>();

        string id = data;
        int hp = int.Parse(_excelReader.GetCellData(cnt, 1));
        int def = int.Parse(_excelReader.GetCellData(cnt, 2));
        int atk = int.Parse(_excelReader.GetCellData(cnt, 3));
        int rapid = int.Parse(_excelReader.GetCellData(cnt, 4));
        int spd = int.Parse(_excelReader.GetCellData(cnt, 5));
        int weight = int.Parse(_excelReader.GetCellData(cnt, 6));
        int cost = int.Parse(_excelReader.GetCellData(cnt, 7));
        int load = int.Parse(_excelReader.GetCellData(cnt, 8));
        int legType = int.Parse(_excelReader.GetCellData(cnt, 9));
        int addEffectType = int.Parse(_excelReader.GetCellData(cnt, 10));
        string text = _excelReader.GetCellData(cnt, 11);


        partData.Id = id;
        partData.Hp = hp;
        partData.Def = def;
        partData.Atk = atk;
        partData.Rapid = rapid;
        partData.Spd = spd;
        partData.Weight = weight;
        partData.Cost = cost;
        partData.Load = load;
        partData.EffectType = (RoboPartParam.AddEffectType)addEffectType;
        partData.RoboAttribute = (RoboPartParam.PartAttribute)legType;
        partData.Dist = text;
        partData.RoboType = RoboPartParam.PartType.Leg;

        AssetDatabase.CreateAsset(partData, basePath + "Leg/" + id + ".asset");
        AssetDatabase.Refresh();
        legList.Add(partData);

        cnt++;
      }

      return legList;
    }

    static List<RoboPartParam> ImportAccessoryParts(ExcelReader _excelReader)
    {
      _excelReader.SetSheet("Accessory");
      List<RoboPartParam> accessoryList = new List<RoboPartParam>();
      int cnt = 1;
      //Headパーツ読み込み
      while (true)
      {
        var data = _excelReader.GetCellData(cnt, 0);

        if (data == null)
        {
          break;
        }

        var partData = CreateInstance<RoboPartParam>();

        string id = data;
        int hp = int.Parse(_excelReader.GetCellData(cnt, 1));
        int def = int.Parse(_excelReader.GetCellData(cnt, 2));
        int atk = int.Parse(_excelReader.GetCellData(cnt, 3));
        int rapid = int.Parse(_excelReader.GetCellData(cnt, 4));
        int spd = int.Parse(_excelReader.GetCellData(cnt, 5));
        int weight = int.Parse(_excelReader.GetCellData(cnt, 6));
        int cost = int.Parse(_excelReader.GetCellData(cnt, 7));
        int range = int.Parse(_excelReader.GetCellData(cnt, 8));
        int load = int.Parse(_excelReader.GetCellData(cnt, 9));
        int addEffectType = int.Parse(_excelReader.GetCellData(cnt, 10));
        string text = _excelReader.GetCellData(cnt, 11);


        partData.Id = id;
        partData.Hp = hp;
        partData.Def = def;
        partData.Atk = atk;
        partData.Rapid = rapid;
        partData.Spd = spd;
        partData.Weight = weight;
        partData.Cost = cost;
        partData.Load = load;
        partData.EffectType = (RoboPartParam.AddEffectType)addEffectType;
        partData.Dist = text;
        partData.RoboType = RoboPartParam.PartType.Accessory;

        AssetDatabase.CreateAsset(partData, basePath + "Accessory/" + id + ".asset");
        AssetDatabase.Refresh();
        accessoryList.Add(partData);

        cnt++;
      }

      return accessoryList;
    }



  }
}
