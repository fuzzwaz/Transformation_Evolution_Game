using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EvolutionNamerandHider : MonoBehaviour {
	public Text player1, player2, player3, player4;
	public Text player1score, player2score, player3score, player4score;
	public string p1String, p2String, p3String, p4String;
	public string p1scoreString, p2scoreString, p3scoreString, p4scoreString;
	public float timeToChange = 2.0f;
	float countUp = 0.0f;
	
	// Use this for initialization
	void Start () 
	{
		//TODO: Get the last evolution per player here and use the string 
		player1.text =p1String;// " ";
		player2.text = p2String;//" ";
		player3.text = p3String;//" ";
		player4.text = p4String;//" ";

		//TODO: Get each player's score and set it here (you can delete testScore)
		int testScore = 0;
		player1score.text = p1scoreString;//testScore.ToString();
		player2score.text = p2scoreString;//testScore.ToString();
		player3score.text = p3scoreString;//testScore.ToString();
		player4score.text = p4scoreString;//testScore.ToString();
	}
	
	void Update()
	{
		countUp += Time.deltaTime;
		if(countUp > timeToChange)
		{
			player1.color = new Color(player1.color.r, player1.color.g, player1.color.b, player1.color.a - 0.01f);
			player2.color = new Color(player2.color.r, player2.color.g, player2.color.b, player2.color.a - 0.01f);
			player3.color = new Color(player3.color.r, player3.color.g, player3.color.b, player3.color.a - 0.01f);
			player4.color = new Color(player4.color.r, player4.color.g, player4.color.b, player4.color.a - 0.01f);
		}
	}

	public void updateText()
	{
		player1.text =p1String;// " ";
		player2.text = p2String;//" ";
		player3.text = p3String;//" ";
		player4.text = p4String;//" ";
		player1score.text = p1scoreString;//testScore.ToString();
		player2score.text = p2scoreString;//testScore.ToString();
		player3score.text = p3scoreString;//testScore.ToString();
		player4score.text = p4scoreString;//testScore.ToString();
	}
}
