namespace Organization
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Common.ResourceLoader;

  public class RoboPartCache
  {
    Dictionary<string, GameObject> partDict = new Dictionary<string, GameObject>();
    
    //パーツをキャッシュしてロードする
    public GameObject LoadPartCache(string _id)
    {
      if(partDict == null)
      {
        partDict = new Dictionary<string, GameObject>();
      }

      if(partDict.ContainsKey(_id))
      {
        return partDict[_id];
      }

      GameObject obj = ResourceLoader.Instance.LoadPartResource(_id);

      if(obj !=null)
      {
        partDict.Add(_id, obj);
        return obj;
      }

      return null;
    }     
  }
}