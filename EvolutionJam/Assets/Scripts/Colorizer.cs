using UnityEngine;
using System.Collections;

public class Colorizer : MonoBehaviour {

	public Color color;
	public GameObject[] objectsToColor;

	// Use this for initialization
	void Start () {
		foreach(GameObject g in objectsToColor)
		{
			g.GetComponent<SpriteRenderer>().color = color;
		}
	}
}
