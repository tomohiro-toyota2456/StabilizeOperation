namespace Game.Robo
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UniRx;

  public interface IRoboController 
  {
    IObservable<Vector3> Move();
    bool Attack();
  }
}