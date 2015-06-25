using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public int playerBullet = 1;
	private float timer = 0.0f;
	private bool canGet = false;
	private bool pickedUp = false;
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = (GameObject) GameObject.Find ("Player" + playerBullet);
		if(player == null)
		{
			Debug.Log("Bullet couldn't find the player");
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Block")
		{
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			canGet = true;
		}

		else if (col.tag == "Player" && canGet && pickedUp == false)
		{
			Destroy(this.gameObject);
			col.gameObject.GetComponentInParent<playerShooting>().BulletPickedUp();
			pickedUp = true;
		}
		else if (col.tag == "Player")
		{
			if (col.gameObject.GetComponentInParent<playerMovement>().playerNum != playerBullet)
			{
				Destroy(this.gameObject);

				//Ability update
				player.GetComponentInChildren<playerAbilities>().bulletHits++;
				player.GetComponentInChildren<playerAbilities>().deathRange = Vector2.Distance(player.transform.position,this.transform.position);
				print (player.GetComponentInChildren<playerAbilities>().deathRange);
				col.gameObject.GetComponent<playerMovement>().killed();
			}
		}
	}

	void OnTriggerStay2D (Collider2D col)
	{
		if (col.tag == "Block")
		{
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			canGet = true;
		}

	}
}
