using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EvolutionToGameSwitcher : MonoBehaviour 
{	
	public Text player1, player2, player3, player4;
	public float timeToChange = 5.0f;
	float countUp = 0.0f;

	// Use this for initialization
	void Start () 
	{
		//TODO: Get 
		player1.text = "DASADAW";
		player2.text = "DASADAW";
		player3.text = "DASADAW";
		player4.text = "DASADAW";
	}

	void Update()
	{
		countUp += Time.deltaTime;
		if(countUp > timeToChange)
		{
			Application.LoadLevel("testLevel");
		}
	}
}
