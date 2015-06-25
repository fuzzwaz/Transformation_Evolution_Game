using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class surroundingObjects : MonoBehaviour {

	public int objectCount = 0;
	private List<string> objects;
	// Use this for initialization
	void Start () {
		objects = new List<string>();
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<playerAbilities>().surroundingObjects = objectCount;
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (!isTouching (col.gameObject.name))
		{
			objects.Add (col.gameObject.name);
		}
	}
	
	void OnTriggerExit2D (Collider2D col)
	{
		objects.Remove(col.gameObject.name);
	}

	bool isTouching (string name)
	{
		foreach (string obj in objects)
		{
			if (obj == name)
			{
				return true;
			}
		}
		return false;
	}
}
