namespace Unit
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Common;

  //UnitSceneとしたのはゲームでUnit系のスクリプトを作ることが多いため
  //混同しないように
  public class UnitSceneManager : MonoBehaviour
  {
    // Use this for initialization
    void Start()
    {
      SceneChanger.Instance.IsInitialize = true;
    }
  }
}
