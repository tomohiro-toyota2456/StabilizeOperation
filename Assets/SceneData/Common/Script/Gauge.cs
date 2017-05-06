namespace Common
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.UI;

  //数値の割合でスケーリングしてるだけ
  //ゲージ側のピボットをちゃんと合わせないと動きません
  public class Gauge : MonoBehaviour
  {
    [SerializeField]
    Image gaugeBase;
    [SerializeField]
    Image gauge;
    [SerializeField]
    float max;
    [SerializeField]
    float cur;

    // Update is called once per frame
    void Update()
    {
      SetValue(cur, max);
    }

    public void SetValue(float _cur,float _max)
    {
      float scl = _cur / _max;

      gauge.transform.localScale = new Vector3(scl, 1, 1);
    }
  }
}
