using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bullet : MonoBehaviour {
	

	public int playerBullet = 1;
	private bool notExploded = true;
	private bool noGravity = true;
	private float timer = 0.0f;
	private bool canGet = false;
	private bool pickedUp = false;
	private int seekPlayer = 0;
	private GameObject seekingPlayer;
	private GameObject player;
	public GameObject explosion;
	public AudioClip shotSound, hitWallSound, pickUpSound;
	public int bounces = 0;

	private List<GameObject> objects;

	// Use this for initialization
	void Start () {
		player = (GameObject) GameObject.Find ("Player" + playerBullet);
		player = (GameObject) player.transform.FindChild("Body").gameObject;
		if(player == null)
		{
			Debug.Log("Bullet couldn't find the player");
		}

		if (player.GetComponent<playerAbilities>().p_LargerShot > 0.0f)
		{
			this.transform.localScale = new Vector3(this.transform.localScale.x * player.GetComponent<playerAbilities>().p_LargerShot,
			                                                this.transform.localScale.y * player.GetComponent<playerAbilities>().p_LargerShot,
			                                                this.transform.localScale.z);
		}
		AudioSource.PlayClipAtPoint(shotSound, Camera.main.transform.position);
		bounces = player.GetComponent<playerAbilities>().p_Bouncing;
		objects = new List<GameObject>();

		if (player.GetComponent<playerAbilities>().p_explosiveShot > 0.0f)
		{
			this.transform.FindChild("Fire").gameObject.SetActive(true);
		}

		if (player.GetComponent<playerAbilities>().p_Bouncing > 0)
		{
			this.transform.FindChild("Bubble").gameObject.SetActive(true);
		}

		if (player.GetComponent<playerAbilities>().p_PiercingShot)
		{
			this.transform.FindChild("Wind").gameObject.SetActive(true);
		}

		if (player.GetComponent<playerAbilities>().p_Seeking)
		{
			this.transform.FindChild("Spike").gameObject.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (player.gameObject != null && player.GetComponent<playerAbilities>().p_Seeking && seekPlayer == 1)
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

		if (player.gameObject != null && player.GetComponent<playerAbilities>().p_explosiveShot > 0.0f)
		{
			if (notExploded)
			{
				if (col.gameObject.name == "Bullet" || col.gameObject.name == "Bullet(Clone)")
				{
					GameObject newExplosion = (GameObject) GameObject.Instantiate(explosion,this.transform.position,Quaternion.identity);
					newExplosion.GetComponent<Explosion>().setPlayerNum(playerBullet);
					newExplosion.transform.localScale = new Vector3(newExplosion.transform.localScale.x * player.GetComponent<playerAbilities>().p_explosiveShot,
					                                                newExplosion.transform.localScale.y * player.GetComponent<playerAbilities>().p_explosiveShot,
					                                                newExplosion.transform.localScale.z);
					notExploded = false;
				}
				if (col.gameObject.tag == "Player")
				{
					if (col.gameObject.GetComponent<playerMovement>().playerNum != playerBullet)
					{
						GameObject newExplosion = (GameObject) GameObject.Instantiate(explosion,this.transform.position,Quaternion.identity);
						newExplosion.GetComponent<Explosion>().setPlayerNum(playerBullet);
						newExplosion.transform.localScale = new Vector3(newExplosion.transform.localScale.x * player.GetComponent<playerAbilities>().p_explosiveShot,
						                                                newExplosion.transform.localScale.y * player.GetComponent<playerAbilities>().p_explosiveShot,
						                                                newExplosion.transform.localScale.z);
						notExploded = false;
					}
				}
				else if (col.gameObject.tag == "Bullet")
				{

				}
				else
				{
					GameObject newExplosion = (GameObject) GameObject.Instantiate(explosion,this.transform.position,Quaternion.identity);
					newExplosion.GetComponent<Explosion>().setPlayerNum(playerBullet);
					newExplosion.transform.localScale = new Vector3(newExplosion.transform.localScale.x * player.GetComponent<playerAbilities>().p_explosiveShot,
					                                                newExplosion.transform.localScale.y * player.GetComponent<playerAbilities>().p_explosiveShot,
	                                                				newExplosion.transform.localScale.z);
					notExploded = false;
				}
			}
		}

		if (player.gameObject != null && player.GetComponent<playerAbilities>().p_GravityShot > 0.0f)
		{
			if (noGravity)
			{
				foreach (GameObject obj in objects)
				{
					Vector2 pullDirection = this.transform.position - obj.gameObject.transform.position;
					obj.gameObject.GetComponent<Rigidbody2D>().AddForce(pullDirection * player.GetComponent<playerAbilities>().p_GravityShot);
				}
				noGravity = false;
			}
		}
		if (col.gameObject.tag == "Block")
		{
			AudioSource.PlayClipAtPoint(hitWallSound, Camera.main.transform.position);
			Vector2 reflection = col.contacts[0].normal;

			if (bounces > 0)
			{
				bounces--;
				this.GetComponent<Rigidbody2D>().AddForce(reflection * player.GetComponent<playerShooting>().bulletForce);
			}
			else
			{
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				seekPlayer = 2;
				canGet = true;
			}
		}
		else if (col.gameObject.tag == "deadBullet")
		{
			killBullet();
		}
		else if (col.gameObject.tag == "Bullet")
		{
			if (col.gameObject.GetComponent<bullet>().canGet)
			{
				AudioSource.PlayClipAtPoint(hitWallSound, Camera.main.transform.position);
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				seekPlayer = 2;
				canGet = true;
			}
		}
		else if (col.gameObject.tag == "Player" && canGet && pickedUp == false)
		{
			AudioSource.PlayClipAtPoint(pickUpSound, Camera.main.transform.position);
			killBullet();
			col.gameObject.GetComponentInParent<playerShooting>().BulletPickedUp();
			pickedUp = true;
		}
		else if (col.gameObject.tag == "Player")
		{
			if (col.gameObject.GetComponentInParent<playerMovement>().playerNum != playerBullet)
			{
				if (this.gameObject != null && player.GetComponent<playerAbilities>().p_PiercingShot == false)
				{
					killBullet();
				}
			
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
		if (col.gameObject.tag == "Block" && bounces == 0)
		{
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			canGet = true;
		}
		else if (col.gameObject.tag == "deadBullet")
		{
			killBullet();
		}

	}

	bool isTouching (string name)
	{
		foreach (GameObject obj in objects)
		{
			if (obj.transform.parent.gameObject.name == name)
			{
				return true;
			}
		}
		return false;
	}


	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player" && col.gameObject.GetComponentInParent<playerMovement>().playerNum != playerBullet && seekPlayer == 0)
		{
			seekPlayer = 1;
			seekingPlayer = col.gameObject;
		}

		if (col.tag == "Player" && col.gameObject.GetComponentInParent<playerMovement>().playerNum != playerBullet)
		{
			if (!isTouching (col.transform.parent.gameObject.name))
			{
				objects.Add (col.gameObject);
			}
		}
	}
	void OnTriggerStay2D (Collider2D col)
	{
		if (col.tag == "Player" && col.gameObject.GetComponentInParent<playerMovement>().playerNum != playerBullet && seekPlayer == 0)
		{
			seekPlayer = 1;
			seekingPlayer = col.gameObject;
		}

		if (col.tag == "Player" && col.gameObject.GetComponentInParent<playerMovement>().playerNum != playerBullet)
		{
			if (!isTouching (col.transform.parent.gameObject.name))
			{
				objects.Add (col.gameObject);
			}
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.tag == "Player" && col.gameObject.GetComponentInParent<playerMovement>().playerNum != playerBullet)
		{
			objects.Remove(col.gameObject);
		}
	}

	void killBullet()
	{
		Destroy (this.gameObject);
	}
}
