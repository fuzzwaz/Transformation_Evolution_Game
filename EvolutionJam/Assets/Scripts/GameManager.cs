using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public struct playerInfo
	{
		public bool playerDied; //done

		public int playerNum; //done
		public int bulletsShot; //done
		public int dashesMade; //done
		public int bulletHits; //done
		public int surroundingObjects; //done
		public int dashingHits; //done

		public float deathRange; //done
		public float lengthOfLife; // done

	}

	public playerInfo[] PlayerInformation;

	private float restartTimer = 10.0f;
	private int playersAlive = 3;
	private bool p1Dead = false;
	private bool p2Dead = false;
	private bool p3Dead = false;
	private bool p4Dead = false;


	// Use this for initialization
	void Start () {
		PlayerInformation = new playerInfo[4];
		resetPlayers();
	}

	void resetPlayers()
	{
		for (int i = 0; i < 4; i++)
		{
			PlayerInformation[i].playerDied = false;
			PlayerInformation[i].playerNum = i + 1;
			PlayerInformation[i].bulletsShot = 0;
			PlayerInformation[i].dashesMade = 0;
			PlayerInformation[i].bulletHits = 0;
			PlayerInformation[i].surroundingObjects = 0;
			PlayerInformation[i].dashingHits = 0;
			PlayerInformation[i].deathRange = 0.0f;
			PlayerInformation[i].lengthOfLife = 0.0f;
		}
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

		PlayerInformation[player - 1].playerDied = true;
		playersAlive--;

	}
}
