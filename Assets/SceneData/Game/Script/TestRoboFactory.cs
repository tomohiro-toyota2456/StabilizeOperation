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
  Test test;
  [SerializeField]
  Camera camera;
  List<GameObject> objList = new List<GameObject>();

  private void Start()
  {
    test.del = Select;
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

    objList.Add(obj);

  }

   GameObject Create(GameObject _prefab)
  {
    GameObject obj = Instantiate<GameObject>(_prefab);
    obj.transform.position = new Vector3(0, 0, 0);
    return obj;
  }

  public void Select(Vector3 posSt,Vector3 posEd)
  {
    for(int i = 0; i < objList.Count;i++)
    {
      if(CheckHit(camera.WorldToScreenPoint(objList[i].transform.position),posSt,posEd))
      {
        Debug.Log("Hit");
      }
    }
  }

  bool CheckHit(Vector3 pos,Vector3 st,Vector3 ed)
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
