using UnityEngine;
using System.Collections;

public class veritcalTriggers : MonoBehaviour {

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
			if (mainCharacter.transform.position.y - this.transform.position.y > 0)
			{
				scrollingCamera.GetComponent<scrollCameraScript>().TouchingU = true;
			}
			else
			{
				scrollingCamera.GetComponent<scrollCameraScript>().TouchingD = true;
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject == mainCharacter)
		{
			scrollingCamera.GetComponent<scrollCameraScript>().TouchingU = false;
			scrollingCamera.GetComponent<scrollCameraScript>().TouchingD = false;
		}
	}
	
}
