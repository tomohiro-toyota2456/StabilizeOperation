namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UniRx;
  using UniRx.Triggers;
  using Common;

  public class Shot : MonoBehaviour
  {
    [SerializeField]
    Collider collider;
    [SerializeField]
    Rigidbody rdbody;
    float atk;
    int range;
    float ctPer;

    ShotEffectFunctions.EffectDelSimple effectDel = null;
 
    //追加効果ない場合
    public void Fire(float _atk, int _range,float _ctPer,Vector3 _pos, Vector3 _vec)
    {
      atk = _atk;
      range = _range;
      transform.position = _pos;
      ctPer = _ctPer;

      GameObject obj = this.gameObject;

      Vector3 add = Vector3.zero;
      Vector3 oldPos = transform.position;
      this.UpdateAsObservable()
        .TakeWhile(_ => add.magnitude <= range)
        .Subscribe(_ =>
        {
          add += transform.position - oldPos;
          oldPos = transform.position;
          rdbody.velocity = _vec;
        },
        ()=>Destroy(gameObject))
        .AddTo(gameObject);

      SetHitBehavior(null);

    }

    public void Fire(float _atk, int _range, float _ctPer, Vector3 _pos, Vector3 _vec, ShotEffectFunctions.EffectDelSimple _effectDel)
    {
      atk = _atk;
      range = _range;
      ctPer = _ctPer;
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
        () => Destroy(gameObject))
        .AddTo(gameObject);

      SetHitBehavior(_effectDel);

    }

    void SetHitBehavior(ShotEffectFunctions.EffectDelSimple _effectDel)
    {
      effectDel = _effectDel;

      collider.OnTriggerEnterAsObservable()
        .Subscribe((_) =>
        {
          if (_.gameObject.tag == "Enemy")
          {
            var param = _.gameObject.GetComponent<RoboParam>();

            GameCommon.CalDamage(param, atk, ctPer);

            if (effectDel != null)
            {
              effectDel(param);
            }

            Destroy(gameObject);
          }

        }).AddTo(gameObject);
    }


  }
}