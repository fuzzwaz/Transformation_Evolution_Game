using UnityEngine;
using System.Collections;

public class createGM : MonoBehaviour {


	private GameObject GM;

	bool started = false;
	// Use this for initialization

	void Awake ()
	{

	}


	void Start () {
		GM = GameObject.Find("GameManagerMaster");
	}
	
	// Update is called once per frame
	void Update () {

		if (!started)
		{
			if (GameObject.Find("Player1") != null)
			{
				GM = GameObject.Find("GameManagerMaster");
				GM.GetComponent<GameManager>().resetPlayers();
				GM.GetComponent<GameManager>().roundStart();
				started = true;
			}
		}
	}
}
