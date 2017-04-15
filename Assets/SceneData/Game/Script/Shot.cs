namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UniRx;
  using UniRx.Triggers;

  public class Shot : MonoBehaviour
  {
    [SerializeField]
    Collider collider;
    float atk;
    int range;
  
    public void Fire(float _atk, int _range, Vector3 _pos, Vector3 _vec)
    {
      atk = _atk;
      range = _range;
      transform.position = _pos;

      GameObject obj = this.gameObject;

      Vector3 add = Vector3.zero;
      this.UpdateAsObservable()
        .TakeWhile(_ => add.magnitude <= range)
        .Subscribe(_ =>
        {
          add += _vec;
          transform.position += _vec;
        },
        ()=>Destroy(gameObject))
        .AddTo(gameObject);

      collider.OnTriggerEnterAsObservable()
        .Subscribe((_) =>
        {
          if(_.gameObject.tag == "Enemy")
          {
            _.gameObject.GetComponent<RoboParam>().CalHp(-atk);
            Destroy(gameObject);
          }

        }).AddTo(gameObject);

    }
 
  }
}