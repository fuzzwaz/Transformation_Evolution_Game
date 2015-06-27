using UnityEngine;
using System.Collections;

public class TailLink : MonoBehaviour {

	public GameObject connected;
	public float maxDistance;
	public float catchUpSpeed = 0.1f;

	// Update is called once per frame
	void Update () 
	{
		if(connected == null)
		{
			return;
		}

		if(Vector2.Distance(transform.position, connected.transform.position) > maxDistance)
		{
			transform.position = Vector2.Lerp(transform.position, connected.transform.position, catchUpSpeed);
		}
	}
}
