using UnityEngine;
using System.Collections;

public class playerShooting : MonoBehaviour {


	public int playerNum = 1;
	public int playerShots = 5;
	public float sizeChange = 0.1f;
	public GameObject playerSprite;

	public GameObject aimingReticle;
	public Material aiming;
	public Material reticleColor;
	public GameObject bullet;
	public float bulletForce = 1500.0f;
	private float bulletDelay = 0.05f;
	public float recoilForce = 3.0f;
	
	private float timer = 0.0f;
	private bool shooting = false;
	private bool recoiled = false;
	private Vector2 recoilDirection;
	private bool released = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis ("Fire" + playerNum.ToString()) == 1.0f)
		{
			released = true;
			aimingReticle.GetComponent<Renderer>().material = reticleColor;
			/*
			if (!shooting && playerShots > 0)
			{
				GameObject newBullet = (GameObject) GameObject.Instantiate(bullet,this.transform.position,Quaternion.identity);
				Vector2 bulletDirection = new Vector2(aimingReticle.transform.position.x - this.transform.position.x,aimingReticle.transform.position.y - this.transform.position.y);
				bulletDirection.Normalize();
				recoilDirection = bulletDirection * -1;
				newBullet.GetComponent<Rigidbody2D>().AddForce(bulletDirection * bulletForce);
				this.GetComponent<Rigidbody2D>().AddForce(recoilDirection * recoilForce,ForceMode2D.Impulse);
				shooting = true;
			}
			*/
		}

		if (Input.GetAxis ("Fire" + playerNum.ToString()) < 0.5 && released == true && !shooting)
		{
			if (!shooting && playerShots > 0)
			{
				GameObject newBullet = (GameObject) GameObject.Instantiate(bullet,this.transform.position,Quaternion.identity);
				Vector2 bulletDirection = new Vector2(aimingReticle.transform.position.x - this.transform.position.x,aimingReticle.transform.position.y - this.transform.position.y);
				bulletDirection.Normalize();
				recoilDirection = bulletDirection * -1;
				newBullet.GetComponent<Rigidbody2D>().AddForce(bulletDirection * bulletForce);
				this.GetComponent<Rigidbody2D>().AddForce(recoilDirection * recoilForce,ForceMode2D.Impulse);
				shooting = true;
				released = false;
			}
		}

		if (shooting)
		{
			if (timer > 0.02 && !recoiled)
			{
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,0.0f);
				recoiled = true;
			}
			timer += Time.deltaTime;
		}
		if (timer > bulletDelay)
		{
			shooting = false;
			recoiled = false;
			playerShots--;
			this.GetComponent<playerMovement>().increaseSpeed();
			playerSprite.gameObject.transform.localScale = new Vector3 (playerSprite.transform.localScale.x - sizeChange,playerSprite.transform.localScale.y - sizeChange, 1);
			timer = 0.0f;
		}

		if (Input.GetAxis ("AimHorz" + playerNum.ToString()) < 0.1 && Input.GetAxis ("AimHorz" + playerNum.ToString()) > -0.1 && Input.GetAxis ("AimVert" + playerNum.ToString()) < 0.1 && Input.GetAxis ("AimVert" + playerNum.ToString()) > -0.1)
		{
			aimingReticle.GetComponent<Renderer>().enabled = false;
		}
		else
		{
			if (released == false)
			{aimingReticle.GetComponent<Renderer>().material = aiming;}
			aimingReticle.GetComponent<Renderer>().enabled = true;
			aimingReticle.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x + Input.GetAxis ("AimHorz" + playerNum.ToString()) * 3, this.transform.position.y + Input.GetAxis ("AimVert" + playerNum.ToString()) * 3);
		}
	
	}

	public void BulletPickedUp()
	{
		playerShots++;
		playerSprite.gameObject.transform.localScale = new Vector3 (playerSprite.transform.localScale.x + sizeChange,playerSprite.transform.localScale.y + sizeChange, 1);

		this.GetComponent<playerMovement>().decreaseSpeed();
	}

}
