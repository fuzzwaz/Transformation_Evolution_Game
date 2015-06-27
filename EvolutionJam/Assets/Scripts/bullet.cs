using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public int playerBullet = 1;
	private float timer = 0.0f;
	private bool canGet = false;
	private bool pickedUp = false;
	private int seekPlayer = 0;
	private GameObject seekingPlayer;
	private GameObject player;
	public AudioClip shotSound, hitWallSound, pickUpSound;
	// Use this for initialization
	void Start () {
		player = (GameObject) GameObject.Find ("Player" + playerBullet);
		player = (GameObject) player.transform.FindChild("Body").gameObject;
		if(player == null)
		{
			Debug.Log("Bullet couldn't find the player");
		}
		AudioSource.PlayClipAtPoint(shotSound, Camera.main.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<playerAbilities>().p_Seeking && seekPlayer == 1)
		{
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

			Vector2 newDirection = new Vector2(seekingPlayer.transform.position.x - this.transform.position.x,seekingPlayer.transform.position.y - this.transform.position.y);
			newDirection.Normalize();
			this.GetComponent<Rigidbody2D>().AddForce(newDirection * player.GetComponent<playerShooting>().bulletForce);

			seekPlayer = 2;
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Block")
		{
			AudioSource.PlayClipAtPoint(hitWallSound, Camera.main.transform.position);
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			seekPlayer = 2;
			canGet = true;
		}
		else if (col.gameObject.tag == "deadBullet")
		{
			Destroy(this.gameObject);
		}
		else if (col.gameObject.tag == "Player" && canGet && pickedUp == false)
		{
			AudioSource.PlayClipAtPoint(pickUpSound, Camera.main.transform.position);
			Destroy(this.gameObject);
			col.gameObject.GetComponentInParent<playerShooting>().BulletPickedUp();
			pickedUp = true;
		}
		else if (col.gameObject.tag == "Player")
		{
			if (col.gameObject.GetComponentInParent<playerMovement>().playerNum != playerBullet)
			{
				Destroy(this.gameObject);
			
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
	}

	void OnCollisionStay2D (Collision2D col)
	{
		if (col.gameObject.tag == "Block")
		{
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			canGet = true;
		}
		else if (col.gameObject.tag == "deadBullet")
		{
			Destroy(this.gameObject);
		}

	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player" && col.gameObject.GetComponentInParent<playerMovement>().playerNum != playerBullet && seekPlayer == 0)
		{
			seekPlayer = 1;
			seekingPlayer = col.gameObject;
		}
	}
	void OnTriggerStay2D (Collider2D col)
	{
		if (col.tag == "Player" && col.gameObject.GetComponentInParent<playerMovement>().playerNum != playerBullet && seekPlayer == 0)
		{
			seekPlayer = 1;
			seekingPlayer = col.gameObject;
		}
	}
}
