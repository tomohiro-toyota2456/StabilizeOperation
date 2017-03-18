namespace Game.Debug
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Game.Robo;
  using UniRx;

  public class DebugController : MonoBehaviour,IRoboController
  {
    Vector3ReactiveProperty moveVec = new Vector3ReactiveProperty();

    void Update()
    {
      if(Input.GetKey(KeyCode.W))
      {
        moveVec.Value = new Vector3(0, 0, 1);
      }
      else
      {
        moveVec.Value = new Vector3(0, 0, 0);
      }
    }

    public IObservable<Vector3> Move()
    {
      return moveVec.AsObservable<Vector3>();
    }

    public bool Attack()
    {
      return true;
    }

  }
}
