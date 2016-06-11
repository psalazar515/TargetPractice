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

    static public void Score()
    {
        score++;
        Debug.Log(score);
    }
}
