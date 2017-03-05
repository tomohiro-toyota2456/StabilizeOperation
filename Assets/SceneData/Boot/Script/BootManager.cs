using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Common;
public class BootManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
 {
  StartCoroutine(Load());
 }

 IEnumerator Load()
 {
  var op = SceneManager.LoadSceneAsync("SceneManager", LoadSceneMode.Additive);
  while(!op.isDone)
  {
   yield return null;
  }

  SceneChanger.Instance.ChangeScene("Title", 0);

  SceneManager.UnloadSceneAsync("Boot");

 }
	
}
