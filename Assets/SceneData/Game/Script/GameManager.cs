namespace Game
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Common;
  using Game.Robo;
  using UniRx;

  public class GameManager : MonoBehaviour
  {
    [SerializeField]
    RoboParam enemyBase;

    private void Start()
    {
      SceneChanger.Instance.IsInitialize = true;

      enemyBase.CurHp.
        Subscribe(_ => 
        {
          if (_ <= 0)
          {
            SceneChanger.Instance.ChangeScene("Result");
          }
        }).AddTo(gameObject);
    }

  }
}
