using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	public string sceneToLoad;


	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Start"))
		{
			Debug.Log("Loading " + sceneToLoad);
			Application.LoadLevel(sceneToLoad);
		}
	}
}
