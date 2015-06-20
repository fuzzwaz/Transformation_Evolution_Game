using UnityEngine;
using System.Collections;

public class horizontalTriggers : MonoBehaviour {

	//The scrolling Camera should be attached to this
	public GameObject scrollingCamera;

	//This is the main character. Should be taken from scrolling Camera script
	private GameObject mainCharacter;
	// Use this for initialization
	void Start () {
	
		mainCharacter = scrollingCamera.GetComponent<scrollCameraScript>().mainCharacter;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject == mainCharacter)
		{
			if (mainCharacter.transform.position.x - this.transform.position.x > 0)
			{
				scrollingCamera.GetComponent<scrollCameraScript>().TochingR = true;
			}
			else
			{
				scrollingCamera.GetComponent<scrollCameraScript>().TochingL = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject == mainCharacter)
		{
			scrollingCamera.GetComponent<scrollCameraScript>().TochingR = false;
			scrollingCamera.GetComponent<scrollCameraScript>().TochingL = false;
		}
	}

}
