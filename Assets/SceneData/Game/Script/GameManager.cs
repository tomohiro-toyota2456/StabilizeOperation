namespace Game
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Common;
  using Game.Robo;
  using UniRx;
  using UnityEngine.SceneManagement;
  using UniRx.Triggers;

  public class GameManager : MonoBehaviour
  {
    [SerializeField]
    RoboParam enemyBase;

    static int stageId = 1;
    public static int StageId { set { stageId = value; } }

    private void Start()
    {
      LoadStage();

      enemyBase.CurHp.
        Subscribe(_ => 
        {
          if (_ <= 0)
          {
            SceneChanger.Instance.ChangeScene("Result");
          }
        }).AddTo(gameObject);
    }

    void LoadStage()
    {
      string name = "Stage" + stageId.ToString();

      var op = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

      this.UpdateAsObservable()
        .TakeWhile(_ => !op.isDone)
        .Subscribe(_ => {}, () =>
          {
            SceneChanger.Instance.IsInitialize = true;
          });

    }

  }
}
