using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    static private int score = 0;

	//Initialization
	void Start()
    {
        Debug.Log(score);
	}
	
	//Updates
	void Update()
    {
	
	}

    static public void Score(int scoreMultiplier)
    {
        score += scoreMultiplier;
        Debug.Log(score);
    }
}
