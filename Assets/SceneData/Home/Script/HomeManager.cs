namespace Home
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Common;

  //ホームの制御用？今のところロードでtrue流すだけ
  public class HomeManager : MonoBehaviour
  {

    // Use this for initialization
    void Start()
    {
      SceneChanger.Instance.IsInitialize = true;
    }

  }
}
