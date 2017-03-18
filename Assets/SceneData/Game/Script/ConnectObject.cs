namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  //ConnectObject
  //接続先のオブジェクトを保存するだけ
  public class ConnectObject : MonoBehaviour
  {
    [SerializeField]
    GameObject connectRoot;
    [SerializeField]
    GameObject connectLeft;
    [SerializeField]
    GameObject connectRight;

    public GameObject ConnectRoot { get { return connectRoot; } }
    public GameObject ConnectLeft { get { return connectLeft; } }
    public GameObject ConnectRight { get { return connectRight; } }
  }
}
