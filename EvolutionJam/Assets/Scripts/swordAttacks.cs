using UnityEngine;
using System.Collections;

public class swordAttacks : MonoBehaviour {

	public int directionFacing = 0; // 0 - UP, 1 - Down, 2 - Right, 3 - Left
	public GameObject sword;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis("Vertical") > 0.0f)
		{directionFacing = 0;}
		else if (Input.GetAxis("Vertical") < 0.0f)
		{directionFacing = 1;}
		else if (Input.GetAxis ("Horizontal") > 0.0f)
		{directionFacing = 2;}
		else if (Input.GetAxis ("Horizontal") < 0.0f)
		{directionFacing = 3;}

		if (Input.GetMouseButtonDown(0) || Input.GetAxis ("Fire1") != 0.0f)
		{
			if (sword.activeSelf == false)
			{sword.SetActive(true);}

			if(sword.GetComponent<BoxCollider2D>().isActiveAndEnabled == false)
			{
				sword.GetComponent<Animator>().Play(0);
			}
		}
	}
}
