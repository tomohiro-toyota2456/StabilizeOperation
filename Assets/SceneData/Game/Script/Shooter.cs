namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UniRx;
  using UniRx.Triggers;

  //*********************************************************
  //Shooter
  //ショット攻撃クラス
  //*********************************************************
  public class Shooter : MonoBehaviour
  {
    [SerializeField]
    Transform shotRoot;
    [SerializeField]
    Transform shotBase;
    [SerializeField]
    int range;//射程
    [SerializeField]
    FloatReactiveProperty rapid = new FloatReactiveProperty();
    [SerializeField]
    FloatReactiveProperty atk = new FloatReactiveProperty();

    public FloatReactiveProperty Rapid { set { rapid = value; } }
    public FloatReactiveProperty Atk { set { atk = value; } }
    public int Range { set { range = value; } }
    public Vector3 ShotVec { get; set; }


    float bufRapid = 0;
    public void Fire()
    {
      Vector3 vec = ShotVec;
      vec.y = 0;

      shotBase.rotation = Quaternion.LookRotation(vec);

      if (bufRapid <= 0)
      {
        bufRapid = rapid.Value;
        ShotManager.Instance.Shot(atk.Value, range, shotRoot.transform.position,ShotVec);

        this.UpdateAsObservable()
          .TakeWhile(_ => bufRapid >0)
          .Subscribe(
          _ => { bufRapid -= Time.deltaTime; },
          () => bufRapid = 0
          ).AddTo(gameObject);
      }
    }

  }
}
