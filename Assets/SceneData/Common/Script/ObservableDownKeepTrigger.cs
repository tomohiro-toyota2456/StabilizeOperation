namespace Common
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UniRx;
  using UniRx.Triggers;
  using UnityEngine.EventSystems;
  using System;

  public class ObservableDownKeepTrigger : ObservableTriggerBase,IPointerDownHandler,IPointerUpHandler
  {
    float? raiseTime = null;
    Subject<float> pointerDownKeepObserver;

    // Update is called once per frame
    void Update()
    {
      if(raiseTime != null)
      {
        if(pointerDownKeepObserver != null)
        {
          float time = Time.realtimeSinceStartup - raiseTime.Value;
          pointerDownKeepObserver.OnNext(time);
        }
      }
    }

    protected override void RaiseOnCompletedOnDestroy()
    {

    }

    public void OnPointerDown(PointerEventData _eventData)
    {
      raiseTime = Time.realtimeSinceStartup;
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
      raiseTime = null;
    }

    public IObservable<float> OnDownKeepObservable()
    {
      return pointerDownKeepObserver == null ? pointerDownKeepObserver = new Subject<float>() : pointerDownKeepObserver;
    }

    private void OnDisable()
    {
      raiseTime = null;
    }

  }
}