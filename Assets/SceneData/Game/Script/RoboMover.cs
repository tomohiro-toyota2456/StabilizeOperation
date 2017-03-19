namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UniRx;

  public class RoboMover : MonoBehaviour
  { 
    [SerializeField]
    Rigidbody rbody;
    [SerializeField]
    RoboParam roboParam;

    IRoboController controller;
    public IRoboController RoboController { set { controller = value; } }
    public Rigidbody Rbody { set { rbody = value; } }
    public RoboParam RoboParam {set { roboParam = value; } }
    // Use this for initialization
    void Start()
    {
      controller = GetComponent<IRoboController>();

      IObservable<Vector3> ob = controller.Move();

      if(ob != null)
      {
          ob
          .Subscribe(_ =>
          {
            rbody.velocity = roboParam.CurSpd.Value * _;
          });
      }
    }
  }
}