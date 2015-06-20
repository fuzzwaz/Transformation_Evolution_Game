using UnityEngine;
using System.Collections;

public class dashAttack : MonoBehaviour {

	private GameObject player;
	// Use this for initialization
	void Start () {
		player = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player")
		{
			if (player.gameObject.GetComponent<playerMovement>().isDashing)
			{
				col.gameObject.transform.parent.gameObject.GetComponent<playerMovement>().killed ();
			}
		}
	}

	void OnTriggerStay2D (Collider2D col)
	{
		if (col.tag == "Player")
		{
			if (player.gameObject.GetComponent<playerMovement>().isDashing)
			{
				col.gameObject.transform.parent.gameObject.GetComponent<playerMovement>().killed ();
			}
		}
	}
}
