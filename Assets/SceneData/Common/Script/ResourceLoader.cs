namespace Common.ResourceLoader
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class ResourceLoader : SingletonUnityObject<ResourceLoader>
  {

    //引数にはパーツIDを入れる
    //パーツのオブジェクトを返す
    public GameObject LoadPartResource(string _id)
    {
      string path = GetDirNameFromId(_id)+"/";
      return Resources.Load<GameObject>(path + _id);
    }

    //_pathにはResourcesからパーツ毎のフォルダの前までを入れる
    //例 Texture/
    public T LoadPartResource<T>(string _path, string _id) where T : UnityEngine.Object
    {
      string path = _path + GetDirNameFromId(_id) +"/";
      return Resources.Load<T>(path + _id);
    }

    //パーツのIDからHead/Wepon/Leg/Accessoryの文字列を返す
    public string GetDirNameFromId(string _id)
    {
      string sub = _id.Substring(0, 1);

      string ans = "";
      switch (sub)
      {
        case "h":
          ans = "Head";
          break;

        case "w":
          ans = "Wepon";
          break;

        case "l":
          ans = "Leg";
          break;

        case "a":
          ans = "Accessory";
          break;
      }

      return ans;

    }
  }

}