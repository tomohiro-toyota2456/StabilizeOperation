namespace Game
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.UI;
  using UniRx;
  using Common;


  public class CameraMoveButton : MonoBehaviour
  {
    [SerializeField]
    float moveVal;
    [SerializeField]
    Camera camera;
    [SerializeField]
    Button upButton;
    [SerializeField]
    Button downButton;
    [SerializeField]
    Button leftButton;
    [SerializeField]
    Button rightButton;


    // Use this for initialization
    void Start()
    {
      upButton.gameObject.AddComponent<ObservableDownKeepTrigger>()
        .OnDownKeepObservable()
        .Subscribe(_=>
        {
          MoveCameraUp();
        }).AddTo(gameObject);

      downButton.gameObject.AddComponent<ObservableDownKeepTrigger>()
        .OnDownKeepObservable()
        .Subscribe(_ =>
        {
          MoveCameraDown();
        }).AddTo(gameObject);

      leftButton.gameObject.AddComponent<ObservableDownKeepTrigger>()
        .OnDownKeepObservable()
        .Subscribe(_ =>
        {
          MoveCameraLeft();
        }).AddTo(gameObject);

      rightButton.gameObject.AddComponent<ObservableDownKeepTrigger>()
        .OnDownKeepObservable()
        .Subscribe(_ =>
        {
          MoveCameraRight();
        }).AddTo(gameObject);
    }

    public void MoveCameraLeft()
    {
      var pos = camera.transform.localPosition;
      pos.x += -moveVal;
      camera.transform.localPosition = pos;
    }

    public void MoveCameraRight()
    {
      var pos = camera.transform.localPosition;
      pos.x += moveVal;
      camera.transform.localPosition = pos;

    }

    public void MoveCameraUp()
    {
      var pos = camera.transform.localPosition;
      pos.z += moveVal;
      camera.transform.localPosition = pos;
    }

    public void MoveCameraDown()
    {
      var pos = camera.transform.localPosition;
      pos.z += -moveVal;
      camera.transform.localPosition = pos;
    }
  }
}
