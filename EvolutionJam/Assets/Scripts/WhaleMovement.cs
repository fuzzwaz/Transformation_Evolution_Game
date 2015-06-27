using UnityEngine;
using System.Collections;

public class WhaleMovement : MonoBehaviour {

	public Transform startPosition;
	public float _velocity = 1.0f;
	public Transform finalPosition;
	public Transform stageTop;
	public Transform stageBottom;
	public float minScale = 1.2f;
	public float maxScale = 2.5f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = transform.position;
		position.x -= _velocity * Time.deltaTime;
		transform.position = position;

		if (transform.position.x < finalPosition.position.x)
		{
			transform.position = startPosition.position;
			float whaleHeight = Random.Range(stageBottom.position.y, stageTop.position.y);
			Vector3 whalePosition = new Vector3(transform.position.x, whaleHeight, transform.position.z);
			transform.position = whalePosition;

			float scale = Random.Range(minScale, maxScale);
			Vector3 whaleScale = new Vector3(scale, scale, scale);
			transform.localScale = whaleScale;
		}
	}
}
