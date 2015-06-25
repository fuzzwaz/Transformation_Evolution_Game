using UnityEngine;
using System.Collections;

public class dashAttack : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (gameObject.GetComponent<playerMovement>().isDashing)
			{
				col.gameObject.GetComponent<playerMovement>().killed ();
				this.GetComponent<playerAbilities>().dashingHits++;
			}
		}
	}

	void OnCollisionStay2D (Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (gameObject.GetComponent<playerMovement>().isDashing)
			{
				col.gameObject.GetComponent<playerMovement>().killed ();
			}
		}
	}
}
