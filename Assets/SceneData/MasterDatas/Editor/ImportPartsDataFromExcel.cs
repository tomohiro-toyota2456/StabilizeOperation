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

  public class ImportPartsDataFromExcel : EditorWindow
  {
    static string excelPath = "Assets/SceneData/MasterDatas/Editor/PartDatas.xlsx";
    static string shopExcelPath = "Assets/SceneData/MasterDatas/Editor/ShopDatas.xlsx";
    static string expTableExcelPath = "Assets/SceneData/MasterDatas/Editor/ExpTable.xlsx";
    static string masterPartsDataPath = "Assets/SceneData/MasterDatas/MasterPartsData/MasterPartsData.asset";
    static string masterShopDataPath = "Assets/SceneData/MasterDatas/MasterShopData/MasterShopData.asset";
    static string masterExpTablePath = "Assets/SceneData/MasterDatas/MasterExpTableData/MasterExpTableData.asset";
    static string basePath = "Assets/SceneData/MasterDatas/";
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

      using (new EditorGUILayout.HorizontalScope())
      {
        EditorGUILayout.LabelField("ExpTableExcelPath:");
        shopExcelPath = EditorGUILayout.TextField(expTableExcelPath);
      }

      using (new EditorGUILayout.VerticalScope())
      {
        if (GUILayout.Button("ImportExpTableData"))
        {
          ImportExpTableData();
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
        partData.Regist = regists;
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


          AssetDatabase.CreateAsset(product, basePath+"ProductsData/"+sheetNameArray[i]+"/"+productId+".asset");
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

      Debug.Log("FnishCreateMasterShopData!!!");

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
          break;
        case "w":
          return "Wepon";
          break;
        case "l":
          return "Leg";
          break;
        case "a":
          return "Accessory";
          break;       
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


  }
}
