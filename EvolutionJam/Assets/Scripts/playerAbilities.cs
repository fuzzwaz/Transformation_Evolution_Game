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
	public bool p_PiercingShot = false;
	public bool p_SpikingDash = false;

	public int p_BodySpikes = 0;
	public int p_Bouncing = 0;
	public int p_MoreBullets = 0;

	public float p_explosiveShot = 0.0f;
	public float p_LongerDash = 0.0f;
	public float p_FasterDash = 0.0f;
	public float p_FasterShot = 0.0f;
	public float p_LargerShot = 0.0f;
	public float p_GrowingDash = 0.0f;
	public float p_GravityShot = 0.0f;
	public float p_FasterMovement = 0.0f;
	
	private int playerNumID;
	private GameObject gameManager;
	private GameObject evolutionaryParts;


	// Use this for initialization
	void Start () {
		playerNumID = this.GetComponent<playerMovement>().playerNum - 1;
		gameManager = (GameObject) GameObject.Find("GameManagerMaster");
		evolutionaryParts = this.transform.FindChild("EvolutionParts").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (!p_Block)
		{
			evolutionaryParts.transform.FindChild("Armor").gameObject.SetActive(false);
		}
	}

	public void UpdatePlayerInfo()
	{
		gameManager = (GameObject) GameObject.Find("GameManagerMaster");
		gameManager.GetComponent<GameManager>().PlayerInformation[playerNumID].bulletsShot = bulletsShot;
		gameManager.GetComponent<GameManager>().PlayerInformation[playerNumID].dashesMade = dashesMade;
		gameManager.GetComponent<GameManager>().PlayerInformation[playerNumID].bulletHits = bulletHits;
		gameManager.GetComponent<GameManager>().PlayerInformation[playerNumID].deathRange = deathRange;
		gameManager.GetComponent<GameManager>().PlayerInformation[playerNumID].lengthOfLife = lengthOfLife;
		gameManager.GetComponent<GameManager>().PlayerInformation[playerNumID].surroundingObjects = surroundingObjects;
		gameManager.GetComponent<GameManager>().PlayerInformation[playerNumID].dashingHits = dashingHits;
	}

	public void abl_Block(bool has) //art
	{
		p_Block = has;
		if (p_Block)
		{
			evolutionaryParts.transform.FindChild("Armor").gameObject.SetActive(true);
		}
	}

	public void abl_SeekingShot(bool has) //art 
	{
		p_Seeking = has;
	}

	public void abl_PoisonGas(bool has) //art
	{
		p_Poision = has;
	}

	public void abl_BodySpike(int spikes) //art
	{
		if (spikes > 0)
		{
			p_BodySpikes = 1;
			this.transform.FindChild("Spike").gameObject.SetActive(true);
		}
	}

	public void abl_SpreadShot(bool has) //art
	{
		p_SpreadShot = has;
		if (p_SpreadShot)
		{
			evolutionaryParts.transform.FindChild("Mandibles").gameObject.SetActive(true);
		}
	}

	public void abl_ExplosiveShot(float explosiveMultiplier) //art
	{
		p_explosiveShot = explosiveMultiplier;
	}

	public void abl_BouncingShot(int bounces) //art
	{
		p_Bouncing = bounces;
	}

	public void abl_Blink(bool has) //remove?
	{
		//print ("BLINK IS REMOVED");
	}

	public void abl_PiercingShot(bool has) //art
	{
		p_PiercingShot = has;
	}

	public void abl_LongerDash(float additionalSeconds)  //art
	{
		p_LongerDash = additionalSeconds;
		this.GetComponent<playerMovement>().increaseDashDuration(p_LongerDash);

		if (p_LongerDash > 0.0f)
		{
			evolutionaryParts.transform.FindChild("Wings").gameObject.SetActive(true);
		}
	}

	public void abl_FasterDash(float seconds) //art
	{
		p_FasterDash = seconds; 
		this.GetComponent<playerMovement>().increaseDashSpeed(p_FasterDash);

		if (p_FasterDash > 0.0f)
		{
			evolutionaryParts.transform.FindChild("SlugPart").gameObject.SetActive(true);
		}
	}

	public void abl_MoreAmmo(int additionalBullets) //art
	{
		p_MoreBullets = additionalBullets;
		this.GetComponent<playerShooting>().increaseAmmoCount(p_MoreBullets);
		for (int i = 0; i < p_MoreBullets; i++)
		{
			this.GetComponent<playerShooting>().tailStack.AddSegment();
		}
	}

	public void abl_FasterShot(float force) //art
	{
		p_FasterShot = force;
		this.GetComponent<playerShooting>().increaseShotSpeed(p_FasterShot);

		if (p_FasterShot > 0.0f)
		{
			evolutionaryParts.transform.FindChild("Arms").gameObject.SetActive(true);
		}
	}

	public void abl_LargerShot(float sizeMultiplier) 
	{
		p_LargerShot = sizeMultiplier;
	}

	public void abl_GrowingDash(float sizeMultiplier) 
	{
		p_GrowingDash = sizeMultiplier;
	}

	public void abl_SpikingDash(bool has) //art
	{
		p_SpikingDash = has;
	}

	public void abl_GravityShot(float gravityMultiplier) 
	{
		p_GravityShot = gravityMultiplier;
	}

	public void abl_FasterMovement(float speedmultplier) 
	{
		p_FasterMovement = speedmultplier;
		this.GetComponent<playerMovement>().increaseSpeed(p_FasterMovement);

		if (p_FasterMovement > 0.0f)
		{
			evolutionaryParts.transform.FindChild("Legs").gameObject.SetActive(true);
		}
	}
}
