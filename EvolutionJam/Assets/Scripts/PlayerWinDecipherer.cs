using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerWinDecipherer : MonoBehaviour {

	public Text playerNumber;
	public GameObject GM;

	private bool wait = true;

	// Use this for initialization
	void Start () {


		//playerNumber.text = "PLAYER " + "99";
	}
	
	// Update is called once per frame
	void Update () {
		if (wait)
		{
			GM = GameObject.Find ("GameManagerMaster");
			if (GM.gameObject != null)
			{
				if (GM.GetComponent<GameManager>().PlayerRoundsWon[0] == 3)
				{
					playerNumber.text = "PLAYER " + "1";
				}
				else if (GM.GetComponent<GameManager>().PlayerRoundsWon[1] == 3)
				{
					playerNumber.text = "PLAYER " + "2";
				}
				else if (GM.GetComponent<GameManager>().PlayerRoundsWon[2] == 3)
				{
					playerNumber.text = "PLAYER " + "3";
				}
				else 
				{
					playerNumber.text = "PLAYER " + "4";
				}
			}
			wait = false;
		}
	}
}
