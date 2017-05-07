namespace Organization
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using Game.Robo;

  public class UnitModelViewer : MonoBehaviour
  {
    [SerializeField]
    GameObject root;

    string headPartId ="";
    string weponPartId ="";
    string legPartId="";

    GameObject headPart;
    GameObject weponPart;
    GameObject legPart;

    RoboPartCache roboPartCache = null;
    private void Start()
    {
      roboPartCache = new RoboPartCache();
    }

    public void View(string _headPartId,string _weponPartId,string _legPartId)
    {
      if (headPartId != _headPartId)
      {
        headPart = roboPartCache.LoadPartCache(_headPartId);
        headPart = Create(headPart);
        headPartId = _headPartId;
      }

      if(weponPartId != _weponPartId)
      {
        weponPart = roboPartCache.LoadPartCache(_weponPartId);
        weponPart = Create(weponPart);
        weponPartId = _weponPartId;
      }

      if(legPartId != _legPartId)
      {
        legPart = roboPartCache.LoadPartCache(_legPartId);
        legPart = Create(legPart);
        legPartId = _legPartId;
      }

      root.SetActive(true);
      headPart.SetActive(true);
      weponPart.SetActive(true);
      legPart.SetActive(true);

      legPart.transform.SetParent(root.transform);
      var legConnect  = legPart.GetComponent<ConnectObject>();
      var weponConnect = weponPart.GetComponent<ConnectObject>();

      weponPart.transform.SetParent(legConnect.ConnectRoot.transform);
      headPart.transform.SetParent(weponConnect.ConnectRoot.transform);

      legPart.transform.localPosition = Vector3.zero;
      weponPart.transform.localPosition = Vector3.zero;
      headPart.transform.localPosition = Vector3.zero;
    }

    public void Disable()
    {
      root.SetActive(false);
      if(headPart != null)
      headPart.SetActive(false);

      if(weponPart != null)
      weponPart.SetActive(false);

      if(legPart != null)
      legPart.SetActive(false);
    }

    GameObject Create(GameObject _prefab)
    {
      GameObject obj = Instantiate<GameObject>(_prefab);
      obj.transform.position = new Vector3(0, 0, 0);
      return obj;
    }


  }
}