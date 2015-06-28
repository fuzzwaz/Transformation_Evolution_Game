using UnityEngine;
using System.Collections;

public class poisionGas : MonoBehaviour {

	public Animator gasAnimator;
	public float timer = 5.0f;
	public float activeTimer = 0.3f;
	public int playerOwned = 0;
	private float activeTimerTemp;
	private bool isActive = true;
	private GameObject player;
	// Use this for initialization
	void Start () {
		activeTimerTemp = activeTimer;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (timer < 0.0f)
		{ 
			gasAnimator.SetTrigger("Fade");
		}

		timer -= Time.deltaTime;

		if (!isActive)
		{
			activeTimer -= Time.deltaTime;
			if (activeTimer < 0)
			{
				isActive = true;
				activeTimer = activeTimerTemp;
			}
		}

	}

	public void setPlayer(int playerNum)
	{
		playerOwned = playerNum;
		player = (GameObject) GameObject.Find ("Player" + playerOwned);
		player = (GameObject) player.transform.FindChild("Body").gameObject;
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player" && isActive && col.GetComponent<playerMovement>().playerNum != playerOwned)
		{
			isActive = false;

			//Ability update
			player.GetComponentInChildren<playerAbilities>().bulletHits++;
			
			if (col.gameObject.GetComponent<playerAbilities>().p_Block)
			{
				col.gameObject.GetComponent<playerAbilities>().p_Block = false;
			}
			else
			{
				player.GetComponentInChildren<playerAbilities>().deathRange = Vector2.Distance(player.transform.position,col.transform.position);
				col.gameObject.GetComponent<playerMovement>().killed();
			}
		}
	}
	
	void OnTriggerStay2D (Collider2D col)
	{
		if (col.tag == "Player" && isActive && col.GetComponent<playerMovement>().playerNum != playerOwned)
		{
			isActive = false;
			
			//Ability update
			player.GetComponentInChildren<playerAbilities>().bulletHits++;
			
			if (col.gameObject.GetComponent<playerAbilities>().p_Block)
			{
				col.gameObject.GetComponent<playerAbilities>().p_Block = false;
			}
			else
			{
				player.GetComponentInChildren<playerAbilities>().deathRange = Vector2.Distance(player.transform.position,col.transform.position);
				col.gameObject.GetComponent<playerMovement>().killed();
			}
		}
	}

	public void CleanUp()
	{
		Destroy(gameObject);
	}
}
