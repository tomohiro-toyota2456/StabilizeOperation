namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Common.DataBase;
  using Common.ResourceLoader;
  using Common;

  //ユニット生成機構
  public class UnitFactory : MonoBehaviour
  {
    [SerializeField]
    GameObject roboBase;

    MasterPartsDB masterPartsDB;
    ResourceLoader resourceLoader;
    HavePartsDB havePartsDB;
    ExpTableDB expTableDB;

    //Player
    UserDB userDB;
    UserDataObject.OrganizationData playerDeck;
  
    // Use this for initialization
    void Start()
    {
      userDB = DataBaseManager.Instance.GetDataBase<UserDB>();
      masterPartsDB =DataBaseManager.Instance.GetDataBase<MasterPartsDB>();
      resourceLoader = ResourceLoader.Instance;
      havePartsDB = DataBaseManager.Instance.GetDataBase<HavePartsDB>();
      expTableDB = DataBaseManager.Instance.GetDataBase<ExpTableDB>();

      playerDeck = userDB.GetUsingDeck();
    }

    public bool CheckUsingPlayerUnitData(int _idx)
    {
      if (playerDeck.unitDataArray[_idx] == null)
      {
        return false;
      }

      return playerDeck.unitDataArray[_idx].isUse;
    }

    string GetPlayerUnitName(int _idx)
    {
      if (playerDeck.unitDataArray[_idx] == null)
      {
        return "NODATA";
      }

      if(playerDeck.unitDataArray[_idx].isUse)
      {
        return playerDeck.unitDataArray[_idx].unitName;
      }

      return "NOUSE";
    }

    public GameObject CreatePlayerUnit(int _idx)
    {
      //ベース作成
      var rbase = Create(roboBase);

      //編成データからID取得
      string headId = playerDeck.unitDataArray[_idx].headId;
      string weponId = playerDeck.unitDataArray[_idx].weponId;
      string legId = playerDeck.unitDataArray[_idx].legId;
      string accId = playerDeck.unitDataArray[_idx].accessoryId;

      //データ取得
      var param3 = masterPartsDB.GetData(headId);
      var param2 = masterPartsDB.GetData(weponId);
      var param1 = masterPartsDB.GetData(legId);

      //生成
      var robj  = resourceLoader.LoadPartResource(param1.Id);
      var robj2 = resourceLoader.LoadPartResource(param2.Id);
      var robj3 = resourceLoader.LoadPartResource(param3.Id);

      var obj = Create(robj);
      var obj2 = Create(robj2);
      var obj3 = Create(robj3);


      //親子関係＋接続
      rbase.transform.SetParent(this.transform);

      var con = obj.GetComponent<ConnectObject>().ConnectRoot;
      var con2 = obj2.GetComponent<ConnectObject>().ConnectRoot;

      obj.transform.SetParent(rbase.transform);
      obj2.transform.SetParent(con.transform);
      obj3.transform.SetParent(con2.transform);

      obj2.transform.localPosition = new Vector3(0, 0, 0);
      obj3.transform.localPosition = new Vector3(0, 0, 0);

      rbase.GetComponent<RoboController>().shooter = obj2.GetComponent<Shooter>();

      //ステータス計算
      RoboParam param = rbase.GetComponent<RoboParam>();

      SetParam(param, headId, weponId, legId, accId);

      Shooter shooter = obj2.GetComponent<Shooter>();
      shooter.Atk = rbase.GetComponent<RoboParam>().CurAtk;
      shooter.Range = rbase.GetComponent<RoboParam>().Range;
      shooter.Rapid = rbase.GetComponent<RoboParam>().CurRapid;

      return rbase;
    }

    void SetParam(RoboParam _roboParam,string _headId,string _weponId,string _legId,string _accessoryId)
    {
      RoboPartParam baseHead  = masterPartsDB.GetData(_headId);
      RoboPartParam baseWepon = masterPartsDB.GetData(_weponId);
      RoboPartParam baseLeg   = masterPartsDB.GetData(_legId);
      int headLv = havePartsDB.GetData(_headId).lv;
      int weponLv = havePartsDB.GetData(_weponId).lv;
      int legLv = havePartsDB.GetData(_legId).lv;
      ExpTableDB.ParamData headData = expTableDB.GetParam(_headId, headLv);
      ExpTableDB.ParamData weponData = expTableDB.GetParam(_weponId, weponLv);
      ExpTableDB.ParamData legData = expTableDB.GetParam(_legId, legLv);

      _roboParam.Hp    += baseHead.Hp + baseWepon.Hp + baseLeg.Hp+ headData.hp + weponData.hp + legData.hp;
      _roboParam.Atk   += baseHead.Atk + baseWepon.Atk + baseLeg.Atk + headData.atk + weponData.atk + legData.atk;
      _roboParam.Def   += baseHead.Def + baseWepon.Def + baseLeg.Def + headData.def + weponData.def + legData.def;
      _roboParam.Spd   += baseHead.Spd + baseWepon.Spd + baseLeg.Spd + headData.spd + weponData.spd + legData.spd;
      _roboParam.Rapid += baseHead.Rapid + baseWepon.Rapid + baseLeg.Rapid + headData.rapid + weponData.rapid + legData.rapid;
      _roboParam.Range += baseHead.Range + baseWepon.Range + baseLeg.Range + headData.range + weponData.range + legData.range;
      _roboParam.Load +=  baseHead.Load + baseWepon.Load + baseLeg.Load + headData.load + weponData.load + legData.load;
      _roboParam.Fov   += baseHead.Fov + baseWepon.Fov + baseLeg.Fov + headData.fov + weponData.fov + legData.fov;
      _roboParam.Weight+= baseHead.Weight + baseWepon.Weight + baseLeg.Weight + headData.weight + weponData.weight + legData.weight;

      int ctper = 0;
        
      ctper += baseHead.CtPer + baseWepon.CtPer + baseLeg.CtPer;

      if(!string.IsNullOrEmpty(_accessoryId))
      {
        RoboPartParam baseAccessory = masterPartsDB.GetData(_accessoryId);
        int accessoryLv = havePartsDB.GetData(_accessoryId).lv;
        ExpTableDB.ParamData accessoryData = expTableDB.GetParam(_accessoryId, accessoryLv);
        _roboParam.Hp    += baseAccessory.Hp + accessoryData.hp;
        _roboParam.Atk   += baseAccessory.Atk + accessoryData.atk;
        _roboParam.Def   += baseAccessory.Def + accessoryData.def;
        _roboParam.Spd   += baseAccessory.Spd + accessoryData.spd ;
        _roboParam.Rapid += baseAccessory.Rapid + accessoryData.rapid ;
        _roboParam.Range += baseAccessory.Range + accessoryData.range;
        _roboParam.Load  += baseAccessory.Load + accessoryData.load ;
        _roboParam.Fov   += baseAccessory.Fov + accessoryData.fov;
        _roboParam.Cost  += baseAccessory.Cost;
        _roboParam.Weight += baseAccessory.Weight;

        ctper += baseAccessory.CtPer;
      }

      _roboParam.CtPer = (float)ctper / 100.0f;
      
      _roboParam.Resistance = baseHead.Resistance;
      _roboParam.Cost += baseHead.Cost + baseWepon.Cost + baseLeg.Cost;

      _roboParam.Leg = GameCommon.ConvertLegTypeFromPartData(baseLeg.RoboAttribute);
      _roboParam.Shot = GameCommon.ConvertShotTypeFromPartData(baseWepon.RoboAttribute);
    }

    GameObject Create(GameObject _prefab)
    {
      GameObject obj = Instantiate<GameObject>(_prefab);
      obj.transform.position = new Vector3(0, 0, 0);
      return obj;
    }

  }
}
