using UnityEngine;
using System.Collections;

public class scrollCameraScript : MonoBehaviour {

	//These are the different Camera versions the user can choose from. Just set one of them to true
	public bool SmoothScroll = true;//This is for a camera that follows the character around
	public bool JumpScroll = false; //This is for a camera that transitions to a new area instantly. 
	public bool JumpScrollSmooth = false; //This is for a camera that transitions to a new area but does it smoothly

	//Camera Additionals to go with camera type
	public bool ZoomEnable = true; //Only works with an actual mouse scroll wheel
	public bool lockLeft = false;
	public bool lockRight = false;
	public bool lockUp = false;
	public bool lockDown = false;

	//These bools are changed by the vertical and horiztonal scopes attached to the camera.
	//When their triggers are touched, these become trues
	public bool TochingR = false;
	public bool TochingL = false;
	public bool TouchingU = false;
	public bool TouchingD = false;

	public int positiveH = 0;
	public int positiveV = 0;

	private int prevH = 0;
	private int prevV = 0;
	public float cameraStall = 0.1f;
	private float camPause;

	//Please DRAG MAIN CHARACTER INTO THIS SCRIPT
	public GameObject mainCharacter;


	//Camera Movement speed (should probably be same as main character
	public float cameraMovementSpeed = 5.0f;

	//These values are for the jumping cameras, how far should the camera jump
	public float cameraJumpXDistance = 8.0f;
	public float cameraJumpYDistance = 8.0f;

	//this is to prevent rapid camera movements when jumping
	private float jumpTimerEnd = 0.05f;  //how long to wait before checking again
	private float jumpTimerStart = 0.0f;

	//This is the speed for the jumping smooth camera movemnt
	public float jumpCameraSmoothSpeed = 10.0f; //how fast do you want to transition
	public float cameraMovementTimer = 0.9f; //How long it transitions for
	private float xdirect; //horz direction to be moved to
	private float ydirect; //vert direction to be moved to
	private float cameraMovementTimerEnd = 0f;
	private bool smoothTransitionX = false;
	private bool smoothTransitionY = false;

	//controls the direction of each camera movement
	private int inputX = 0;
	private int inputY = 0;

	private Vector2 positionVector; //used to set vectors to


	void Start () {
		camPause = cameraStall;
		cameraMovementSpeed = mainCharacter.GetComponent<playerMovement>().playerSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		//Checking to see what boundaries are being touched
		//Also checks to see if user wants to lock a particular side
		//------------------------------------------------------------

		cameraMovementSpeed = mainCharacter.GetComponent<playerMovement>().playerSpeed;
		if (camPause  == cameraStall)
		{

			if (TochingR && !lockRight)
			{inputX = 1;}
			else if (TochingL && !lockLeft)
			{inputX = -1;}
			else
			{inputX = 0;}

			if (TouchingU && !lockUp)
			{inputY = 1;}
			else if (TouchingD && !lockDown)
			{inputY = -1;}
			else
			{inputY = 0;}
		}
		else
		{
			camPause -= Time.deltaTime;
			if (camPause < 0)
			{camPause = cameraStall;}
		}



		prevH = positiveH;
		prevV = positiveV;

		if (Input.GetAxis ("Horizontal") > 0)
		{positiveH = 1;}
		else if (Input.GetAxis ("Horizontal") < 0)
		{positiveH = -1;}


		if (Input.GetAxis ("Vertical") > 0)
		{positiveV = 1;}
		else if (Input.GetAxis("Vertical") < 0)
		{positiveV = -1;}


		if (TochingR == true && prevH == 1 && positiveH == -1)
		{
			TochingR = false;
			inputX = 0;
			camPause -= Time.deltaTime;
		}
		else if (TochingL == true && prevH == -1 && positiveH == 1)
		{
			TochingL = false;
			inputX = 0;
			camPause -= Time.deltaTime;
		}

		if (TouchingU == true && prevV == 1 && positiveV == -1)
		{
			TouchingU = false;
			inputY = 0;
			camPause -= Time.deltaTime;
		}
		else if (TouchingD == true && prevV == -1 && positiveV == 1)
		{
			TouchingD = false;
			inputY = 0;
			camPause -= Time.deltaTime;
		}

		//-----------------------------------------------------------------

		//SmoothScroll mode is currently enabled
		//-----------------------------------------------------------------
		if (SmoothScroll)
		{
			positionVector.x = inputX * (cameraMovementSpeed) * Time.deltaTime * Mathf.Abs (Input.GetAxis ("Horizontal"));
			positionVector.y = inputY * (cameraMovementSpeed) * Time.deltaTime * Mathf.Abs (Input.GetAxis ("Vertical"));


			this.transform.position = new Vector3(this.transform.position.x + positionVector.x, this.transform.position.y + positionVector.y, -1.0f);
			//transform.GetComponent<Rigidbody2D>().velocity = positionVector;
		}
		//----------------------------------------------------------------

		//If JumpScroll mode is currently enabled
		//Timer so that it only works once per timer set
		//-----------------------------------------------------------------
		else if (JumpScroll && jumpTimerEnd < jumpTimerStart)
		{
			positionVector.x = this.transform.position.x + (cameraJumpXDistance * inputX);
			positionVector.y = this.transform.position.y + (cameraJumpYDistance * inputY);

			transform.position = positionVector;
			jumpTimerStart = 0;
		}
		//------------------------------------------------------------------

		//If JumpScrollSmooth mode is currently enabled
		//Timer and bools to ensure that only one transitioning happening at once
		//--------------------------------------------------------------------------------
		else if (JumpScrollSmooth && jumpTimerEnd < jumpTimerStart && smoothTransitionX == false && smoothTransitionY == false)
		{

			if (inputX != 0)
			{
				smoothTransitionX = true;
				xdirect = inputX;
				ydirect = 0;
			}
			else if (inputY != 0)
			{
				smoothTransitionY = true;
				ydirect = inputY;
				xdirect = 0;
			}
			jumpTimerStart = 0;
		}
		//---------------------------------------------------------------------------------

		//If Zoom feature is enabled
		//----------------------------------------------------------------------------------
		if (ZoomEnable)
		{
			if (Input.GetAxis("Mouse ScrollWheel") > 0 && this.GetComponent<Camera>().orthographicSize > 1) // back
			{
				this.GetComponent<Camera>().orthographicSize--;
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0)
			{
				this.GetComponent<Camera>().orthographicSize++;
			}
		}
		//---------------------------------------------------------------------------------

		//This is the actualy smooth transition happening
		//----------------------------------------------------------------------------------
		if (smoothTransitionX || smoothTransitionY)
		{
			cameraMovementTimerEnd += Time.deltaTime;

			positionVector.x = xdirect * (jumpCameraSmoothSpeed);
			positionVector.y = ydirect * (jumpCameraSmoothSpeed);

			transform.GetComponent<Rigidbody2D>().velocity = positionVector;

			if (cameraMovementTimerEnd > cameraMovementTimer)
			{
				smoothTransitionX = false;
				smoothTransitionY = false;
				cameraMovementTimerEnd = 0;
				positionVector.x =0;
				positionVector.y =0;
				
				transform.GetComponent<Rigidbody2D>().velocity = positionVector;
			}
		}
		//--------------------------------------------------------------------------------------------

		jumpTimerStart += Time.deltaTime;

	}




}
