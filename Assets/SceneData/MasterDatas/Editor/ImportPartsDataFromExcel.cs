namespace EditorTool
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEditor;
  using Excel;
  using Common.MasterData;
  using Game.Robo;
  using Shop;
  using Exp;
  using Item;
  using Mission;
  using EnemyUnitData;

  public class ImportPartsDataFromExcel : EditorWindow
  {
    static string excelPath = "Assets/SceneData/MasterDatas/Editor/PartDatas.xlsx";
    static string shopExcelPath = "Assets/SceneData/MasterDatas/Editor/ShopDatas.xlsx";
    static string expTableExcelPath = "Assets/SceneData/MasterDatas/Editor/ExpTable.xlsx";
    static string itemExcelPath = "Assets/SceneData/MasterDatas/Editor/ItemData.xlsx";
    static string missionExcelPath = "Assets/SceneData/MasterDatas/Editor/MissionData.xlsx";
    static string enemyUnitExcelPath= "Assets/SceneData/MasterDatas/Editor/EnemyUnitData.xlsx";

    static string masterPartsDataPath = "Assets/SceneData/MasterDatas/MasterPartsData/MasterPartsData.asset";
    static string masterShopDataPath = "Assets/SceneData/MasterDatas/MasterShopData/MasterShopData.asset";
    static string masterExpTablePath = "Assets/SceneData/MasterDatas/MasterExpTableData/MasterExpTableData.asset";
    static string masterItemDataPath= "Assets/SceneData/MasterDatas/MasterItemData/MasterItemData.asset";
    static string masterMissionDataPath = "Assets/SceneData/MasterDatas/MasterMissionData/MasterMissionData.asset";
    static string masterEnemyUnitDataPath= "Assets/SceneData/MasterDatas/MasterEnemyUnitData/MasterEnemyUnitData.asset";

    static string basePath = "Assets/SceneData/MasterDatas/";
    [MenuItem("ExcelImporter/OpenImportWindow")]
    static public void OpenWindow()
    {
      GetWindow<ImportPartsDataFromExcel>();
    }

    private void OnGUI()
    {
      //パーツデータ
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


      //ショップデータ
      using (new EditorGUILayout.HorizontalScope())
      {
        EditorGUILayout.LabelField("ShopDataExcelPath:");
        shopExcelPath = EditorGUILayout.TextField(shopExcelPath);
      }

      using (new EditorGUILayout.VerticalScope())
      {
        if (GUILayout.Button("ImportShopData"))
        {
          ImportShopData();
        }
      }


      //経験値データ
      using (new EditorGUILayout.HorizontalScope())
      {
        EditorGUILayout.LabelField("ExpTableExcelPath:");
        expTableExcelPath = EditorGUILayout.TextField(expTableExcelPath);
      }

      using (new EditorGUILayout.VerticalScope())
      {
        if (GUILayout.Button("ImportExpTableData"))
        {
          ImportExpTableData();
        }
      }

      //アイテムデータ
      using (new EditorGUILayout.HorizontalScope())
      {
        EditorGUILayout.LabelField("ItemDataExcelPath:");
        itemExcelPath = EditorGUILayout.TextField(itemExcelPath);
      }

      using (new EditorGUILayout.VerticalScope())
      {
        if (GUILayout.Button("ImportItemData"))
        {
          ImportItemData();
        }
      }

      //ミッションデータ
      using (new EditorGUILayout.HorizontalScope())
      {
        EditorGUILayout.LabelField("MissionDataExcelPath:");
        missionExcelPath = EditorGUILayout.TextField(missionExcelPath);
      }

      using (new EditorGUILayout.VerticalScope())
      {
        if (GUILayout.Button("ImportMissionData"))
        {
          ImportMissionData();
        }
      }


      //敵ユニットデータ
      using (new EditorGUILayout.HorizontalScope())
      {
        EditorGUILayout.LabelField("EnemyUnitDataExcelPath:");
        enemyUnitExcelPath = EditorGUILayout.TextField(enemyUnitExcelPath);
      }

      using (new EditorGUILayout.VerticalScope())
      {
        if (GUILayout.Button("ImportEnemyUnitData"))
        {
          ImportEnemyUnitData();
        }
      }
    }

    #region Parts
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
        string name = _excelReader.GetCellData(cnt, 1);
        int hp = int.Parse(_excelReader.GetCellData( cnt, 2));
        int def = int.Parse(_excelReader.GetCellData(cnt, 3));
        int atk = int.Parse(_excelReader.GetCellData(cnt, 4));
        int rapid = int.Parse(_excelReader.GetCellData(cnt, 5));
        int spd = int.Parse(_excelReader.GetCellData(cnt, 6));
        int weight = int.Parse(_excelReader.GetCellData(cnt, 7));
        int cost = int.Parse(_excelReader.GetCellData(cnt, 8));
        int fov = int.Parse(_excelReader.GetCellData(cnt, 9));
        float regist1 = float.Parse(_excelReader.GetCellData(cnt, 10));
        float regist2 = float.Parse(_excelReader.GetCellData(cnt, 11));
        float regist3 = float.Parse(_excelReader.GetCellData(cnt, 12));
        string text = _excelReader.GetCellData(cnt, 13);

        float[] regists = new float[3];
        regists[0] = regist1;
        regists[1] = regist2;
        regists[2] = regist3;

        partData.Name = name;
        partData.Id = id;
        partData.Hp = hp;
        partData.Def = def;
        partData.Atk = atk;
        partData.Rapid = rapid;
        partData.Spd = spd;
        partData.Weight = weight;
        partData.Cost = cost;
        partData.Fov = fov;
        partData.Resistance = regists;
        partData.Dist = text;
        partData.RoboType = RoboPartParam.PartType.Head;

        AssetDatabase.CreateAsset(partData, basePath +"PartsData/"+ "Head/" + id + ".asset");
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
        string name = _excelReader.GetCellData(cnt, 1);
        int hp = int.Parse(_excelReader.GetCellData(cnt, 2));
        int def = int.Parse(_excelReader.GetCellData(cnt, 3));
        int atk = int.Parse(_excelReader.GetCellData(cnt, 4));
        int rapid = int.Parse(_excelReader.GetCellData(cnt, 5));
        int spd = int.Parse(_excelReader.GetCellData(cnt, 6));
        int weight = int.Parse(_excelReader.GetCellData(cnt, 7));
        int cost = int.Parse(_excelReader.GetCellData(cnt, 8));
        int range = int.Parse(_excelReader.GetCellData(cnt, 9));
        int shotType = int.Parse(_excelReader.GetCellData(cnt, 10));
        int addEffectType = int.Parse(_excelReader.GetCellData(cnt, 11));
        string text = _excelReader.GetCellData(cnt, 12);
        int ctPer = int.Parse(_excelReader.GetCellData(cnt, 13));

        partData.Name = name;
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
        partData.CtPer = ctPer;

        AssetDatabase.CreateAsset(partData, basePath + "PartsData/" + "Wepon/" + id + ".asset");
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
        string name = _excelReader.GetCellData(cnt, 1);
        int hp = int.Parse(_excelReader.GetCellData(cnt, 2));
        int def = int.Parse(_excelReader.GetCellData(cnt, 3));
        int atk = int.Parse(_excelReader.GetCellData(cnt, 4));
        int rapid = int.Parse(_excelReader.GetCellData(cnt, 5));
        int spd = int.Parse(_excelReader.GetCellData(cnt, 6));
        int weight = int.Parse(_excelReader.GetCellData(cnt, 7));
        int cost = int.Parse(_excelReader.GetCellData(cnt, 8));
        int load = int.Parse(_excelReader.GetCellData(cnt, 9));
        int legType = int.Parse(_excelReader.GetCellData(cnt, 10));
        int addEffectType = int.Parse(_excelReader.GetCellData(cnt, 11));
        string text = _excelReader.GetCellData(cnt, 12);

        partData.Name = name;
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

        AssetDatabase.CreateAsset(partData, basePath + "PartsData/" + "Leg/" + id + ".asset");
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
        string name = _excelReader.GetCellData(cnt, 1);
        int hp = int.Parse(_excelReader.GetCellData(cnt, 2));
        int def = int.Parse(_excelReader.GetCellData(cnt, 3));
        int atk = int.Parse(_excelReader.GetCellData(cnt, 4));
        int rapid = int.Parse(_excelReader.GetCellData(cnt, 5));
        int spd = int.Parse(_excelReader.GetCellData(cnt, 6));
        int weight = int.Parse(_excelReader.GetCellData(cnt, 7));
        int cost = int.Parse(_excelReader.GetCellData(cnt, 8));
        int range = int.Parse(_excelReader.GetCellData(cnt, 9));
        int load = int.Parse(_excelReader.GetCellData(cnt, 10));
        int addEffectType = int.Parse(_excelReader.GetCellData(cnt, 11));
        string text = _excelReader.GetCellData(cnt, 12);


        partData.Name = name;
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

        AssetDatabase.CreateAsset(partData, basePath + "PartsData/" + "Accessory/" + id + ".asset");
        AssetDatabase.Refresh();
        accessoryList.Add(partData);

        cnt++;
      }

      return accessoryList;
    }

    #endregion

    #region Shop
    static void ImportShopData()
    {
      ExcelReader excelReader = new ExcelReader();
      if (!excelReader.Open(shopExcelPath))
      {
        Debug.Log("ExcelRead Failed!!!");
        return;
      }

      var masterShopData = CreateInstance<MasterShopData>();

      string []sheetNameArray = new string[4];
      sheetNameArray[0] = "Head";
      sheetNameArray[1] = "Wepon";
      sheetNameArray[2] = "Leg";
      sheetNameArray[3] = "Accessory";

      List<ProductData>[] productLists = new List<ProductData>[sheetNameArray.Length];
      
      for(int i = 0; i < sheetNameArray.Length; i++)
      {
        productLists[i] = new List<ProductData>();

        excelReader.SetSheet(sheetNameArray[i]);

        int cnt = 1;
        while(true)
        {
          string str = excelReader.GetCellData(cnt, 0);

          if(str == null)
          {
            break;
          }

          var product = CreateInstance<ProductData>();

          int productId = int.Parse(str);
          string itemId = excelReader.GetCellData(cnt, 1);
          int price = int.Parse(excelReader.GetCellData(cnt, 2));
          int needLv = int.Parse(excelReader.GetCellData(cnt, 3));

          product.ProductId = productId;
          product.ItemId = itemId;
          product.Price = price;
          product.NeedLevel = needLv;


          AssetDatabase.CreateAsset(product, basePath+"ProductsData/"+sheetNameArray[i]+"/"+"s_"+productId+".asset");
          AssetDatabase.Refresh();
          productLists[i].Add(product);
          cnt++;
        }

      }

      masterShopData.HeadPartProductList = productLists[0];
      masterShopData.WeponPartProductList = productLists[1];
      masterShopData.LegPartProductList = productLists[2];
      masterShopData.AccessoryProductList = productLists[3];

      AssetDatabase.CreateAsset(masterShopData, masterShopDataPath);
      AssetDatabase.Refresh();

      Debug.Log("FinshCreateMasterShopData!!!");

    }
    #endregion

    #region Exp

    static void ImportExpTableData()
    {
      ExcelReader excelReader = new ExcelReader();
      if (!excelReader.Open(expTableExcelPath))
      {
        Debug.Log("ExcelRead Failed!!!");
        return;
      }

      var list1 = ImportExp("HeadExp",excelReader);
      var list2 = ImportExp("WeponExp", excelReader);
      var list3 = ImportExp("LegExp", excelReader);
      var list4 = ImportExp("AccessoryExp", excelReader);

      MasterExpTableData master = CreateInstance<MasterExpTableData>();
      master.HeadPartExpList = list1;
      master.WeponPartExpList = list2;
      master.LegPartExpList = list3;
      master.AccessoryPartExpList = list4;

      AssetDatabase.CreateAsset(master,masterExpTablePath);
      AssetDatabase.Refresh();

      Debug.Log("FinishCreateMasterExpTableData!!!");

    }

    static List<ExpTableData> ImportExp(string _sheetName,ExcelReader _excelReader)
    {
      _excelReader.SetSheet(_sheetName);

      List<ExpTableData> list = new List<ExpTableData>();
      int cnt = 1;
      //Headパーツ読み込み
      while (true)
      {
        var data = _excelReader.GetCellData(cnt, 0);

        if (data == null)
        {
          break;
        }
        string type = _excelReader.GetCellData(cnt, 1);
        int hp = int.Parse(_excelReader.GetCellData(cnt, 2));
        int def = int.Parse(_excelReader.GetCellData(cnt, 3));
        int atk = int.Parse(_excelReader.GetCellData(cnt, 4));
        int rapid = int.Parse(_excelReader.GetCellData(cnt, 5));
        int spd = int.Parse(_excelReader.GetCellData(cnt, 6));
        int weight = int.Parse(_excelReader.GetCellData(cnt, 7));
        int cost = int.Parse(_excelReader.GetCellData(cnt, 8));
        int fov = int.Parse(_excelReader.GetCellData(cnt, 9));
        int range = int.Parse(_excelReader.GetCellData(cnt, 10));
        int load = int.Parse(_excelReader.GetCellData(cnt, 11));
        int ctPer = int.Parse(_excelReader.GetCellData(cnt, 12));

        ExpTableData exptableData = CreateInstance<ExpTableData>();
        exptableData.Id = data;
        exptableData.Hp = hp;
        exptableData.Def = def;
        exptableData.Atk = atk;
        exptableData.Rapid = rapid;
        exptableData.Spd = spd;
        exptableData.Weight = weight;
        exptableData.Cost = cost;
        exptableData.Fov = fov;
        exptableData.Range = range;
        exptableData.Load = load;
        exptableData.CtPer = ctPer;
        exptableData.Type = ConvertGrowthTypeFromStr(type);

        string dir = ConvertDirNameFromId(exptableData.Id);

        AssetDatabase.CreateAsset(exptableData, basePath + "ExpTableData/" + dir + "/" + "exp_"+exptableData.Id + ".asset");
        AssetDatabase.Refresh();

        list.Add(exptableData);

        cnt++;
      }

      return list;
    } 

    static string ConvertDirNameFromId(string _id)
    {
      string headStr = _id.Substring(0,1);
      switch(headStr)
      {
        case "h":
          return "Head";
          case "w":
          return "Wepon";
        case "l":
          return "Leg";
        case "a":
          return "Accessory";  
      }

      Debug.Log("ID Wrong!!!");
      return "";
    }

    static ExpTableData.GrowthType ConvertGrowthTypeFromStr(string _str)
    {
      ExpTableData.GrowthType type = ExpTableData.GrowthType.Fast;
      switch(_str)
      {
        case "早熟":
          type = ExpTableData.GrowthType.Fast;
          break;

        case "普通":
          type = ExpTableData.GrowthType.Normal;
          break;

        case "晩成":
          type = ExpTableData.GrowthType.Slow;
          break;

        default:
          Debug.Log("GrowthType Wrong!!!");
          break;
      }

      return type;
    }

    #endregion

    #region Item

    static void ImportItemData()
    {
      ExcelReader excelReader = new ExcelReader();
      if (!excelReader.Open(itemExcelPath))
      {
        Debug.Log("ExcelRead Failed!!!");
        return;
      }

      var master = CreateInstance<MasterItemData>();
      master.List = ImportItem(excelReader);

      AssetDatabase.CreateAsset(master, masterItemDataPath);

      Debug.Log("FinishCreateMasterItemData!!!");
    }

    static List<ItemData> ImportItem(ExcelReader _excelReader)
    {
      _excelReader.SetSheet("Item");

      int cnt = 1;

      List<ItemData> list = new List<ItemData>();

      while(true)
      {
        string data = _excelReader.GetCellData(cnt, 0);

        if(string.IsNullOrEmpty(data))
        {
          break;
        }

        string name = _excelReader.GetCellData(cnt, 1);
        string type = _excelReader.GetCellData(cnt, 2);
        int effect = int.Parse(_excelReader.GetCellData(cnt, 3));
        string text = _excelReader.GetCellData(cnt, 4);

        var item = CreateInstance<ItemData>();
        item.Id = data;
        item.Name = name;
        item.Dist = text;
        item.Effect = effect;
        item.Type = ConvertItemTypeFromStr(type);

        AssetDatabase.CreateAsset(item, basePath + "ItemsData/"+item.Id + ".asset");
        AssetDatabase.Refresh();
        list.Add(item);
        cnt++;
      }

      return list;
    }

    static ItemData.ItemType ConvertItemTypeFromStr(string _str)
    {
      ItemData.ItemType type = ItemData.ItemType.Exp;
      switch(_str)
      {
        case "Exp":
          type = ItemData.ItemType.Exp;
          break;
      }

      return type;
    }

    #endregion

    #region Misson

    static void ImportMissionData()
    {
      ExcelReader excelReader = new ExcelReader();
      if (!excelReader.Open(missionExcelPath))
      {
        Debug.Log("ExcelRead Failed!!!");
        return;
      }

      var master = CreateInstance<MasterMissionData>();

      master.List = ImportMission(excelReader);
      master.ChapterNameArray = ImportChapterName(excelReader);


      AssetDatabase.CreateAsset(master, masterMissionDataPath);
      AssetDatabase.Refresh();

      Debug.Log("FinshCreateMasterMissionData!!!");

    }

    static string[] ImportChapterName(ExcelReader _excelReader)
    {
      _excelReader.SetSheet("ChapterData");

      int cnt = 1;

      List<string> list = new List<string>();

      while (true)
      {
        string data = _excelReader.GetCellData(cnt, 0);

        if (string.IsNullOrEmpty(data))
        {
          break;
        }

        list.Add(_excelReader.GetCellData(cnt, 1));

        cnt++;
      }

      return list.ToArray();

    }

    static List<MissionData> ImportMission(ExcelReader _excelReader)
    {
      _excelReader.SetSheet("MissionData");

      int cnt = 1;

      List<MissionData> list = new List<MissionData>();

      while (true)
      {
        string data = _excelReader.GetCellData(cnt, 0);

        if (string.IsNullOrEmpty(data))
        {
          break;
        }

        string missionId = _excelReader.GetCellData(cnt, 1);
        string missionName = _excelReader.GetCellData(cnt, 2);

        List<string> rewardIdList = new List<string>();

        for (int i = 0; i < 5; i++)
        {
          int idx = i + 3;
          string id = _excelReader.GetCellData(cnt, idx);
          if (string.IsNullOrEmpty(id))
          {
            break;
          }

          rewardIdList.Add(id);
        }

        List<string> dropIdList = new List<string>();

        for (int i = 0; i < 5; i++)
        {
          int idx = i + 8;
          string id = _excelReader.GetCellData(cnt, idx);
          if (string.IsNullOrEmpty(id))
          {
            break;
          }

          dropIdList.Add(id);
        }

        List<MissionData.EnemyUnitData> eUnitList = new List<MissionData.EnemyUnitData>();
        for (int i = 0; i < 20; i += 2)
        {
          int idx = i + 13;
          string id = _excelReader.GetCellData(cnt, idx);
          if(string.IsNullOrEmpty(id))
          {
            break;
          }

          MissionData.EnemyUnitData eData = new MissionData.EnemyUnitData();
          eData.enemyUnitId = id;
          eData.lv =int.Parse(_excelReader.GetCellData(cnt, idx + 1));

          eUnitList.Add(eData);
        }

        var missionData = CreateInstance<MissionData>();
        missionData.MissionId = missionId;
        missionData.ChapterId = int.Parse(data);
        missionData.Name = missionName;
        missionData.RewardIdArray = rewardIdList.ToArray();
        missionData.DropIdArray = dropIdList.ToArray();
        missionData.EUnitDataArray = eUnitList.ToArray();

        AssetDatabase.CreateAsset(missionData, basePath + "MissionDatas/" + missionData.MissionId + ".asset");
        AssetDatabase.Refresh();

        list.Add(missionData);

        cnt++;

      }

      return list;
     }

    #endregion

    #region EnemyUnit

    static void ImportEnemyUnitData()
    {
      ExcelReader excelReader = new ExcelReader();
      if (!excelReader.Open(enemyUnitExcelPath))
      {
        Debug.Log("ExcelRead Failed!!!");
        return;
      }

      var master = CreateInstance<MasterEnemyUnitData>();
      master.List = ImportEnemyUnit(excelReader);
      AssetDatabase.CreateAsset(master,masterEnemyUnitDataPath);
      AssetDatabase.Refresh();

      Debug.Log("FinishCreateMasterEnemyUnitData");
    }
    
    static List<EnemyUnitData> ImportEnemyUnit(ExcelReader _excelReader)
    {
      _excelReader.SetSheet("EnemyUnitData");

      int cnt = 1;

      List<EnemyUnitData> list = new List<EnemyUnitData>();

      while (true)
      {
        string data = _excelReader.GetCellData(cnt, 0);

        if (string.IsNullOrEmpty(data))
        {
          break;
        }

        List<string> partsList = new List<string>();
        for(int i = 0; i < 4;i++)
        {
          int idx = i + 1;

          string id = _excelReader.GetCellData(cnt, idx);
          if (string.IsNullOrEmpty(id))
          {
            break;
          }

          partsList.Add(id);
        }

        var eUnitData = CreateInstance<EnemyUnitData>();
        eUnitData.EnemyUnitId = data;
        eUnitData.PartIdArray = partsList.ToArray();


        AssetDatabase.CreateAsset(eUnitData, basePath + "EnemyUnitDatas/" + eUnitData.EnemyUnitId + ".asset");
        AssetDatabase.Refresh();

        list.Add(eUnitData);
        cnt++;
      }

      return list;
    }

    #endregion


  }
}
