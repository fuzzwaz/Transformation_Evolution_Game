using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public float activeTimer = 0.3f;
	private float activeTimerTemp;
	private bool isActive = true;

	public float explosionTimer = 0.2f;
	public Collider2D hitBox;

	private int playerNum;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!isActive)
		{
			activeTimer -= Time.deltaTime;
			if (activeTimer < 0)
			{
				isActive = true;
				activeTimer = activeTimerTemp;
			}
		}

		explosionTimer -= Time.deltaTime;
		if (explosionTimer < 0.0f)
		{
			Destroy(hitBox);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player" && col.gameObject.GetComponentInParent<playerMovement>().playerNum != playerNum)
		{
			//Ability update
			if (col.gameObject.GetComponent<playerAbilities>().p_Block)
			{
				col.gameObject.GetComponent<playerAbilities>().p_Block = false;
				isActive = false;
			}
			else
			{
				col.gameObject.GetComponent<playerAbilities>().deathRange = 0.0f;
				col.gameObject.GetComponent<playerMovement>().killed();
				isActive = false;
			}
		}
	}
	
	void OnTriggerStay2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player" && col.gameObject.GetComponentInParent<playerMovement>().playerNum != playerNum)
		{
			//Ability update
			if (col.gameObject.GetComponent<playerAbilities>().p_Block)
			{
				col.gameObject.GetComponent<playerAbilities>().p_Block = false;
				isActive = false;
			}
			else
			{
				col.gameObject.GetComponent<playerAbilities>().deathRange = 0.0f;
				col.gameObject.GetComponent<playerMovement>().killed();
				isActive = false;
			}
		}
	}

	public void setPlayerNum (int num)
	{
		playerNum = num;
	}

	public void CleanUp()
	{
		Destroy(gameObject);
	}
}
