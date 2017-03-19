namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UniRx;
  using UniRx.Triggers;
  public class RoboController : MonoBehaviour,IRoboController
  {
    Vector3ReactiveProperty moveVec = new Vector3ReactiveProperty();

    Vector3 targetPos;
    bool isMove = false;

    public void SetTargetPos(Vector3 _pos)
    {
      targetPos = _pos;
      isMove = true;
    }

    // Update is called once per frame
    void Update()
    {
      if (!isMove)
        return;

      Vector3 pos = targetPos - transform.position;
      pos.y = 0;
      if(pos.magnitude < 2)
      {
        isMove = false;
      }

      moveVec.Value = Vector3.Normalize(pos);
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
