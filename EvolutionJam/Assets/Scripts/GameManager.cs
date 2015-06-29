using UnityEngine;
using System.Collections;
using FuzzyEvolutions;

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

public struct playerAbilityMap
{
	public bool a_block;
	public bool a_seeking;
	public bool a_poison;
	public bool a_spread;
	public bool a_blink;
	public bool a_piercing;
	public bool a_spikingDash;
	
	public int a_spike;
	public int a_moreAmmo;
	public int a_bouncing;
	
	public float a_gravity;
	public float a_explosive;
	public float a_longerDash;
	public float a_fasterDash;
	public float a_fasterShot;
	public float a_largerShot;
	public float a_growingDash;
	public float a_fasterMovement;
}

public class GameManager : MonoBehaviour {



	public playerInfo[] PlayerInformation = new playerInfo[4];
	public int[] PlayerRoundsWon;// = new int[4];
	public playerAbilityMap[] PlayerAbilities = new playerAbilityMap[4];
	public GameObject AIManager;
	public int rounds = 0;

	private float restartTimer = 2.0f;
	private int playersAlive = 4;
	private GameObject[] playerObjects;

	private bool p1Dead = false;
	private bool p2Dead = false;
	private bool p3Dead = false;
	private bool p4Dead = false;

	public FuzzyInferenceEngine fuzzyEngine;

	// Use this for initialization
	void Start () {

		if (GameObject.Find("GameManagerMaster") != null)
		{
			Destroy(this.gameObject);
		}


		this.gameObject.name = "GameManagerMaster";
		DontDestroyOnLoad(gameObject);
		PlayerRoundsWon = new int[4];
		//PlayerInformation = new playerInfo[4];
		//PlayerAbilities = new playerAbilityMap[4];
		playerObjects = new GameObject[4];
		fuzzyEngine = new FuzzyInferenceEngine();
		//resetPlayers();
		//roundStart ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playersAlive <= 1)
		{

			restartTimer -= Time.deltaTime;
			if (restartTimer < 0.0f)
			{
				if (!p1Dead)
				{PlayerRoundsWon[0]++;}
				else if (!p2Dead)
				{PlayerRoundsWon[1]++;}
				else if (!p3Dead)
				{PlayerRoundsWon[2]++;}
				else if (!p4Dead)
				{PlayerRoundsWon[3]++;}



				if (PlayerRoundsWon[0] >= 3 || PlayerRoundsWon[1] >= 3 || PlayerRoundsWon[2] >= 3 || PlayerRoundsWon[3] >= 3)
				{
					restartTimer = 2.0f;
					playersAlive = 4;
					
					p1Dead = false;
					p2Dead = false;
					p3Dead = false;
					p4Dead = false;
					Application.LoadLevel("Winner");

				}
				else
				{
					rounds++;
					//roundStart();
					restartTimer = 2.0f;
					playersAlive = 4;
					
					p1Dead = false;
					p2Dead = false;
					p3Dead = false;
					p4Dead = false;
					Application.LoadLevel("testLevel");


				}

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

	public void roundStart()
	{
		for (int i = 0; i < 4; i++)
		{
			if (playerObjects[i] != null)
			{
				int num = i + 1;
				playerObjects[i] = (GameObject) GameObject.Find ("Player" + num.ToString());
				playerObjects[i] = playerObjects[i].transform.FindChild("Body").gameObject;
				
				playerObjects[i].GetComponent<playerAbilities>().abl_Block(PlayerAbilities[i].a_block);
				playerObjects[i].GetComponent<playerAbilities>().abl_SeekingShot(PlayerAbilities[i].a_seeking);
				playerObjects[i].GetComponent<playerAbilities>().abl_PoisonGas(PlayerAbilities[i].a_poison);
				playerObjects[i].GetComponent<playerAbilities>().abl_BodySpike(PlayerAbilities[i].a_spike);
				playerObjects[i].GetComponent<playerAbilities>().abl_SpreadShot(PlayerAbilities[i].a_spread);
				playerObjects[i].GetComponent<playerAbilities>().abl_ExplosiveShot(PlayerAbilities[i].a_explosive);
				playerObjects[i].GetComponent<playerAbilities>().abl_BouncingShot(PlayerAbilities[i].a_bouncing);
				playerObjects[i].GetComponent<playerAbilities>().abl_Blink(PlayerAbilities[i].a_blink);
				playerObjects[i].GetComponent<playerAbilities>().abl_PiercingShot(PlayerAbilities[i].a_piercing);
				playerObjects[i].GetComponent<playerAbilities>().abl_LongerDash(PlayerAbilities[i].a_longerDash);
				playerObjects[i].GetComponent<playerAbilities>().abl_FasterDash(PlayerAbilities[i].a_fasterDash);
				playerObjects[i].GetComponent<playerAbilities>().abl_MoreAmmo(PlayerAbilities[i].a_moreAmmo);
				playerObjects[i].GetComponent<playerAbilities>().abl_FasterShot(PlayerAbilities[i].a_fasterShot);
				playerObjects[i].GetComponent<playerAbilities>().abl_LargerShot(PlayerAbilities[i].a_largerShot);
				playerObjects[i].GetComponent<playerAbilities>().abl_GrowingDash(PlayerAbilities[i].a_growingDash);
				playerObjects[i].GetComponent<playerAbilities>().abl_SpikingDash(PlayerAbilities[i].a_spikingDash);
				playerObjects[i].GetComponent<playerAbilities>().abl_GravityShot(PlayerAbilities[i].a_gravity);
				playerObjects[i].GetComponent<playerAbilities>().abl_FasterMovement(PlayerAbilities[i].a_fasterMovement);
			}
		}
	}

	public void resetPlayers()
	{
		if (rounds >= 0)
		{
			for (int i = 0; i < 4; i++)
			{
				int num = i + 1;
				playerObjects[i] = (GameObject) GameObject.Find ("Player" + num.ToString());


				PlayerInformation[i].playerDied = false;
				PlayerInformation[i].playerNum = i + 1;
				PlayerInformation[i].bulletsShot = 0;
				PlayerInformation[i].dashesMade = 0;
				PlayerInformation[i].bulletHits = 0;
				PlayerInformation[i].surroundingObjects = 0;
				PlayerInformation[i].dashingHits = 0;
				PlayerInformation[i].deathRange = 0.0f;
				PlayerInformation[i].lengthOfLife = 0.0f;

				if (rounds > 0)
				{
				PlayerAbilities[i].a_spike = Random.Range (0,2);
				PlayerAbilities[i].a_moreAmmo = Random.Range(0,6);


				PlayerAbilities[i].a_bouncing = Random.Range (0,5);
				
				PlayerAbilities[i].a_gravity = (float) Random.Range (0,100);
				PlayerAbilities[i].a_explosive = (float) Random.Range (0,2);
				PlayerAbilities[i].a_longerDash = (float) Random.Range (0,2);
				PlayerAbilities[i].a_fasterShot = (float) Random.Range (0,150);
				PlayerAbilities[i].a_fasterDash = (float) Random.Range (0,50);
				PlayerAbilities[i].a_largerShot = 0.0f;
				PlayerAbilities[i].a_growingDash = 0.0f;
				PlayerAbilities[i].a_fasterMovement = (float) Random.Range (0,2);

				if (Random.Range (0,2) == 0)
				{
					PlayerAbilities[i].a_block = false;
				}
				else
				{
					PlayerAbilities[i].a_block = true;
				}

				if (Random.Range (0,2) == 0)
				{
					PlayerAbilities[i].a_seeking = false;
				}
				else
				{
					PlayerAbilities[i].a_seeking = true;
				}

				if (Random.Range (0,50) < 40)
				{
					PlayerAbilities[i].a_poison = false;
				}
				else
				{
					PlayerAbilities[i].a_poison = true;
				}

				if (Random.Range (0,2) == 0)
				{
					PlayerAbilities[i].a_spread = false;
				}
				else
				{
					PlayerAbilities[i].a_spread = true;
				}


				if (Random.Range (0,2) == 0)
				{
					PlayerAbilities[i].a_piercing = false;
				}
				else
				{
					PlayerAbilities[i].a_piercing = true;
				}

				if (Random.Range (0,2) == 0)
				{
					PlayerAbilities[i].a_spikingDash = false;
				}
				else
				{
					PlayerAbilities[i].a_spikingDash = true;
				}
				}
				
			}
		}
	}
}
