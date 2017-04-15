namespace Common
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.SceneManagement;

  //Awakeで初期化する奴がなければアタッチするだけでいけそう
  //順番が気になる場合は参照をもってチェック関数呼ぶ感じで
  public class DebugLoader : MonoBehaviour
  {
    private void Awake()
    {
      if(!IsLoadedSceneManager())
      {
        Load();
      }
    }

    //SceneManagerがロード済みかどうか調べる
    public bool IsLoadedSceneManager()
    {
      var scene =  SceneManager.GetSceneByName("SceneManager");

      if(scene.IsValid())
      {
        return true;
      }


      return false;
    }

    public void Load()
    {
      StartCoroutine(load());
    }

    IEnumerator load()
    {
      var op = SceneManager.LoadSceneAsync("SceneManager", LoadSceneMode.Additive);

      while (!op.isDone)
      {
        yield return null;
      }

      List<string> sceneNameList = new List<string>();

      //今ロードされているシーンを取得
      for (int i = 0; i < SceneManager.sceneCount; i++)
      {
        var scene = SceneManager.GetSceneAt(i);
        if (scene.IsValid() && scene.name != "SceneManager")
        {
          sceneNameList.Add(scene.name);
        }
      }

      op = SceneManager.UnloadSceneAsync(sceneNameList[0]);
      SceneChanger.Instance.ChangeScene(sceneNameList[0]);
      while (!op.isDone)
      {
        yield return null;
      }

      Debug.Log("Unload");
    }
  }

}