﻿using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {

	/*Stopping player movement for smooth collision*/
	public bool isMoving = false;
	public bool blockTop = false;
	public bool blockBot = false;
	public bool blockRight = false;
	public bool blockLeft = false;

	/*Specific variables for the player*/
	public int playerNum = 1;
	public float playerSpeed = 1350.0f;
	public float playerSpeedChange = 0.5f;

	/*Variables relating to dash that can change*/
	public float dashDuration = 0.2f;
	public float dashSpeed = 40.0f;
	private float dashDelay = 1.5f;
	public bool isDashing = false;

	public float poisionDelay = 0.3f;
	public GameObject poision;
	private float poisionDelayTemp;

    private float lengthOfLife = 0.0f;
	private float savedPlayerSpeed = 1350.0f;
	private float savedDashDuration = 0.25f;
	private float savedDashDelay = 1.5f;
	private bool canDash = true;
	private float verticalMovement = 0;
	private float horizontalMovement = 0;
	private GameObject GM;

	Rigidbody2D rb2d;
	public Collider2D bodyCollider;
	public GameObject eyeBrows;
	public Animator playerAnimation;
	bool dead = false;
	public playerShooting shooting;
	public GameObject tailStack;
	public AudioClip deathSound, dashSound;

	private int growth = 0;
	private Vector3 currentScale;

	// Use this for initialization
	void Start () {
		GM = GameObject.Find ("GameManagerMaster");
		rb2d = GetComponent<Rigidbody2D>();
		poisionDelayTemp = poisionDelay;
		savedDashDuration = dashDuration;
		savedDashDelay = dashDelay;
		savedPlayerSpeed = playerSpeed;
		currentScale = this.transform.parent.transform.localScale;


	}
	
	// Update is called once per frame
	void Update () {
		if(dead)
		{
			return;
		}


		if (isDashing  && growth == 0)
		{
			if (this.GetComponent<playerAbilities>().p_GrowingDash > 0.0f)
			{
				this.transform.parent.transform.localScale = new Vector3(this.transform.parent.transform.localScale.x + this.GetComponent<playerAbilities>().p_GrowingDash,
			                                        this.transform.parent.transform.localScale.y + this.GetComponent<playerAbilities>().p_GrowingDash,
			                                       this.transform.parent.transform.localScale.z);
			}

			this.transform.FindChild("DashSpikes").gameObject.SetActive(this.GetComponent<playerAbilities>().p_SpikingDash);
			growth = 1;
		}
		
		if (!isDashing && growth == 1)
		{
			if (this.GetComponent<playerAbilities>().p_GrowingDash > 0.0f)
			{
				this.transform.parent.transform.localScale = currentScale;
			}

			this.transform.FindChild("DashSpikes").gameObject.SetActive(false);
			growth = 0;
		}
		
		lengthOfLife += Time.deltaTime;
		//if (dashDuration < savedDashDuration)
		if (isDashing)
		{dashDuration -= Time.deltaTime;}
		if (dashDuration < 0.0f)
		{
			dashDuration = savedDashDuration;
			playerSpeed = savedPlayerSpeed;
			isDashing = false;
			eyeBrows.SetActive(false);
		}

		if (Input.GetAxis ("Dash" + playerNum.ToString()) == 1.0f && canDash) 
		{
			AudioSource.PlayClipAtPoint(dashSound, Camera.main.transform.position);
			playerSpeed = dashSpeed;
			dashDuration -= Time.deltaTime;
			canDash = false;
			isDashing = true;
			eyeBrows.SetActive(true);
			rb2d.AddForce(rb2d.velocity.normalized * playerSpeed, ForceMode2D.Impulse);

            this.GetComponent<playerAbilities>().dashesMade++;
		}

		if (canDash == false && isDashing == false)
		{dashDelay -= Time.deltaTime;}
		if (dashDelay < 0.0f)
		{
			dashDelay = savedDashDelay;
			canDash = true;
		}

		if (this.GetComponent<playerAbilities>().p_Poision)
		{
			if (poisionDelay < 0)
			{
				poisionDelay = poisionDelayTemp;
				GameObject newPoision = (GameObject) GameObject.Instantiate(poision,this.transform.position,Quaternion.identity);
				newPoision.GetComponent<poisionGas>().setPlayer(playerNum);
			}
			poisionDelay -= Time.deltaTime;
		}
	}

	void FixedUpdate()
	{
		
		if(dead)
		{
			return;
		}
		verticalMovement = Input.GetAxis("Vertical" + playerNum.ToString()) * playerSpeed;
		horizontalMovement = Input.GetAxis ("Horizontal" + playerNum.ToString()) * playerSpeed;
		rb2d.AddForce(new Vector2(horizontalMovement, verticalMovement));

		RotateToAimDirection();
	}

	void RotateToAimDirection()
	{
		Vector2 v = new Vector2(Input.GetAxis("AimHorz" + playerNum.ToString()), Input.GetAxis("AimVert" + playerNum.ToString()));

		if(v.x < 0.01f && v.y < 0.01f && v.x > -0.1f && v.y > -0.1f)
		{
			v = rb2d.velocity;
		}

		v.Normalize();
		float angle = Vector2.Angle(Vector2.up, v);
		if(v.x > 0.0f)
		{
			angle = 360 - angle;
		}
		transform.eulerAngles = new Vector3(0, 0, angle);
	}

	public void stopSide (int side)
	{
		if (side == 1)
		{blockTop = true;}
		else if (side == 2)
		{blockBot = true;}
		else if (side == 3)
		{blockRight = true;}
		else if (side == 4)
		{blockLeft = true;}
	}

	public void allowSide (int side)
	{
		if (side == 1)
		{blockTop = false;}
		else if (side == 2)
		{blockBot = false;}
		else if (side == 3)
		{blockRight = false;}
		else if (side == 4)
		{blockLeft = false;}
	}

	public void increaseSpeed()
	{
		savedPlayerSpeed+=playerSpeedChange;
		if (dashDuration == savedDashDuration)
		{
			playerSpeed = savedPlayerSpeed;
		}

	}

	public void decreaseSpeed()
	{
		savedPlayerSpeed-=playerSpeedChange;
		if (dashDuration == savedDashDuration)
		{
			playerSpeed = savedPlayerSpeed;
		}
	}

	public void killed()
	{
		AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
		dead = true;
		playerAnimation.SetTrigger("Die");
		GM = GameObject.Find ("GameManagerMaster");
		GM.gameObject.GetComponent<GameManager>().playerDied(playerNum);
        this.GetComponent<playerAbilities>().lengthOfLife = lengthOfLife;
        this.GetComponent<playerAbilities>().UpdatePlayerInfo();
		Destroy(shooting);
		Destroy(tailStack);
		Destroy(bodyCollider);
		Destroy(rb2d);
	}

	public void increaseDashDuration (float increase)
	{
		dashDuration += increase;
		savedDashDuration = dashDuration;

	}

	public void increaseDashSpeed (float increase)
	{
		dashSpeed += increase;
		
	}

	public void increaseSpeed (float increase)
	{
		playerSpeed += increase;	
		savedPlayerSpeed = playerSpeed;
	}

	public void CleanUpDeceasedBody()
	{
		Destroy (this.gameObject);
	}
}
