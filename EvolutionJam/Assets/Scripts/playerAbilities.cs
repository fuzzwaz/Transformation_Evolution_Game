using UnityEngine;
using System.Collections;

public class playerAbilities : MonoBehaviour {


	public int bulletsShot = 0;
	public int dashesMade = 0;
	public int bulletHits = 0;
	public int dashingHits = 0;
	public float deathRange = 0.0f;
	public float lengthOfLife = 0.0f;
	
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

	}

	public void abl_Block()
	{

	}

	public void abl_SeekingShot()
	{}

	public void abl_PoisonGas()
	{}

	public void abl_BodySpike()
	{}

	public void abl_SpreadShot()
	{}

	public void abl_ExplosiveShot()
	{}

	public void abl_BouncingShot()
	{}

	public void abl_Blink()
	{}

	public void abl_PiercingShot()
	{}

	public void abl_LongerDash()
	{}

	public void abl_FasterDash()
	{}

	public void abl_MoreAmmo()
	{}

	public void abl_FasterShot()
	{}

	public void abl_LargerShot()
	{}

	public void abl_GrowindDash()
	{}

	public void abl_SpikingDash()
	{}

	public void abl_GravityShot()
	{}

	public void abl_FasterMovement()
	{}
}
