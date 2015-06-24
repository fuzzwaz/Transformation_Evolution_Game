using UnityEngine;
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
	public float playerSpeed = 10.0f;
	public float playerSpeedChange = 0.5f;

	/*Variables relating to dash that can change*/
	public float dashDuration = 0.2f;
	public float dashSpeed = 40.0f;
	public float dashDelay = 0.5f;
	public bool isDashing = false;

	private float savedPlayerSpeed = 450.0f;
	private float savedDashDuration = 0.25f;
	private float savedDashDelay = 0.5f;
	private bool canDash = true;
	private float verticalMovement = 0;
	private float horizontalMovement = 0;
	private GameObject GM;

	Rigidbody2D rb2d;
	public GameObject eyeBrows;


	// Use this for initialization
	void Start () {
		GM = GameObject.Find ("GameManager");
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		if (dashDuration < savedDashDuration)
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
			playerSpeed = dashSpeed;
			dashDuration -= Time.deltaTime;
			canDash = false;
			isDashing = true;
			eyeBrows.SetActive(true);
		}

		if (canDash == false)
		{dashDelay -= Time.deltaTime;}
		if (dashDelay < 0.0f)
		{
			dashDelay = savedDashDelay;
			canDash = true;
		}
		/*
		verticalMovement = Input.GetAxis("Vertical" + playerNum.ToString()) * Time.deltaTime * playerSpeed;
		horizontalMovement = Input.GetAxis ("Horizontal" + playerNum.ToString()) * Time.deltaTime * playerSpeed;

		if (blockTop && verticalMovement > 0.0f)
		{verticalMovement = 0.0f;}
		if (blockBot && verticalMovement < 0.0f)
		{verticalMovement = 0.0f;}
		if (blockRight && horizontalMovement > 0.0f)
		{horizontalMovement = 0.0f;}
		if (blockLeft && horizontalMovement < 0.0f)
		{horizontalMovement = 0.0f;}

		if (verticalMovement != 0.0f || horizontalMovement != 0.0f)
		{isMoving = true;}
		else
		{isMoving = false;}

		this.transform.position = new Vector3(this.transform.position.x + horizontalMovement, this.transform.position.y + verticalMovement, 0.0f);
		*/
	}

	void FixedUpdate()
	{
		verticalMovement = Input.GetAxis("Vertical" + playerNum.ToString()) * playerSpeed;
		horizontalMovement = Input.GetAxis ("Horizontal" + playerNum.ToString()) * playerSpeed;
		rb2d.AddForce(new Vector2(horizontalMovement, verticalMovement));
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
		GM.gameObject.GetComponent<GameManager>().playerDied(playerNum);
		Destroy (this.gameObject);
	}
}
