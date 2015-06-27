using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TailStack : MonoBehaviour {

	public GameObject segmentClone; //The object we will clone to make tail segments
	public GameObject body; //The body of the player
	public Color color;

	Stack<GameObject> segments;

	// Use this for initialization
	void Start () 
	{
	}

	void Awake()
	{
		segments = new Stack<GameObject>();
	}

	public void AddSegment()
	{
		GameObject g = (GameObject)Instantiate(segmentClone, body.transform.position, Quaternion.identity);
		g.transform.parent = transform;
		g.GetComponent<TailLink>().connected = body;
		g.GetComponent<SpriteRenderer>().color = color;

		if(segments.Count > 0)
		{
			segments.Peek().GetComponent<TailLink>().connected = g;
		}

		segments.Push(g);
	}

	public void RemoveSegment()
	{
		if(segments.Count <= 0)
		{
			Debug.Log("Error trying to remove tail segment when there are none");
			return;
		}

	 	Destroy(segments.Pop());
		if(segments.Count > 0)
		{
			segments.Peek().GetComponent<TailLink>().connected = body;
		}
	}
}
