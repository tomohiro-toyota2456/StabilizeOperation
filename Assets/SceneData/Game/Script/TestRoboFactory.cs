using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Robo;
using Common.DataBase;
public class TestRoboFactory : MonoBehaviour
{
  [SerializeField]
  Game.GameTouchAction touchAction;
  [SerializeField]
  Camera camera;
  List<GameObject> objList = new List<GameObject>();
  [SerializeField]
  UnitFactory unitFactory;

  List<GameObject> selectList;

  private void Start()
  {
    touchAction.EndDragDel = Select;
    touchAction.TouchDel = MovePoint;
  }

  private void Update()
  {
    Vector3 enemyBase = new Vector3(0, 0.41f, 40.52f);
    for(int i = 0; i < objList.Count; i++)
    {
      Vector3 vec = enemyBase - objList[i].transform.position;

      if(vec.magnitude <= objList[i].GetComponent<RoboParam>().Range)
      {
        objList[i].GetComponent<RoboController>().TestShot(Vector3.Normalize(vec));
      }

    }
  }

  public void CreateRobo()
  {

    var rbase = unitFactory.CreatePlayerUnit(0);
    objList.Add(rbase);

  }


  public void Select(Vector2 posSt,Vector2 posEd)
  {
    selectList = new List<GameObject>();
    for(int i = 0; i < objList.Count;i++)
    {
      if(CheckHit(camera.WorldToScreenPoint(objList[i].transform.position),posSt,posEd))
      {
        selectList.Add(objList[i]);
      }
    }
  }

  public void MovePoint(Vector2 _pos)
  {
    if(selectList == null)
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

  bool CheckHit(Vector2 pos,Vector2 st,Vector2 ed)
  {
    if(pos.x > st.x && pos.x < ed.x)
    {
      if(pos.y < st.y && pos.y > ed.y)
      {
        return true;
      }
    }

    return false;
  }

}
