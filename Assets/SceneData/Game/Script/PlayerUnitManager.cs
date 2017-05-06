namespace Game
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.UI;
  using Robo;
  using TMPro;
  using UniRx;
  using UniRx.Triggers;
  using Common;

  //ユニットの生成管理を行う
  public class PlayerUnitManager : MonoBehaviour
  {
    [SerializeField]
    UnitFactory unitFactory;
    [SerializeField]
    Button[] buttonArray = new Button[10];//生成用ボタン
    [SerializeField]
    TextMeshProUGUI[] buttonTextArray = new TextMeshProUGUI[10];
    [SerializeField]
    TextMeshProUGUI fundText;
    [SerializeField]
    Gauge fundGauge;
    [SerializeField]
    GameTouchAction gameTouchAction;
    [SerializeField]
    Camera camera;

    bool isGamePlay = true;

    List<GameObject> playerUnitList = new List<GameObject>();
    List<GameObject> selectList = new List<GameObject>();

    public bool IsGamePlay { set { isGamePlay = value; } }

    float fundNum;//ユニット生成に使う
    // Use this for initialization
    void Start()
    {
      gameTouchAction.EndDragDel = Select;
      gameTouchAction.TouchDel = MovePoint;

      //ゲーム中資金を増やす
      this.UpdateAsObservable()
        .Where(_ => isGamePlay)
        .Subscribe(_ =>
        {
          fundNum += GameCommon.AddFundOneSec * Time.deltaTime;

          if(fundNum > GameCommon.maxFund)
          {
            fundNum = GameCommon.maxFund;
          }

          fundText.text = fundNum.ToString();
          fundGauge.SetValue(fundNum, GameCommon.maxFund);

        });

      for(int i = 0; i < GameCommon.unitMax;i++)
      {
        //名前取得（ない場合は規定の文字をもらえる)
        buttonTextArray[i].text = unitFactory.GetPlayerUnitName(i);
        if (unitFactory.CheckUsingPlayerUnitData(i))
        {
          int cost = unitFactory.GetUnitCost(i);
          int idx = i;
          buttonArray[i].OnClickAsObservable()
            .Where(_=>cost <= fundNum && playerUnitList.Count < GameCommon.maxExistUnit)
            .Subscribe(_ =>
            {
              fundNum -= cost;
              CreateUnit(idx);
            }).AddTo(gameObject);
            
        }
        else
        {
          //無い場合はボタンの無効化
          buttonArray[i].interactable = false;
        }        
      }
    }


    private void Update()
    {
      //test
      if(Input.GetKeyDown( KeyCode.A))
      {
        if(selectList.Count != 0)
        {
          selectList[0].GetComponent<RoboController>().TestShot(new Vector3(0, 0, 1));
        }
      }
    }

    public void CreateUnit(int _idx)
    {
      var obj = unitFactory.CreatePlayerUnit(_idx);

      playerUnitList.Add(obj);
    }


    public void Select(Vector2 posSt, Vector2 posEd)
    {
      selectList.Clear();
      for (int i = 0; i < playerUnitList.Count; i++)
      {
        if (CheckHit(camera.WorldToScreenPoint(playerUnitList[i].transform.position), posSt, posEd))
        {
          selectList.Add(playerUnitList[i]);
        }
      }
    }

    bool CheckHit(Vector2 pos, Vector2 st, Vector2 ed)
    {
      if (pos.x > st.x && pos.x < ed.x)
      {
        if (pos.y < st.y && pos.y > ed.y)
        {
          return true;
        }
      }

      return false;
    }

    public void MovePoint(Vector2 _pos)
    {
      if (selectList == null)
      {
        return;
      }

      RaycastHit hit;

      if (Physics.Raycast(camera.ScreenPointToRay(_pos), out hit, 1000))
      {

      }

      for (int i = 0; i < selectList.Count; i++)
      {
        selectList[i].GetComponent<RoboController>().SetTargetPos(hit.point);
      }
    }


  }
}
