namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UniRx;
  using UnityEngine.AI;

  public class RoboMover : MonoBehaviour
  { 
    [SerializeField]
    Rigidbody rbody;
    [SerializeField]
    RoboParam roboParam;
    [SerializeField]
    NavMeshAgent agent;//地上ユニットのみ使う予定


    IRoboController controller;
    public IRoboController RoboController { set { controller = value; } }
    public Rigidbody Rbody { set { rbody = value; } }
    public RoboParam RoboParam {set { roboParam = value; } }
    // Use this for initialization
    void Start()
    {
      controller = GetComponent<IRoboController>();
      roboParam.CurSpd.Subscribe(_ =>
      {
        agent.speed = _;
      }).AddTo(gameObject);

      IObservable<Vector3> ob = controller.Move();

      if(ob != null)
      {
          ob
          .Subscribe(_ =>
          {
            if (_.magnitude != 0)
            {
              //transform.rotation = Quaternion.LookRotation(_);
              agent.SetDestination(_);
            }
           //rbody.velocity = roboParam.CurSpd.Value * _;
          });
      }
    }
  }
}