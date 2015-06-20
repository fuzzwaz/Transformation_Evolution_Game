using UnityEngine;
using System.Collections;

public class stopMovement : MonoBehaviour {

	public GameObject player;
	private int collisionType;

	// Use this for initialization
	void Start () {
		if (this.name == "TopCollision")
		{collisionType = 1;}
		else if (this.name == "BottomCollision")
		{collisionType = 2;}
		else if (this.name == "RightCollision")
		{collisionType = 3;}
		else if (this.name == "LeftCollision")
		{collisionType = 4;}
		else
		{collisionType = -1;}

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Block")
		{
			player.GetComponent<playerMovement>().stopSide(collisionType);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Block")
		{
			player.GetComponent<playerMovement>().allowSide(collisionType);
		}
	}

}
