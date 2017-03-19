using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Robo;
public class TestRoboFactory : MonoBehaviour
{
  [SerializeField]
  GameObject leg;
  [SerializeField]
  GameObject wepon;
  [SerializeField]
  GameObject head;
  [SerializeField]
  GameObject roboBase;
  [SerializeField]
  Game.GameTouchAction touchAction;
  [SerializeField]
  Camera camera;
  List<GameObject> objList = new List<GameObject>();

  List<GameObject> selectList;

  private void Start()
  {
    touchAction.EndDragDel = Select;
    touchAction.TouchDel = MovePoint;
  }

  public void CreateRobo()
  {
    var rbase = Create(roboBase);
    var obj = Create(leg);
    var obj2 = Create(wepon);
    var obj3 = Create(head);

    
    var con = obj.GetComponent<ConnectObject>().ConnectRoot;
    var con2 = obj2.GetComponent<ConnectObject>().ConnectRoot;

    obj.transform.SetParent(rbase.transform);
    obj2.transform.SetParent(con.transform);
    obj3.transform.SetParent(con2.transform);

    obj2.transform.localPosition = new Vector3(0, 0, 0);
    obj3.transform.localPosition = new Vector3(0, 0, 0);

    objList.Add(rbase);

  }

   GameObject Create(GameObject _prefab)
  {
    GameObject obj = Instantiate<GameObject>(_prefab);
    obj.transform.position = new Vector3(0, 0, 0);
    return obj;
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

    if (Physics.Raycast(Camera.main.ScreenPointToRay(_pos), out hit, 1000))
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
