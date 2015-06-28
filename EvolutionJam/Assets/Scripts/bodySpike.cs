using UnityEngine;
using System.Collections;

public class bodySpike : MonoBehaviour {
	
	public float activeTimer = 0.3f;
	private float activeTimerTemp;
	private bool isActive = true;
	private GameObject body;
	
	private int playerNum;
	// Use this for initialization
	void Start () {
		body = this.transform.parent.gameObject;
		if (body.GetComponent<playerMovement>() != null)
		{
			playerNum = body.GetComponent<playerMovement>().playerNum;
		}
		else
		{
			body = body.transform.parent.gameObject;
			playerNum = body.GetComponent<playerMovement>().playerNum;
		}

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
				col.gameObject.GetComponent<playerAbilities>().deathRange = Vector2.Distance(body.transform.position,col.transform.position);
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
				col.gameObject.GetComponent<playerAbilities>().deathRange = Vector2.Distance(body.transform.position,col.transform.position);
				col.gameObject.GetComponent<playerMovement>().killed();
				isActive = false;
			}
		}
	}
}