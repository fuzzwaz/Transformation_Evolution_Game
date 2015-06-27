using UnityEngine;
using System.Collections;

public class playerShooting : MonoBehaviour {


	public int playerNum = 1;
	public int playerShots = 5;
	public float sizeChange = 0.1f;
	public GameObject playerSprite;

	public SpriteRenderer aimingReticle;
	public Color aiming;
	public GameObject reticle;
	public Color reticleColor;
	public GameObject bullet;
	public float bulletForce = 1500.0f;
	private float bulletDelay = 0.05f;
	public float recoilForce = 3.0f;
	
	private float timer = 0.0f;
	private bool shooting = false;
	private bool recoiled = false;
	private Vector2 recoilDirection;
	private bool released = false;
	bool squish = false;

	public TailStack tailStack;
	public Animator playerAnimation;
	Colorizer colorizer;

	void Awake()
	{
		colorizer = GetComponent<Colorizer>();
	}

	// Use this for initialization
	void Start () {
		for(int i = 0; i < playerShots; ++i)
		{
			tailStack.AddSegment();
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis ("Fire" + playerNum.ToString()) >= 0.9f && playerShots > 0)
		{
			released = true;
			aimingReticle.color = reticleColor;
			if(!squish)
			{
				playerAnimation.SetTrigger("Squish");
				squish = true;
			}
		}

		if (Input.GetAxis ("Fire" + playerNum.ToString()) < 0.5 && released == true && !shooting)
		{
			if (!shooting && playerShots > 0)
			{
				Vector2 bulletDirection = new Vector2(aimingReticle.transform.position.x - this.transform.position.x,aimingReticle.transform.position.y - this.transform.position.y);
				bulletDirection.Normalize();
				Shoot(bulletDirection);

				if (this.GetComponent<playerAbilities>().p_SpreadShot)
				{
					Vector2 upDirection = new Vector2(bulletDirection.x - 0.25f, bulletDirection.y - 0.25f);
					Vector2 downDirection = new Vector2(bulletDirection.x + 0.25f, bulletDirection.y + 0.25f);

					upDirection.Normalize();
					downDirection.Normalize();
					Shoot (upDirection);
					Shoot (downDirection);
				}
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
			this.GetComponent<playerMovement>().increaseSpeed();
			timer = 0.0f;
		}

		if (Input.GetAxis ("AimHorz" + playerNum.ToString()) < 0.1 && Input.GetAxis ("AimHorz" + playerNum.ToString()) > -0.1 && Input.GetAxis ("AimVert" + playerNum.ToString()) < 0.1 && Input.GetAxis ("AimVert" + playerNum.ToString()) > -0.1)
		{
			aimingReticle.enabled = false;
		}
		else
		{
			if (released == false)
			{
				aimingReticle.color = aiming;
			}
			aimingReticle.enabled = true;
			aimingReticle.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x + Input.GetAxis ("AimHorz" + playerNum.ToString()) * 3, this.transform.position.y + Input.GetAxis ("AimVert" + playerNum.ToString()) * 3);
		}
	
	}

	public void BulletPickedUp()
	{
		tailStack.AddSegment();
		playerShots++;

		this.GetComponent<playerMovement>().decreaseSpeed();
	}

	void Shoot(Vector2 bulletDirection)
	{
		playerAnimation.SetTrigger("Unsquish");
		squish = false;

		if (playerShots > 0)
		{
			tailStack.RemoveSegment();
			GameObject newBullet = (GameObject) GameObject.Instantiate(bullet,reticle.transform.position,Quaternion.identity);
			newBullet.GetComponent<bullet>().playerBullet = playerNum;
			newBullet.GetComponent<SpriteRenderer>().color = colorizer.color;
			recoilDirection = bulletDirection * -1;
			newBullet.GetComponent<Rigidbody2D>().AddForce(bulletDirection * bulletForce);
			this.GetComponent<Rigidbody2D>().AddForce(recoilDirection * recoilForce,ForceMode2D.Impulse);
			shooting = true;
			released = false;
			playerShots--;

	        //Add a count to bullets shot
	        this.GetComponent<playerAbilities>().bulletsShot++;
		}
	}
}
