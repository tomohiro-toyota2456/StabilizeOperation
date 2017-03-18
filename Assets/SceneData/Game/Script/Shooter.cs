namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UniRx;

  //*********************************************************
  //Shooter
  //ショット攻撃クラス
  //*********************************************************
  public class Shooter : MonoBehaviour
  {
    [SerializeField]
    Transform shotRoot;
    [SerializeField]
    int range;//射程
    [SerializeField]
    FloatReactiveProperty rapid;
    [SerializeField]
    FloatReactiveProperty atk;

    public FloatReactiveProperty Rapid { set { rapid = value; } }
    public FloatReactiveProperty Atk { set { atk = value; } }
    public int Range { set { range = value; } }

    public void Fire()
    {

    }

  }
}
