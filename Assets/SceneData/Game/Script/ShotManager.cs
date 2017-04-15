namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Common;

  public class ShotManager : SingletonUnityObject<ShotManager>
  {
    [SerializeField]
    GameObject shotSample;

    public void Shot(float _atk,int _range,Vector3 _pos,Vector3 _vec)
    {
      var obj = CreateShot(shotSample);
      obj.GetComponent<Shot>().Fire(_atk, _range, _pos, _vec);
    }

    public GameObject CreateShot(GameObject _obj)
    {
      var obj = Instantiate<GameObject>(_obj);
      return obj;
    }

  }
}
