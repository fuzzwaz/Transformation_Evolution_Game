using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private float restartTimer = 10.0f;

	private int playersAlive = 3;
	private bool p1Dead = false;
	private bool p2Dead = false;
	private bool p3Dead = false;
	private bool p4Dead = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (playersAlive <= 1)
		{
			restartTimer -= Time.deltaTime;
			if (restartTimer < 0.0f)
			{
				Application.LoadLevel("testLevel");
			}
		}
	}

	public void playerDied (int player)
	{
		if (player == 1 && p1Dead)
		{return;}
		else if (player == 2 && p2Dead)
		{return;}
		else if (player == 3 && p3Dead)
		{return;}
		else if (player == 4 && p4Dead)
		{return;}

		if (player == 1)
		{
			p1Dead = true;
		}
		else if (player == 2)
		{
			p2Dead = true;
		}
		else if (player == 3)
		{
			p3Dead = true;
		}
		else if (player == 4)
		{
			p4Dead = true;
		}

		playersAlive--;

	}
}
