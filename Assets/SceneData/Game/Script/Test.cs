using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Test : MonoBehaviour
{
  [SerializeField]
  Image image;
  [SerializeField]
  Camera camera;
  [SerializeField]
  RectTransform canvas;
  Vector3 mousePosA;
  Vector3 mousePosB;

  RectTransform rt;

  Vector2 canvasSize;

  public Action<Vector3, Vector3> del;

	// Use this for initialization
	void Start ()
  {
    rt = image.GetComponent<RectTransform>();
    image.gameObject.SetActive(false);
  }
	
	// Update is called once per frame
	void Update ()
  {

    canvasSize.x = canvas.sizeDelta.x;// * canvas.transform.localScale.x;
    canvasSize.y = canvas.sizeDelta.y;// * canvas.transform.localScale.y;

    if (Input.GetMouseButtonDown(0))
    {
      mousePosA = Input.mousePosition;
      image.gameObject.SetActive(true);
      rt.anchoredPosition = Offset(mousePosA);
    }

    if(image.gameObject.activeSelf)
    {
      mousePosB = Input.mousePosition;
      rt.sizeDelta = GetSize(Offset(mousePosA),Offset(mousePosB));
    }


    if (Input.GetMouseButtonUp(0))
    {
      mousePosB = Input.mousePosition;
      image.gameObject.SetActive(false);
      Select();
    }
  }

  Vector2 Offset(Vector3 _mousePos)
  {
    Vector2 offset;

    offset.x = canvasSize.x / Screen.width;
    offset.y = canvasSize.y / Screen.height;

    Vector2 ans;

    ans.x = offset.x * _mousePos.x;
    ans.y = offset.y * _mousePos.y;

    return ans;
  }

  Vector2 GetSize(Vector2 _mousePosSt,Vector2 _mousePosEd)
  {
    Vector2 size;

    size.x = _mousePosEd.x - _mousePosSt.x;
    size.y = -_mousePosEd.y + _mousePosSt.y;


    return size;
  }

  void Select()
  {
    RaycastHit hit;

    Vector3 z = Vector3.one;
    if( Physics.Raycast(camera.transform.position,camera.transform.forward.normalized,out hit,10000))
    {
      z = hit.point - camera.transform.position;
    }

    Debug.Log(z.magnitude);
    Vector3 mPosSt = mousePosA;
    Vector3 mPosEd = mousePosB;
    mPosSt.z = 50.0f;
    mPosEd.z = 50.0f;
    Vector3 wPosSt = camera.ScreenToWorldPoint(mPosSt);
    Vector3 wPosEd = camera.ScreenToWorldPoint(mPosEd);


    Vector3 view = camera.WorldToScreenPoint(Vector3.zero);

    Vector3 vPosSt = Vector3.zero;
    Vector3 vPosEd = Vector3.zero;

    vPosSt.x = mPosSt.x / Screen.width;
    vPosSt.y = mPosSt.y / Screen.height;

    vPosEd.x = mPosEd.x / Screen.width;
    vPosEd.y = mPosEd.y / Screen.height;

    del(mousePosA, mousePosB);

    Debug.Log(view);
    Debug.Log(mousePosA);
    Debug.Log(mousePosB);
  }

}
