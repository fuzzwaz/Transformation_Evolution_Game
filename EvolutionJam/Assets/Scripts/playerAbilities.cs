using UnityEngine;
using System.Collections;

public class playerAbilities : MonoBehaviour {


	public int bulletsShot = 0;
	public int dashesMade = 0;
	public int bulletHits = 0;
	public int dashingHits = 0;
	public int surroundingObjects = 0;
	public float deathRange = 0.0f;
	public float lengthOfLife = 0.0f;

	//Current Abilities
	public bool p_Block = false;
	public bool p_Seeking = false;
	public bool p_Poision = false;
	public bool p_SpreadShot = false;

	public int p_bodySpikes = 0;

	public float p_explosiveShot = 0.0f;
	
	private int playerNumID;
	private GameObject gameManager;



	// Use this for initialization
	void Start () {
		playerNumID = this.GetComponent<playerMovement>().playerNum - 1;
		gameManager = (GameObject) GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdatePlayerInfo()
	{
		gameManager.GetComponent<GameManager>().PlayerInformation[playerNumID].bulletsShot = bulletsShot;
		gameManager.GetComponent<GameManager>().PlayerInformation[playerNumID].dashesMade = dashesMade;
		gameManager.GetComponent<GameManager>().PlayerInformation[playerNumID].bulletHits = bulletHits;
		gameManager.GetComponent<GameManager>().PlayerInformation[playerNumID].deathRange = deathRange;
		gameManager.GetComponent<GameManager>().PlayerInformation[playerNumID].lengthOfLife = lengthOfLife;
		gameManager.GetComponent<GameManager>().PlayerInformation[playerNumID].surroundingObjects = surroundingObjects;
		gameManager.GetComponent<GameManager>().PlayerInformation[playerNumID].dashingHits = dashingHits;
	}

	public void abl_Block(bool has) //complete and tested
	{
		p_Block = has;
	}

	public void abl_SeekingShot(bool has) //complete and tested 
	{
		p_Seeking = has;
	}

	public void abl_PoisonGas(bool has) //complete and tested
	{
		p_Poision = has;
	}

	public void abl_BodySpike(int spikes) //completed and tested
	{
		if (spikes > 0)
		{
			p_bodySpikes = 1;
			this.transform.FindChild("Spike").gameObject.SetActive(true);
		}
	}

	public void abl_SpreadShot(bool has) //completed and tested
	{
		p_SpreadShot = has;
	}

	public void abl_ExplosiveShot(float explosiveMultiplier) //completed and tested
	{
		p_explosiveShot = explosiveMultiplier;
	}

	public void abl_BouncingShot(float bounces) 
	{}

	public void abl_Blink(bool has)
	{}

	public void abl_PiercingShot(bool has)
	{}

	public void abl_LongerDash(float additionalSeconds) 
	{}

	public void abl_FasterDash(float seconds) 
	{}

	public void abl_MoreAmmo(int additionalBullets)
	{}

	public void abl_FasterShot(float seconds) 
	{}

	public void abl_LargerShot(float sizeMultiplier) 
	{}

	public void abl_GrowingDash(float sizeMultiplier) 
	{}

	public void abl_SpikingDash(bool has)
	{}

	public void abl_GravityShot(float gravityMultiplier)
	{}

	public void abl_FasterMovement(float speedmultplier)
	{}
}
