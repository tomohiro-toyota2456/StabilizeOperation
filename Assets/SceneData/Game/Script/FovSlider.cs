using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class FovSlider : MonoBehaviour
{
  [SerializeField]
  Camera camera;
  [SerializeField]
  Slider slider;

	// Use this for initialization
	void Start ()
  {
    slider.value = slider.maxValue / 2;

    slider.OnValueChangedAsObservable()
      .Subscribe(value =>
      {
        camera.fieldOfView = value;
      }).AddTo(gameObject);
	}
	

}
