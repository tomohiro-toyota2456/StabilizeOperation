namespace Game
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.EventSystems;
  using UnityEngine.UI;
  using System;
  


  public class GameTouchAction : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler
  {
    [SerializeField]
    Image image;//範囲描画用
    [SerializeField]
    RectTransform screenObj;//タッチ判定をするオブジェクト　基本スクリーンサイズと同じにする

    Vector2 beginDragPos;
    RectTransform rectTransform;
    Vector2 screenSize;

    Action<Vector2, Vector2> endDragDel;
    public Action<Vector2, Vector2> EndDragDel { set { endDragDel = value; } }

    Action<Vector2> touchDel;
    public Action<Vector2> TouchDel { set { touchDel = value; } }

    //ドラッグ終了時の送り込む座標
    Vector2 returnStPos;
    Vector2 returnEdPos;

    bool isDrag = false;

    private void Start()
    {
      rectTransform = image.GetComponent<RectTransform>();
    }

    Vector2 GetSize(Vector2 _posSt, Vector2 _posEd)
    {
      Vector2 size;

      size.x = _posEd.x - _posSt.x;
      size.y = -_posEd.y + _posSt.y;

      return size;
    }

    Vector2 Offset(Vector2 _pos)
    {
      Vector2 offset;

      offset.x = screenSize.x / Screen.width;
      offset.y = screenSize.y / Screen.height;

      Vector2 ans;

      ans.x = offset.x * _pos.x;
      ans.y = offset.y * _pos.y;

      return ans;
    }

    public void OnBeginDrag(PointerEventData _data)
    {
      screenSize.x = screenObj.rect.width;
      screenSize.y = screenObj.rect.height;
      beginDragPos = _data.position;
      isDrag = true;
      image.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData _data)
    {
      Vector2 stPos;
      Vector2 edPos;

      stPos = beginDragPos;
      edPos = _data.position;

      if(beginDragPos.x > _data.position.x)
      {
        stPos.x = _data.position.x;
        edPos.x = beginDragPos.x;
      }

      if(beginDragPos.y < _data.position.y)
      {
        stPos.y = _data.position.y;
        edPos.y = beginDragPos.y;
      }


      returnStPos = stPos;
      returnEdPos = edPos;

      rectTransform.anchoredPosition = Offset(stPos);
      rectTransform.sizeDelta = GetSize(Offset(stPos), Offset(edPos));

    }

    public void OnEndDrag(PointerEventData _data)
    {
      if(endDragDel != null )
      {
        endDragDel(returnStPos, returnEdPos);
      }


      isDrag = false;

      image.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData _data)
    {
      if (isDrag)
        return;

      if(touchDel != null)
      {
        touchDel(_data.position);
      }
    
    }
  }
}
