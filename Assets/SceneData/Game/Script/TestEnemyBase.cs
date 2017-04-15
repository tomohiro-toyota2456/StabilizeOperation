using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Game.Robo;
using UnityEngine.UI;
public class TestEnemyBase : MonoBehaviour
{
  [SerializeField]
  Text boss;
  // Use this for initialization
	void Start ()
  {
    FloatReactiveProperty hp = this.GetComponent<RoboParam>().CurHp;

    hp.Subscribe(_ =>
    {
      boss.text = "enemyBaseHP:" + _;
    }).AddTo(gameObject);
		
	}
}
